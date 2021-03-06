using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusLevelTwoController : MonoBehaviour {

    public GameObject[] SquareMaskArray;
    private bool keyAlternate;
    int i = 0;
    public Image timerBar;
    private float time;
    private float startTime;
    private bool startTimer;

    public GameObject ArcadeGameObject;

    public Animator StartAnimation;
    public Animator fadeInBlue;

    IEnumerator StartScreenArcade() {

        yield return new WaitForSeconds(0.4f);

        ArcadeGameObject.SetActive(true);

        yield return new WaitForSeconds(1.45f);

        ArcadeGameObject.SetActive(false);

    }

    void Start() {

#if UNITY_STANDALONE

        StartCoroutine("StartScreenArcade");

        time = 2;
        startTime = 2;

#endif

#if UNITY_WEBGL

        //StartAnimation.SetTrigger("Activate2");
        StartCoroutine("StartScreenArcade");

        time = 2;
        startTime = 2;
#endif

#if UNITY_ANDROID

        time = 2.2f;
        startTime = 2.2f;

#endif

#if UNITY_IOS

        time = 2.2f;
        startTime = 2.2f;

#endif
        StartCoroutine(AutoStartTimer());
        AudioManager.instance.PlaySound("BackgroundMusic");

    }

    IEnumerator AutoStartTimer() {

        yield return new WaitForSeconds(2f);

        startTimer = true;
        AudioManager.instance.PlaySound("Timer");

    }

    void CheckForInput() {

        if (i >= SquareMaskArray.Length || time <= 0 || !startTimer) {
            return;
        }

#if UNITY_STANDALONE


        if (Input.GetAxis("BonusButton1") > 0 && Input.GetAxis("BonusButton2") == 0 && keyAlternate == false) {


            SquareMaskArray[i++].SetActive(false);

            keyAlternate = true;


        } else if (Input.GetAxis("BonusButton2") > 0 && Input.GetAxis("BonusButton1") == 0 && keyAlternate == true) {

            SquareMaskArray[i++].SetActive(false);

            keyAlternate = false;
        }

#endif

#if UNITY_WEBGL


        if (Input.GetKeyDown(KeyCode.G) && keyAlternate == false) {


            SquareMaskArray[i++].SetActive(false);

            keyAlternate = true;


        } else if (Input.GetKeyDown(KeyCode.H) && keyAlternate == true) {

            SquareMaskArray[i++].SetActive(false);

            keyAlternate = false;

        }

#endif


#if UNITY_ANDROID

        if (Input.GetMouseButtonDown(0)) {

            SquareMaskArray[i++].SetActive(false);
        }


#endif

#if UNITY_IOS

          if (Input.GetMouseButtonDown(0)) {

            SquareMaskArray[i++].SetActive(false);
        }

#endif

    }

    void HandleResult() {

        if (!startTimer) {
            return;
        }

        if (i >= SquareMaskArray.Length && time >= 0) {

            StartCoroutine(Won());

            startTimer = false;

        } else if (time <= 0) {

            startTimer = false;

            StartCoroutine(Lost());
        }

    }

    IEnumerator Lost() {

        StartAnimation.SetTrigger("ActivateFailure");
        fadeInBlue.SetTrigger("ActivateBlueIn");
        AudioManager.instance.PlaySound("Failure");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(1);
    }

    IEnumerator Won() {

        StartAnimation.SetTrigger("ActivateSuccess");
        fadeInBlue.SetTrigger("ActivateBlueIn");
        AudioManager.instance.PlaySound("Great");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(1);
    }


    void Update() {

        timerBar.fillAmount = time / startTime;

        if (startTimer) {

            time -= Time.deltaTime;
        }

        CheckForInput();

        HandleResult();

    }




}
