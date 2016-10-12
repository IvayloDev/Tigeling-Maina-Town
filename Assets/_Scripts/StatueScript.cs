using UnityEngine;
using System.Collections;

public class StatueScript : MonoBehaviour {

    //called whenever GameObject is activated. This means that if you 
    //failed the level you dont need to restart the game for this 
    //script to  be activated

    private float runSpeed;
    public static bool statueFinished;
    private Animator statueAnim;

    void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "WhoWon2") {
            statueFinished = true;
        }
    }


    void OnEnable() {
        //use this as start

        statueAnim = GetComponent<Animator>();

        statueFinished = false;

        StartCoroutine(WarningStatue());

        Debug.LogError("Statue Enabled");

        StartCoroutine(startRunning(runSpeed));
        StartCoroutine(Audio());
        //Start running fast. when in frame slow down. dont interact with player
        // if go with tag stefcho wins- say fail and lives--
    }

    IEnumerator Audio() {

        yield return new WaitForSeconds(0.3f);

        AudioManager.instance.PlaySound("Statue");
    }

    void Update() {

        statueAnim.speed = runSpeed / 4;

        if (FindObjectsOfType<StatueScript>().Length > 1) {
            Destroy(FindObjectsOfType<StatueScript>()[0].gameObject);
        }

        if (this.isActiveAndEnabled) {
            transform.Translate((Vector3.right * runSpeed) * Time.deltaTime);
        }

    }

    IEnumerator WarningStatue() {

        yield return new WaitForSeconds(0.3f);

        FindObjectOfType<PlayerMovement>().StartAnimation.SetTrigger("WarningStatue");
    }

    IEnumerator startRunning(float _runningSpeed) {

        runSpeed = 8;

        yield return new WaitForSeconds(2.6f);

        runSpeed = Random.Range(5f, 5.8f);

        yield return new WaitForSeconds(2f);

        runSpeed = Random.Range(4.2f, 6.6f);

        yield return new WaitForSeconds(2f);

        runSpeed = Random.Range(5.2f, 5.3f);

        yield return new WaitForSeconds(3f);

        runSpeed = Random.Range(5.2f, 5.9f);

    }

}
