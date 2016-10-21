using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    private Vector2 firstPressPos;

    private RaycastHit hit;

    private bool keyAlternate;

    private PlayerMovement player;



    void Start() {

        player = FindObjectOfType<PlayerMovement>();

    }

    void WalkLeft(RaycastHit hit) {

        if (CollisionManager.hitCountdown <= 0) {

            PlayerMovement.walkSpeed = 18.5f;
            player.playerAnim.SetTrigger("WalkLeft");
            PlayerMovement.addWalkForce = true;
            PlayerMovement.jumpCountdown = 2;


        }

    }

    void WalkRight(RaycastHit hit) {

        if (CollisionManager.hitCountdown <= 0) {

            PlayerMovement.walkSpeed = 19f;
            player.playerAnim.SetTrigger("WalkRight");
            PlayerMovement.addWalkForce = true;
            PlayerMovement.jumpCountdown = 2;


        }

    }

    void Update() {

#if UNITY_ANDROID

        if (Input.GetMouseButtonDown(0)) {

            //Origin point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        }

        if (Input.GetMouseButtonUp(0)) {

            //Destination point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);


            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            if (currentSwipe.y >= 80) {

                if (player.lastJumpCountdown > 0) {
                    return;
                }

                PlayerMovement.jumpCountdown = 2;
                player.Jump();

            }
            return;
        }

        //If its clicked on the screen
        if (Input.GetMouseButton(0) && PlayerMovement.grounded && player.transform.position.y <= -2.4) {


            // Cast ray 
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(toMouse, out hit, 100.0f)) {

                //if the box is the top

                if (hit.transform.tag == "LeftSide" && keyAlternate == false) {

                    WalkLeft(hit);

                    keyAlternate = true;
                    return;

                }

                if (hit.transform.tag == "RightSide" && keyAlternate == true) {

                    WalkRight(hit);

                    keyAlternate = false;
                    return;

                }

            }
        }


#endif
#if UNITY_IOS

        if (Input.GetMouseButtonDown(0)) {

            //Origin point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        }

        if (Input.GetMouseButtonUp(0)) {

            //Destination point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);


            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            if (currentSwipe.y >= 80) {

                if (player.lastJumpCountdown > 0) {
                    return;
                }

                PlayerMovement.jumpCountdown = 2;
                player.Jump();

            }
            return;
        }

        //If its clicked on the screen
        if (Input.GetMouseButton(0) && PlayerMovement.grounded && player.transform.position.y <= -2.4) {


            // Cast ray 
            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(toMouse, out hit, 100.0f)) {

                //if the box is the top

                if (hit.transform.tag == "LeftSide" && keyAlternate == false) {

                    WalkLeft(hit);

                    keyAlternate = true;
                    return;

                }

                if (hit.transform.tag == "RightSide" && keyAlternate == true) {

                    WalkRight(hit);

                    keyAlternate = false;
                    return;

                }

            }
        }

#endif

    }
}
