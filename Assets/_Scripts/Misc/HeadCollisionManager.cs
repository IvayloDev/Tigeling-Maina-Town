using UnityEngine;
using System.Collections;

public class HeadCollisionManager : MonoBehaviour {

    private bool canJump;
    public cameraShake camShake;


    void OnTriggerEnter2D(Collider2D col) {

        //CollisionManager.hitCountdown = 0;
        //camShake.shake_intensity = 0;

        if (col.tag == "Head" && canJump) {

            FindObjectOfType<PlayerMovement>().HeadJump(col);

        }

    }

    void OnTriggerStay2D(Collider2D col) {

        if (col.tag == "Enemy") {
            canJump = false;
        } else {
            canJump = true;
        }

    }



    ////Only for jump on enemy's head
    //void OnTriggerEnter2D(Collider2D col) {

    //    CollisionManager.hitCountdown = 0;
    //    camShake.shake_intensity = 0;

    //    if (col.tag == "FakeHead") {
    //        canJump = true;
    //    }

    //    if (col.tag == "Head" && !canJump) {

    //        canJump = false;
    //    }

    //    if (col.tag == "Head" && canJump) {

    //        FindObjectOfType<PlayerMovement>().HeadJump(col);
    //        canJump = false;
    //        return;
    //    }
    //}
}
