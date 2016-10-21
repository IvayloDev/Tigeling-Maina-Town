using UnityEngine;
using System.Collections;

public class StefchoScript : MonoBehaviour {

    //called whenever GameObject is activated. This means that if you 
    //failed the level you dont need to restart the game for this 
    //script to  be activated

    private float runSpeed;
    public static bool stefchoFinished;
    private Animator stefchoAnim;

    IEnumerator WarningStefcho() {

        yield return new WaitForSeconds(2);

        FindObjectOfType<PlayerMovement>().StartAnimation.SetTrigger("WarningStefcho");
    }

    void OnEnable() {
        //use this as start

        StartCoroutine(WarningStefcho());

        stefchoAnim = GetComponent<Animator>();

        stefchoFinished = false;

        Debug.LogError("Stefcho Enabled");

        StartCoroutine(startRunning(runSpeed));
        StartCoroutine(Audio());
    }

    IEnumerator Audio() {

        yield return new WaitForSeconds(1.3f);

        AudioManager.instance.PlaySound("Stefcho");

    }
    void Update() {

        stefchoAnim.speed = runSpeed;

        if (FindObjectsOfType<StefchoScript>().Length > 1) {
            Destroy(FindObjectsOfType<StefchoScript>()[0].gameObject);
        }

        if (this.isActiveAndEnabled) {
            transform.Translate((Vector3.right * runSpeed) * Time.deltaTime);
        }

    }

    IEnumerator startRunning(float _runningSpeed) {

        runSpeed = Random.Range(3, 4);

        yield return new WaitForSeconds(4.5f);

        runSpeed = Random.Range(3, 3.5f);

        yield return new WaitForSeconds(2f);

        runSpeed = Random.Range(7.5f, 8.5f);

        yield return new WaitForSeconds(2f);

        runSpeed = Random.Range(5, 6);

        yield return new WaitForSeconds(2f);

        runSpeed = Random.Range(5, 7);

        yield return new WaitForSeconds(1f);

        runSpeed = Random.Range(5.2f, 6.4f);

    }


    void OnTriggerEnter2D(Collider2D col) {

        if (col.tag == "Player") {
            GetComponent<Animator>().SetTrigger("Millions");
        }

        if (col.tag == "WhoWon1") {
            stefchoFinished = true;
        }

    }

}
