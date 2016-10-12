using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusLevelOneController : MonoBehaviour {


    private string str;
    public Text textField;
    private bool keyAlternate;
    int i = 0;
    private string sourceText = "ЦЯЛАТА ХАВА\nЕ\nЗА\nГУСТОТО МАЙНА";
    public Image timerBar;
    private float time;
    private float startTime;
    private bool startTimer;

    public Animator StartAnimation;
    public Animator fadeInBlue;

    void Start() {

#if UNITY_STANDALONE

        time = 3;
        startTime = 3;

#endif

#if UNITY_WEBGL

        time = 3;
        startTime = 3;
#endif

#if UNITY_ANDROID

        time = 4;
        startTime = 4;

#endif

#if UNITY_IOS

        time = 4;
        startTime = 4;

#endif


        str = "";
        StartCoroutine(AutoStartTimer());
        StartAnimation.SetTrigger("ActivateBonus");
    }

    IEnumerator AutoStartTimer() {

        yield return new WaitForSeconds(2f);

        startTimer = true;

        AudioManager.instance.PlaySound("Timer");

    }

    void CheckForInput() {

        if (i >= sourceText.Length || time <= 0 || !startTimer) {
            return;
        }

#if UNITY_STANDALONE


        if (Input.GetAxis("Walk1") > 0 && keyAlternate == false) {


            str += sourceText[i++];
            keyAlternate = true;


        } else if (Input.GetAxis("Walk2") > 0 && keyAlternate == true) {

            str += sourceText[i++];
            keyAlternate = false;
        }

#endif

#if UNITY_WEBGL


        if (Input.GetKeyDown(KeyCode.G) && keyAlternate == false) {


            str += sourceText[i++];
            keyAlternate = true;


        } else if (Input.GetKeyDown(KeyCode.H) && keyAlternate == true) {

            str += sourceText[i++];
            keyAlternate = false;

        }

#endif


#if UNITY_ANDROID

        if (Input.GetMouseButtonDown(0)) {

            str += sourceText[i++];

        }


#endif

#if UNITY_IOS

        if (Input.GetMouseButtonDown(0)) {

            str += sourceText[i++];

        }
#endif

    }

    void HandleResult() {

        if (!startTimer) {
            return;
        }

        if (i >= sourceText.Length && time >= 0) {

            startTimer = false;

            StartCoroutine(Won());


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

        textField.text = str;

        timerBar.fillAmount = time / startTime;

        if (startTimer) {

            time -= Time.deltaTime;
        }

        CheckForInput();

        HandleResult();


    }
}
