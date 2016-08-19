using UnityEngine;
using System.Collections;

public class StatueScript : MonoBehaviour {

    //called whenever GameObject is activated. This means that if you 
    //failed the level you dont need to restart the game for this 
    //script to  be activated

    private float runSpeed;
    public static bool statueFinished;


    void OnEnable() {
        //use this as start
        statueFinished = false;

        Debug.LogError("Statue Enabled");

        StartCoroutine(startRunning(runSpeed));

        //Start running fast. when in frame slow down. dont interact with player
        // if go with tag stefcho wins- say fail and lives--
    }

    void Update() {

        if (this.isActiveAndEnabled) {
            transform.Translate((Vector3.right * runSpeed) * Time.deltaTime);
        }

    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "Check2") {

            statueFinished = true;

            DestroyObject(this.gameObject, 2f);

        }

    }

    IEnumerator startRunning(float _runningSpeed) {


        runSpeed = 8;

        yield return new WaitForSeconds(2.6f);

        runSpeed = Random.Range(5f, 5.8f);

        yield return new WaitForSeconds(2f);

        runSpeed = Random.Range(4.2f, 6.6f);

        yield return new WaitForSeconds(2f);

        runSpeed = Random.Range(5.2f, 5.3f);

    }

}
