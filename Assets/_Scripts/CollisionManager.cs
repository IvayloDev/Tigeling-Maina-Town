using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {


    public static float hitCountdown;
    private Vector3 StefchoSpawnPoint;
    private Vector3 statueSpawnPoint;

    public static bool bonusLevelActive = false;


    void Update() {
        if (hitCountdown > 0) {
            hitCountdown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {

        //if tag is ().. Flash screen and slow player
        if (col.tag == "Reward") {

            Destroy(col.gameObject);
            PlayerMovement.Score += Random.Range(5, 30);

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
            hitCountdown = 0.3f;
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
