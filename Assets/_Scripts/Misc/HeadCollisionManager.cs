using UnityEngine;
using System.Collections;

public class HeadCollisionManager : MonoBehaviour {

    private bool allowJump;
    public cameraShake camShake;


    //Only for jump on enemy's head
    void OnTriggerEnter2D(Collider2D col) {


        CollisionManager.hitCountdown = 0;
        camShake.shake_intensity = 0;

        if (col.tag == "FakeHead") {
            allowJump = true;
        }

        if (col.tag == "Head" && !allowJump) {

            allowJump = false;
        }

        if (col.tag == "Head" && allowJump) {

            FindObjectOfType<PlayerMovement>().HeadJump(col);
            allowJump = false;
            return;
        }


    }
}
