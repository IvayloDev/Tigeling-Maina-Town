using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour {

    public static float hitCountdown;
    private Vector3 StefchoSpawnPoint;
    private Vector3 statueSpawnPoint;

    public Animator GustoMainaPopUp;

    public static bool bonusLevelActive1, bonusLevelActive2;

    bool rwd2, rwd3, rwd4;

    public Sprite x100Sprite, x10Sprite;

    public cameraShake camShake;

    //static CollisionManager instance;

    void Update() {

        if (hitCountdown > 0) {
            hitCountdown -= Time.deltaTime;
        }

    }

    void Start() {

        //if (instance != null) {
        //    Destroy(this.gameObject);
        //    return;
        //}

        //instance = this;



    }

    void Give10Likes(Collider2D col) {

        Destroy(col.gameObject);
        PlayerMovement.Score += 10;

        FindObjectOfType<PlayerMovement>().ShowScorePopUp(x10Sprite);

    }


    void OnTriggerEnter2D(Collider2D col) {

        rwd2 = true;

        if (col.tag == "Reward2") {

            Give10Likes(col);


            if (rwd2) {
                rwd3 = true;
            }

        }

        if (col.tag == "Reward3") {

            Give10Likes(col);


            if (rwd3) {
                rwd4 = true;
            }

        }
        if (col.tag == "Reward4") {

            Give10Likes(col);

            if (rwd4) {
                rwd2 = false;
                rwd3 = false;
                rwd4 = false;
                PlayerMovement.Score += 70;

                FindObjectOfType<PlayerMovement>().ShowScorePopUp(x100Sprite);
                GustoMainaPopUp.SetTrigger("play");

            }

        }



        //if tag is ().. Flash screen and slow player
        if (col.tag == "Reward") {

            Give10Likes(col);

        }
        if (col.tag == "Check1") {

            // Do stuff for round 1

            AddRound();

            bonusLevelActive1 = true;
            bonusLevelActive2 = false;

            SceneManager.LoadScene(2);



        }

        if (col.tag == "Check2") {

            AddRound();

            SceneManager.LoadScene(3);

            bonusLevelActive1 = false;
            bonusLevelActive2 = true;


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

        if (col.tag == "Final") {

            SceneManager.LoadScene(4);

            if (StefchoScript.stefchoFinished) {

                //failed to outrun statue.
                Debug.LogError("failed to outrun stefcho");
            } else {

                Debug.LogError("succeeded in outrunning stefcho");

                //success

            }


        }

        if (col.tag == "Enemy") {

            camShake.shake_intensity = 0.3f;
            camShake.Shake();
        }


        if (col.tag == "StatueCheckPoint") {

            statueSpawnPoint = new Vector3(Camera.main.transform.position.x - 10, -2.4f, -6);

            GameObject statue = Instantiate(Resources.Load("Statue"),
            statueSpawnPoint,
            Quaternion.identity) as GameObject;

        }

        if (col.tag == "StefchoCheckPoint") {

            StefchoSpawnPoint = new Vector3(Camera.main.transform.position.x + 15, -2.4f, -6);

            GameObject stefcho = Instantiate(Resources.Load("Stefcho"),
            StefchoSpawnPoint,
            Quaternion.identity) as GameObject;

        }

    }

    void OnTriggerStay2D(Collider2D col) {

        if (col.tag == "Enemy") {

            //flash screen and slow down player
            hitCountdown = 15;

        }
    }

    void AddRound() {
        PlayerMovement.Round++;
    }

}
