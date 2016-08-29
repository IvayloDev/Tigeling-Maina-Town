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

    public static int Round, Score;
    public static int Lives = 3;
    private Vector3 fingerStart, fingerEnd;

    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private Vector2 firstPressPos;



    public static bool shortJump;
    public static bool longJump;
    public static bool addWalkForce;
    private float walkSpeed;


    private Animator playerAnim;
    private Rigidbody2D rb;

    void Start() {

        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Round = 1;
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


    //Only for jump on enemy's head
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Head") {

            shortJump = true;

        }
    }



    void Jump() {

        grounded = false;

        playerAnim.SetTrigger("Jump");

        if (jumpCountdown >= 1.4f) {
            longJump = true;
            lastJumpCountdown = 0.5f;

        } else
            shortJump = true;
        lastJumpCountdown = 0.5f;

    }

    void CheckIfLost() {


        PlayerScreenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (PlayerScreenPos.x <= Screen.width / Screen.width && !isDead) {

            Lives--;

            if (Lives < 0) {
                //END SCENE
                SceneManager.LoadScene(0);

            } else {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                isDead = true;
            }
        }

    }

    void CheckIfGrounded() {

        if (transform.position.y <= -3.25) {

            playerAnim.SetTrigger("Land");
            grounded = true;
        }

    }

    void FixedUpdate() {

        if (addWalkForce) {

            rb.AddForce(Vector2.right * walkSpeed, ForceMode2D.Impulse);

            addWalkForce = false;
        }

        if (shortJump) {

            lastJumpCountdown = 0.8f;
            CollisionManager.hitCountdown = 0;
            rb.AddForce(new Vector2(30, 50), ForceMode2D.Impulse);
            shortJump = false;

        }

        if (longJump) {

            lastJumpCountdown = 0.8f;
            CollisionManager.hitCountdown = 0;
            rb.AddForce(new Vector2(40, 50), ForceMode2D.Impulse);
            longJump = false;

        }

    }

    void Update() {

        // Maybe Check if player has jumped too high duo to jump + autojump
        // From head collision and after curtain height set gravity to high

        if (transform.position.y > -0.8f) {
            rb.gravityScale = 250f;
        } else rb.gravityScale = 15f;



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
        if (CollisionManager.bonusLevelActive) {
            Debug.Log("BONUS LEVEL ACTIVATED");
            return;
        }



        // For Linux, PC, Mac
#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {

            if (lastJumpCountdown > 0) {
                return;
            }
            Jump();

        }


        if (Input.GetKeyDown(KeyCode.G) && keyAlternate == false) {


            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                walkSpeed = 17f;
                playerAnim.SetTrigger("Walk");
                jumpCountdown = 2;


                keyAlternate = true;

            }

        } else if (Input.GetKeyDown(KeyCode.H) && keyAlternate == true) {

            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                jumpCountdown = 2;

                walkSpeed = 17f;

                playerAnim.SetTrigger("Walk");

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

                walkSpeed = 17f;
                playerAnim.SetTrigger("Walk");
                jumpCountdown = 2;


                keyAlternate = true;

            }

        } else if (Input.GetKeyDown(KeyCode.H) && keyAlternate == true) {

            if (CollisionManager.hitCountdown <= 0) {

                addWalkForce = true;

                jumpCountdown = 2;

                walkSpeed = 17f;

                playerAnim.SetTrigger("Walk");

                keyAlternate = false;

            }

        }

#endif


#if UNITY_ANDROID



        if (Input.GetMouseButtonDown(0)) {

            //Origin point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (CollisionManager.hitCountdown <= 0) {

                walkSpeed = 23f;
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
