using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreenButtons : MonoBehaviour {

    public Animator InstructionsPanelAnim;
    public Animator exitTransitionAnimation;
    public Animator blueScreenOverlayAnim;

    public static bool comingFromStartMenu;

    public void OnPlay2GoClick() {

        StartCoroutine(WaitForAnimation());
        AudioManager.instance.PlaySound("ButtonClick");

    }

    void Update() {

        if (Input.GetAxis("GO") > 0) {

            StartCoroutine(WaitForAnimation());
            AudioManager.instance.PlaySound("ButtonClick");
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

