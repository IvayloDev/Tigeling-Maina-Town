using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public static bool startMoving;

    private bool keyAlternate;
    private Vector3 PlayerScreenPos;

    private float lastJumpCountdown;

    public static bool isDead;
    public static bool grounded;

    private AudioManager audioManager;

    private float jumpCountdown;

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
    private float walkSpeed;

    public Vector3 bonus1Pos, bonus2Pos, bonus3Pos;

    private Animator playerAnim;
    public GameObject ScorePopUpPrefab;
    private Rigidbody2D rb;
    public Sprite x30Sprite;
    private Camera cam;

    public GameObject parentForScorePopUp;

    void Start() {

        cam = Camera.main;

        if (CollisionManager.bonusLevelActive1) {
            CollisionManager.bonusLevelActive1 = false;
            cam.transform.position = new Vector3(bonus1Pos.x, 0, -10);

        } else if (CollisionManager.bonusLevelActive2) {
            CollisionManager.bonusLevelActive2 = false;
            cam.transform.position = new Vector3(bonus2Pos.x, 0, -10);

        }


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


    }



    public void HeadJump(Collider2D col) {

        GameObject jumpEffect = (GameObject)Instantiate(Resources.Load("HeadJumpEffect"),
            new Vector3(col.transform.position.x, col.transform.position.y + 0.5f, -5), Quaternion.identity);
        Destroy(jumpEffect, 1f);


        headJump = true;

        //add points and pop up "x30"
        Score += 30;

        ShowScorePopUp(x30Sprite);

    }

    public void ShowScorePopUp(Sprite sprite) {

        GameObject scorePopUp = (GameObject)Instantiate(ScorePopUpPrefab, new Vector2(0, 0), Quaternion.identity, parentForScorePopUp.transform);
        scorePopUp.GetComponent<SpriteRenderer>().sprite = sprite;
        Destroy(scorePopUp, 1f);

    }

    void Jump() {

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

            Lives--;

            if (Lives < 0) {
                //END SCENE
                SceneManager.LoadScene(4);

            } else {

                if (transform.position.x > 65) {
                    CollisionManager.bonusLevelActive1 = true;
                } else if (transform.position.x > 165) {
                    CollisionManager.bonusLevelActive2 = true;
                }


                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Score = 0;
                isDead = true;
            }
        }

    }

    void CheckIfGrounded() {

        if (transform.position.y <= -2.4) {

            playerAnim.SetTrigger("Land");
            grounded = true;
        }

    }

    void FixedUpdate() {


        if (addWalkForce) {

            rb.drag = 25;
            rb.AddForce(Vector2.right * walkSpeed, ForceMode2D.Impulse);
            addWalkForce = false;

        }

        if (headJump) {

            rb.AddForce(new Vector2(5, 20), ForceMode2D.Impulse);
            headJump = false;

        }


        if (shortJump) {

            rb.drag = 2;
            lastJumpCountdown = 1f;
            CollisionManager.hitCountdown = 0;
            rb.velocity = new Vector2(7, 25);
            //rb.AddForce(new Vector2(8, 25), ForceMode2D.Impulse);
            shortJump = false;

        }

        if (longJump) {


            rb.drag = 2;
            lastJumpCountdown = 1f;
            CollisionManager.hitCountdown = 0;
            rb.velocity = new Vector2(13, 25);
            //rb.AddForce(new Vector2(10, 25), ForceMode2D.Impulse);
            longJump = false;

        }

    }

    void Update() {

        if (!grounded) {
            rb.drag = 2;
        }

        if (rb.velocity.x <= 0) {
            playerAnim.SetBool("Walk", false);
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

        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.H) ||
          Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {

            startMoving = true;
        }

        if (!grounded) {
            return;
        }


        if (CameraMovement.maxReached || isDead) {
            return;
        }






        //When player activates bonus level dont allow movement
        //if (CollisionManager.bonusLevelActive) {
        //    Debug.Log("BONUS LEVEL ACTIVATED");
        //    return;
        //}



        // For Linux, PC, Mac
#if UNITY_STANDALONE

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {

            if (lastJumpCountdown > 0) {
                return;
            }
            Jump();
            playerAnim.SetBool("Walk", false);

        }


        if (Input.GetKeyDown(KeyCode.G) && keyAlternate == false) {


            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                walkSpeed = 6;
                playerAnim.SetBool("Walk", true);
                jumpCountdown = 2;


                keyAlternate = true;

            }

        } else if (Input.GetKeyDown(KeyCode.H) && keyAlternate == true) {

            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                walkSpeed = 6;

                jumpCountdown = 2;


                playerAnim.SetBool("Walk", true);

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
            playerAnim.SetBool("Walk", false);

        }


        if (Input.GetKeyDown(KeyCode.G) && keyAlternate == false) {


            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                //walkSpeed = 5;
                walkSpeed = 19;

                playerAnim.SetBool("Walk", true);
                jumpCountdown = 2;


                keyAlternate = true;

            }

        } else if (Input.GetKeyDown(KeyCode.H) && keyAlternate == true) {

            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                jumpCountdown = 2;

                //walkSpeed = 4.6f;
                walkSpeed = 17;

                playerAnim.SetBool("Walk", true);

                keyAlternate = false;

            }

        }

#endif


#if UNITY_ANDROID



        if (Input.GetMouseButtonDown(0)) {

            //Origin point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (CollisionManager.hitCountdown <= 0) {

                walkSpeed = 7f;
                playerAnim.SetTrigger("Walk");
                addWalkForce = true;
                jumpCountdown = 2;


            }

        }

        if (Input.GetMouseButtonUp(0)) {

            //Destination point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);


            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);



            if (currentSwipe.x <= 10 && currentSwipe.y <= 10) {

                jumpCountdown = 2;
            }

            if (currentSwipe.y >= 80) {

                if (lastJumpCountdown > 0) {
                    return;
                }

                Jump();

            }



        }





#endif

#if UNITY_IOS


      

#endif

    }
}
