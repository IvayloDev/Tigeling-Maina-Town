using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BonusLevelsButtonController : MonoBehaviour {

    private PlayerMovement playerScript;
    public GameObject splashImage;


    void Awake() {

        StartCoroutine(TwoSecondsSplash());

    }

    IEnumerator TwoSecondsSplash() {

        splashImage.SetActive(true);

        yield return new WaitForSeconds(2f);

        splashImage.SetActive(false);
    }

    public void OnExitBonusLevelClick() {

        SceneManager.LoadScene(1);

    }

}
