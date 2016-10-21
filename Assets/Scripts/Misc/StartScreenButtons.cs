using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreenButtons : MonoBehaviour {

    public Animator InstructionsPanelAnim;
    public Animator exitTransitionAnimation;
    public Animator blueScreenOverlayAnim;

    public static bool comingFromStartMenu;

    public GameObject HowToPlayButton;
    private bool isPressed = true;

    void Start() {

#if UNITY_STANDALONE

        HowToPlayButton.SetActive(false);
#endif

#if UNITY_WEBGL

        HowToPlayButton.SetActive(true);
#endif

    }


    public void OnPlay2GoClick() {

        StartCoroutine(WaitForAnimation());
        AudioManager.instance.PlaySound("ButtonClick");

        EndScreenScript.startTime = System.DateTime.Now.ToString();
    }

    void Update() {

        if (Input.GetAxis("GO") > 0 || Input.GetAxis("Walk1") > 0 ||
            Input.GetAxis("Walk2") > 0 || Input.GetAxis("BonusButton1") > 0 || Input.GetAxis("BonusButton2") > 0) {

            if (isPressed) {

                isPressed = false;
                StartCoroutine(WaitForAnimation());
                AudioManager.instance.PlaySound("ButtonClick");

                EndScreenScript.startTime = System.DateTime.Now.ToString();

            }
        }
    }

    public void OnArrowClick() {

        AudioManager.instance.PlaySound("ArrowClick");

    }

    void ResetGame() {
        PlayerMovement.Lives = 3;
        PlayerMovement.Score = 0;
        PlayerMovement.Round = 1;

        EndScreenScript._FinalHeadJumps = 0;
        EndScreenScript._FinalGustoMainas = 0;
        EndScreenScript._FinalLikes = 0;

        CollisionManager.bonusLevelActive1 = false;
        CollisionManager.bonusLevelActive2 = false;

        PlayerMovement.wasDead = false;
        PlayerMovement.FirstWalk = true;
    }

    IEnumerator WaitForAnimation() {

        blueScreenOverlayAnim.SetBool("ActivateBlueIn", true);
        exitTransitionAnimation.SetBool("Activate", true);

        AudioManager.instance.PlaySound("Round1");

        yield return new WaitForSeconds(2.8f);

        ResetGame();

        CollisionManager.bonusLevelActive2 = false;

        comingFromStartMenu = true;
        SceneManager.LoadScene(1);

    }

    public void OnHowToPlayClick() {

        InstructionsPanelAnim.SetBool("ShowPanel", true);
        AudioManager.instance.PlaySound("ButtonClick");

    }

    public void OnClosePanelClick() {
        InstructionsPanelAnim.SetBool("ShowPanel", false);
        AudioManager.instance.PlaySound("ButtonClick");

    }
}

