using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public static bool startMoving;

    private bool keyAlternate;
    private Vector3 PlayerScreenPos;

    public float lastJumpCountdown;

    public static bool isDead;
    public static bool grounded;

    private AudioManager audioManager;

    public static float jumpCountdown;

    public static int Score = 0;
    public static int Round = 1;
    public static int Lives = 3;

    private Vector3 fingerStart, fingerEnd;

    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private Vector2 firstPressPos;

    public static bool shortJump;
    public static bool longJump;
    public static bool headJump;
    public static bool addWalkForce;
    public static float walkSpeed;

    public Vector3 bonus1Pos, bonus2Pos, bonus3Pos;

    public Animator playerAnim;
    public GameObject ScorePopUpPrefab;
    [HideInInspector]
    public Rigidbody2D rb;
    public Sprite x30Sprite;
    private Camera cam;
    public GameObject parentForScorePopUp;
    public Animator StartAnimation;
    public Animator blueOverlayAnimation;
    public Animator blueScreenFadeInAnim;

    public GameObject ArcadeGameObject;

    public Transform objectTransfom;

    private float noMovementThreshold = 0.0001f;
    private const int noMovementFrames = 3;
    Vector3[] previousLocations = new Vector3[noMovementFrames];

    public static bool FirstWalk = false;
    public static bool wasDead;

    public static float _Counter;
    public static bool timeOver5Mins = false;


    IEnumerator StartScreenArcade() {

        yield return new WaitForSeconds(0.4f);

        ArcadeGameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        ArcadeGameObject.SetActive(false);

    }

    void Start() {

        FirstWalk = true;

        if (wasDead) {
            StartAnimation.SetTrigger("Go");
        }

        //For good measure, set the previous locations
        for (int i = 0; i < previousLocations.Length; i++) {
            previousLocations[i] = Vector3.zero;
        }

        blueOverlayAnimation.SetTrigger("Activate");

        if (StartScreenButtons.comingFromStartMenu) {

            StartScreenButtons.comingFromStartMenu = false;

            // IF Android --> set second animation which is the 
            // same but with different sprites

#if UNITY_STANDALONE

            StartCoroutine("StartScreenArcade");

#endif

#if UNITY_WEBGL

            //StartAnimation.SetTrigger("Activate");
            StartCoroutine("StartScreenArcade");

#endif

#if UNITY_ANDROID


#endif

#if UNITY_IOS


#endif

        }

        cam = Camera.main;

        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();


        CollisionManager.hitCountdown = 0;

        jumpCountdown = 0;
        lastJumpCountdown = 0;

        grounded = true;
        isDead = false;

        startMoving = false;

        audioManager = AudioManager.instance;

        if (audioManager == null) {
            Debug.LogError("No audioManager found");
        }

        if (CollisionManager.bonusLevelActive1) {
            CollisionManager.bonusLevelActive1 = false;
            cam.transform.position = new Vector3(bonus1Pos.x, 0, -10);
            StartAnimation.SetTrigger("Go");

        } else if (CollisionManager.bonusLevelActive2) {
            CollisionManager.bonusLevelActive2 = false;
            cam.transform.position = new Vector3(bonus2Pos.x, 0, -10);
            StartAnimation.SetTrigger("Go");

        }

    }


    public void HeadJump(Collider2D col) {

        GameObject jumpEffect = (GameObject)Instantiate(Resources.Load("HeadJumpEffect"),
            new Vector3(col.transform.position.x, col.transform.position.y + 0.5f, -8), Quaternion.identity);
        Destroy(jumpEffect, 1f);

        playerAnim.SetTrigger("LittleJump");

        headJump = true;
        AudioManager.instance.PlaySound("HeadJump");

        //add points and pop up "x30"
        Score += 30;
        EndScreenScript._FinalHeadJumps += 30;

        ShowScorePopUp(x30Sprite);

    }

    public void ShowScorePopUp(Sprite sprite) {

        GameObject scorePopUp = (GameObject)Instantiate(ScorePopUpPrefab, new Vector2(0, 0), Quaternion.identity, parentForScorePopUp.transform);
        scorePopUp.GetComponent<SpriteRenderer>().sprite = sprite;
        Destroy(scorePopUp, 1f);

    }

    public void Jump() {

        AudioManager.instance.PlaySound("Jump");

        CollisionManager.hitCountdown = 0;
        FindObjectOfType<CollisionManager>().caughtAnimGO.SetActive(false);

        grounded = false;

        playerAnim.SetTrigger("Jump");

        if (jumpCountdown >= 1.4f) {
            longJump = true;
            lastJumpCountdown = 0.7f;

        } else
            shortJump = true;
        lastJumpCountdown = 0.7f;

    }

    void CheckIfLost() {


        PlayerScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (PlayerScreenPos.x <= Screen.width / Screen.width && !isDead) {


            FindObjectOfType<cameraShake>().shake_intensity = 0.5f;
            FindObjectOfType<cameraShake>().Shake();
            AudioManager.instance.PlaySound("Death");

            Score = 0;

            Lives--;

            wasDead = true;

            if (Lives < 0) {

                SceneManager.LoadScene(4);

            } else {

                if (transform.position.x > 100 && transform.position.x < 200) {
                    CollisionManager.bonusLevelActive1 = true;
                } else if (transform.position.x > 207) {
                    CollisionManager.bonusLevelActive2 = true;
                }

                isDead = true;

                StartCoroutine(Death());
            }
        }
    }

    IEnumerator Death() {

        blueOverlayAnimation.SetTrigger("Death");

        yield return new WaitForSeconds(0.25f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

    void CheckIfGrounded() {
        if (transform.position.y <= -2.4) {
            grounded = true;
        }
    }

    void CheckIfStopped() {

        for (int i = 0; i < previousLocations.Length - 1; i++) {
            previousLocations[i] = previousLocations[i + 1];
        }
        previousLocations[previousLocations.Length - 1] = objectTransfom.position;

        //Check the distances between the points in your previous locations
        //If for the past several updates, there are no movements smaller than the threshold,
        //you can most likely assume that the object is not moving
        for (int i = 0; i < previousLocations.Length - 1; i++) {
            if (Vector3.Distance(previousLocations[i], previousLocations[i + 1]) >= noMovementThreshold) {
                playerAnim.SetBool("Idle", false);
            } else {
                playerAnim.SetBool("Idle", true);
                break;
            }
        }
    }

    void FixedUpdate() {

        if (addWalkForce) {

            rb.drag = 25;
            rb.AddForce(Vector2.right * walkSpeed, ForceMode2D.Impulse);
            addWalkForce = false;
        }

        if (shortJump) {

            rb.drag = 2;
            lastJumpCountdown = 1f;
            CollisionManager.hitCountdown = 0;
            rb.velocity = new Vector2(7, 28);
            shortJump = false;
        }

        if (longJump) {

            rb.drag = 2;
            lastJumpCountdown = 1f;
            CollisionManager.hitCountdown = 0;
            rb.velocity = new Vector2(13, 28);
            longJump = false;

        }

        if (headJump) {

            rb.AddForce(new Vector2(6, 25), ForceMode2D.Impulse);
            headJump = false;

        }
    }

    void Update() {

        _Counter += Time.deltaTime;

        if (_Counter >= 300) {
            timeOver5Mins = true;
        } else {
            timeOver5Mins = false;
        }

        if (transform.position.y >= -2.4) {
            rb.drag = 2;
        } else if (transform.position.y <= -2.4) {
            rb.drag = 25;
        }

        if (jumpCountdown > 0) {
            jumpCountdown -= Time.deltaTime;
        }

        if (lastJumpCountdown > 0) {
            lastJumpCountdown -= Time.deltaTime;
        }

        if (jumpCountdown <= 0) {
            jumpCountdown = 0;
        }
        if (lastJumpCountdown <= 0) {
            lastJumpCountdown = 0;
        }

        CheckIfLost();

        CheckIfGrounded();

        CheckIfStopped();

        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.H) ||
          Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) ||
          Input.GetAxis("Walk1") > 0 || Input.GetAxis("Walk2") > 0 || Input.GetAxis("Jump") > 0) {

            startMoving = true;

            if (FirstWalk) {
                FirstWalk = false;
                AudioManager.instance.PlaySound("AfterBonusGO");
            }
        }

        if (!grounded || transform.position.y >= -2.4) {
            return;
        }

        if (CameraMovement.maxReached || isDead) {
            return;
        }


        // For Linux, PC, Mac
#if UNITY_STANDALONE

        if (Input.GetAxis("Jump") > 0 && grounded) {

            if (lastJumpCountdown > 0) {
                return;
            }
            Jump();

        }


        if (Input.GetAxis("Walk1") > 0 && Input.GetAxis("Walk2") <= 0 && keyAlternate == false) {


            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                walkSpeed = 23;
                playerAnim.SetTrigger("WalkLeft");
                jumpCountdown = 2;

                keyAlternate = true;

            }

        } else if (Input.GetAxis("Walk2") > 0 && Input.GetAxis("Walk1") <= 0 && keyAlternate == true) {

            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                walkSpeed = 22;
                jumpCountdown = 2;
                playerAnim.SetTrigger("WalkRight");

                keyAlternate = false;

            }

        }

#endif


#if UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {

            if (lastJumpCountdown > 0) {
                return;
            }
            Jump();

        }


        if (Input.GetKeyDown(KeyCode.G) && keyAlternate == false) {


            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                walkSpeed = 19;

                playerAnim.SetTrigger("WalkLeft");

                jumpCountdown = 2;

                keyAlternate = true;

            }

        } else if (Input.GetKeyDown(KeyCode.H) && keyAlternate == true) {

            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                jumpCountdown = 2;

                walkSpeed = 17;

                playerAnim.SetTrigger("WalkRight");

                keyAlternate = false;

            }

        }

#endif

    }
}
