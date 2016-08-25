using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public static bool startMoving;

    private bool keyAlternate;

    private float speed;
    private float lastJumpCountdown;

    private Vector3 nextPos;
    private Vector3 PlayerScreenPos;

    public float timeTakenDuringLerp = 0.1f;
    private bool isLerping;
    private float _timeStartedLerping;

    public static bool isDead;
    public static bool grounded;

    private AudioManager audioManager;

    private float jumpCountdown;

    public static int Round, Lives, Score;
    private Vector3 fingerStart, fingerEnd;

    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private Vector2 firstPressPos;

    public static bool mobileJump;


    private Animator playerAnim;


    void Start() {

        playerAnim = GetComponent<Animator>();

        speed = 2f;

        Round = 1;
        Lives = 3;
        Score = 0;

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

    public void StartLerping(Vector3 lerpDir, float distanceToMove) {

        isLerping = true;
        _timeStartedLerping = Time.time;
        nextPos = transform.position + lerpDir * distanceToMove;

    }


    //Only for jump on enemy's head
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Head") {

            timeTakenDuringLerp = 0.2f;
            StartLerping(new Vector3(0.5f, 0.3f, 0), 5f);
            CollisionManager.captured = false;

        }
    }



    void Jump() {

        if (jumpCountdown >= 1.4f) {
            StartLerping(new Vector3(0.8f, 0.8f, 0), 4f);
            playerAnim.SetTrigger("Jump");
            timeTakenDuringLerp = 0.2f;
            lastJumpCountdown = 0.5f;

        } else
            StartLerping(new Vector3(0.6f, 0.6f, 0), 4f);
        playerAnim.SetTrigger("Jump");
        timeTakenDuringLerp = 0.2f;
        lastJumpCountdown = 0.5f;

    }

    void CheckIfLost() {


        PlayerScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (PlayerScreenPos.x <= Screen.width / Screen.width && !isDead) {
            Debug.Log("Lost");
            isDead = true;
        }

    }

    void Update() {


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

        if (isLerping) {
            grounded = false;
        }

        if (transform.position.y >= -3.3f) {

            Vector3 tempPosY = transform.position;
            tempPosY.y -= 0.5f;
            transform.position = tempPosY;

        }

        if (transform.position.y <= -3.25f) {

            grounded = true;
            transform.position = new Vector3(transform.position.x, -3.3f);

        }

        if (isLerping) {

            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percantageComplete = timeSinceStarted / timeTakenDuringLerp;
            transform.position = Vector3.Lerp(transform.position, nextPos, percantageComplete);

            if (percantageComplete >= 1.0f) {
                isLerping = false;
                playerAnim.SetTrigger("Land");

            }

        }

        if (!grounded) {
            return;
        }


        if (CameraMovement.maxReached || isDead) {
            return;
        }



        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.H) || Input.GetMouseButtonDown(0)) {

            startMoving = true;
        }


        //When player activates bonus level dont allow movement
        if (CollisionManager.bonusLevelActive) {
            Debug.Log("BONUS LEVEL ACTIVATED");
            return;
        }



#if UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {

            if (lastJumpCountdown > 0) {
                return;
            }
            Jump();

        }

        if (CollisionManager.captured) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.G) && keyAlternate == false) {


            StartLerping(Vector3.right, 1f);
            jumpCountdown = 2;
            playerAnim.SetTrigger("Walk");
            timeTakenDuringLerp = 0.1f;

            keyAlternate = true;

        } else if (Input.GetKeyDown(KeyCode.H) && keyAlternate == true) {

            StartLerping(Vector3.right, 0.85f);
            jumpCountdown = 2;
            playerAnim.SetTrigger("Walk");
            timeTakenDuringLerp = 0.1f;

            keyAlternate = false;
        }

#endif


#if UNITY_ANDROID



        if (Input.GetMouseButtonDown(0)) {

            //Origin point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (CollisionManager.captured) {
                return;
            }

            playerAnim.SetTrigger("Walk");
            StartLerping(Vector3.right, 1.1f);

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

                if (CollisionManager.captured) {
                    CollisionManager.hitCountdown = 0;
                    CollisionManager.captured = false;

                }
                Jump();
                mobileJump = true;

            }



        }





#endif

#if UNITY_IOS


      

#endif

    }
}
