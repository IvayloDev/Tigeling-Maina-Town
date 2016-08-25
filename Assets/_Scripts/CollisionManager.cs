using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {


    public static float hitCountdown;
    private Vector3 StefchoSpawnPoint;
    private Vector3 statueSpawnPoint;

    public static bool bonusLevelActive = false;

    public static bool captured;

    public cameraShake camShake;

    [SerializeField]
    private PlayerMovement playerMovement;

    void Update() {

        if (hitCountdown > 0) {
            hitCountdown -= Time.deltaTime;
        }

        if (captured) {

            if (Input.GetKeyDown(KeyCode.Space)) {
                hitCountdown = 0;
                captured = false;
            }
        }

    }

    void OnTriggerStay2D(Collider2D col) {

        if (col.tag == "Reward2") {

            PlayerMovement.Score += 10;
            Destroy(col.gameObject, 0.3f);

            if (Input.GetKeyDown(KeyCode.Space) || PlayerMovement.mobileJump) {
                Debug.Log("logged");

                playerMovement.timeTakenDuringLerp = 0.3f;
                playerMovement.StartLerping(new Vector3(0.5f, 0.5f, 0), 5f);
                PlayerMovement.mobileJump = false;

            }


        }

    }

    void OnTriggerEnter2D(Collider2D col) {

        //if tag is ().. Flash screen and slow player
        if (col.tag == "Reward") {

            Destroy(col.gameObject);
            PlayerMovement.Score += 10;

        }
        if (col.tag == "Check1") {

            // Do stuff for round 1
            AddRound();
            //bonusLevelActive = true;
        }

        if (col.tag == "Check2") {
            AddRound();
            //bonusLevelActive = true;

            if (StatueScript.statueFinished) {

                //failed to outrun statue.
                Debug.LogError("failed to outrun statue");
            } else {

                Debug.LogError("succeeded in outrunning statue");

                //success

            }

            //show bonus level + fail success screen
            // Do stuff for round 2

        }

        if (col.tag == "Check3") {
            AddRound();
            //bonusLevelActive = true;

        }

        if (col.tag == "Check4") {
            AddRound();
            //bonusLevelActive = true;

            if (StefchoScript.stefchoFinished) {

                //failed to outrun statue.
                Debug.LogError("failed to outrun stefcho");
            } else {

                Debug.LogError("succeeded in outrunning stefcho");

                //success

            }

        }


        if (col.tag == "Enemy") {
            //flash screen and slow down player
            captured = true;
            camShake.shake_intensity = 0.3f;
            camShake.Shake();
            hitCountdown = 15;
            return;



        }


        if (col.tag == "StatueCheckPoint") {

            statueSpawnPoint = new Vector3(Camera.main.transform.position.x - 10, -1.5f);

            GameObject statue = Instantiate(Resources.Load("Statue"),
           statueSpawnPoint,
           Quaternion.identity) as GameObject;

        }

        if (col.tag == "StefchoCheckPoint") {

            StefchoSpawnPoint = new Vector3(Camera.main.transform.position.x + 15, -1, 5f);

            GameObject stefcho = Instantiate(Resources.Load("Stefcho"),
           StefchoSpawnPoint,
           Quaternion.identity) as GameObject;

        }

    }



    void AddRound() {
        PlayerMovement.Round++;
    }
    //else if collectable =] add points

}
