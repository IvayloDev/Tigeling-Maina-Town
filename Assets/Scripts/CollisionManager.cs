using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour {

    public static float hitCountdown;
    private Vector3 StefchoSpawnPoint;
    private Vector3 statueSpawnPoint;

    public Animator GustoMainaPopUp;

    public static bool bonusLevelActive1, bonusLevelActive2;

    public Sprite x100Sprite, x10Sprite;

    public cameraShake camShake;
    public Text BonusLevelIndexText;

    public Animator StartAnimation;

    public GameObject caughtAnimGO;
    public GameObject LikeAnimPrefab;

    public GameObject ArcadeJumpGO;


    void Update() {

        if (hitCountdown > 0) {
            hitCountdown -= Time.deltaTime;
        }

    }

    IEnumerator ArrowBoost() {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;

        CameraMovement.cameraMoveSpeed = 24;

        transform.position = new Vector2(transform.position.x, 0.5f);
        rb.velocity = new Vector2(25, 0);

        yield return new WaitForSeconds(0.3f);

        CameraMovement.cameraMoveSpeed = 5.2f;

        rb.gravityScale = 9;

    }

    void Give10Likes(Collider2D col) {

        PlayerMovement.Score += 10;
        AudioManager.instance.PlaySound("Like");

        GameObject LikeAnim = (GameObject)Instantiate(LikeAnimPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        LikeAnim.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Destroy(LikeAnim, 1f);
        FindObjectOfType<PlayerMovement>().ShowScorePopUp(x10Sprite);
        Destroy(col.gameObject);

        EndScreenScript._FinalLikes += 10;

    }

    IEnumerator LoadBonus(int level) {

        PlayerMovement.isDead = true;

        FindObjectOfType<PlayerMovement>().blueScreenFadeInAnim.SetBool("ActivateBlueIn", true);
        FindObjectOfType<PlayerMovement>().StartAnimation.SetTrigger("Transition");
        BonusLevelIndexText.text = (level - 1).ToString();

        yield return new WaitForSeconds(4.15f);

        SceneManager.LoadScene(level);

    }


    IEnumerator ShowJumpTut() {

        ArcadeJumpGO.SetActive(true);

        yield return new WaitForSeconds(1f);

        ArcadeJumpGO.SetActive(false);

    }


    void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "TriggerSpaceAnim") {

#if UNITY_STANDALONE

            StartCoroutine("ShowJumpTut");
            

#endif

#if UNITY_WEBGL

            StartCoroutine("ShowJumpTut");

            //FindObjectOfType<PlayerMovement>().StartAnimation.SetTrigger("ActivateSpace");

#endif

#if UNITY_ANDROID


#endif

#if UNITY_IOS


#endif

        }

        if (col.tag == "Reward2") {

            PlayerMovement.longJump = true;
            PlayerMovement.Score += 100;
            AudioManager.instance.PlaySound("100");
            FindObjectOfType<PlayerMovement>().ShowScorePopUp(x100Sprite);
            GustoMainaPopUp.SetTrigger("play");

            FindObjectOfType<PlayerMovement>().playerAnim.SetTrigger("GustoMaina");

            Destroy(col.gameObject);

            EndScreenScript._FinalGustoMainas += 100;
        }

        if (col.tag == "Reward3") {

            // Arrow hit, set Al Y to reward's Y, addforce x+

            StartCoroutine(ArrowBoost());

            PlayerMovement.Score += 100;
            AudioManager.instance.PlaySound("100");
            FindObjectOfType<PlayerMovement>().ShowScorePopUp(x100Sprite);
            GustoMainaPopUp.SetTrigger("play");

            FindObjectOfType<PlayerMovement>().playerAnim.SetTrigger("GustoMaina");

            Destroy(col.gameObject);

            EndScreenScript._FinalGustoMainas += 100;
        }

        //if tag is ().. Flash screen and slow player
        if (col.tag == "Reward") {

            Give10Likes(col);

        }
        if (col.tag == "Check1") {

            // Do stuff for round 1

            PlayerMovement.Round = 2;
            AudioManager.instance.PlaySound("Bonus1Enter");
            bonusLevelActive1 = true;
            bonusLevelActive2 = false;

            StartCoroutine(LoadBonus(2));

        }

        if (col.tag == "WhoWon1") {
            if (StefchoScript.stefchoFinished) {

                //failed to outrun statue.
                Debug.LogError("failed to outrun stefcho");

                StartAnimation.SetTrigger("ActivateFailure");

            } else {
                //success
                StartAnimation.SetTrigger("ActivateSuccess");

                Debug.LogError("succeeded in outrunning stefcho");

            }
        }

        if (col.tag == "WhoWon2") {

            if (StatueScript.statueFinished) {

                //failed to outrun statue.
                Debug.LogError("failed to outrun statue");
                StartAnimation.SetTrigger("ActivateFailure");

            } else {

                Debug.LogError("succeeded in outrunning statue");
                //success
                StartAnimation.SetTrigger("ActivateSuccess");

            }
        }

        if (col.tag == "Check2") {

            PlayerMovement.Round = 3;
            AudioManager.instance.PlaySound("Bonus2Enter");

            bonusLevelActive1 = false;
            bonusLevelActive2 = true;

            StartCoroutine(LoadBonus(3));

        }

        if (col.tag == "Final") {

            AudioManager.instance.PlaySound("End");

            SceneManager.LoadScene(4);

        }

        if (col.tag == "Enemy") {

            AudioManager.instance.PlaySound("Enemy");
            hitCountdown = 15;
            camShake.shake_intensity = 0.3f;
            camShake.Shake();
            StartCoroutine(IsCaught());
        }


        if (col.tag == "StatueCheckPoint") {

            statueSpawnPoint = new Vector3(Camera.main.transform.position.x - 10, -2.4f, -8);

            GameObject statue = Instantiate(Resources.Load("Statue"),
            statueSpawnPoint,
            Quaternion.identity) as GameObject;
        }

        if (col.tag == "StefchoCheckPoint") {

            StefchoSpawnPoint = new Vector3(Camera.main.transform.position.x + 15, -1.4f, -8);

            GameObject stefcho = Instantiate(Resources.Load("Stefcho"),
            StefchoSpawnPoint,
            Quaternion.identity) as GameObject;
        }
    }

    IEnumerator IsCaught() {
        caughtAnimGO.SetActive(true);

        yield return new WaitForSeconds(0.6f);

        caughtAnimGO.SetActive(false);
        hitCountdown = 0;

    }
}
