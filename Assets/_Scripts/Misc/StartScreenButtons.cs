using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScreenButtons : MonoBehaviour {

    public GameObject TestPanelHolder;

    public void OnPlay2GoClick() {

        PlayerMovement.Lives = 3;
        CollisionManager.bonusLevelActive1 = false;
        CollisionManager.bonusLevelActive2 = false;

        SceneManager.LoadScene(1);
    }

    public void OnHowToPlayClick() {

        TestPanelHolder.SetActive(!TestPanelHolder.activeSelf);
        // Show some sort of a menu or instructions panel ??

    }

}

