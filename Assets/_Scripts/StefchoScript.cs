using UnityEngine;
using System.Collections;

public class StefchoScript : MonoBehaviour {

    //called whenever GameObject is activated. This means that if you 
    //failed the level you dont need to restart the game for this 
    //script to  be activated

    private float runSpeed;

    public static bool stefchoFinished;

    void OnEnable() {
        //use this as start
        stefchoFinished = false;

        Debug.LogError("Stefcho Enabled");

        StartCoroutine(startRunning(runSpeed));
    }

    void Update() {

        if (FindObjectsOfType<StefchoScript>().Length > 1) {
            Destroy(FindObjectsOfType<StefchoScript>()[0].gameObject);
        }

        if (this.isActiveAndEnabled) {
            transform.Translate((Vector3.right * runSpeed) * Time.deltaTime);
        }

    }

    IEnumerator startRunning(float _runningSpeed) {

        runSpeed = Random.Range(2, 3);

        yield return new WaitForSeconds(4.5f);

        runSpeed = Random.Range(3, 3.5f);

        yield return new WaitForSeconds(3f);

        runSpeed = Random.Range(7.5f, 8.5f);

        yield return new WaitForSeconds(5f);

        runSpeed = Random.Range(2, 3);

        yield return new WaitForSeconds(4f);

        runSpeed = Random.Range(6, 8);

        yield return new WaitForSeconds(2f);

        runSpeed = Random.Range(5.2f, 5.4f);

    }


    void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "Player") {
            CollisionManager.hitCountdown = 0.3f;
        }
        if (col.tag == "Check4") {

            stefchoFinished = true;

            DestroyObject(this.gameObject, 2f);

        }

    }

}
