using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIScript : MonoBehaviour {

    public Text RoundText, ScoreText;
    public GameObject[] LivesArray;

    //static UIScript instance;


    void Start() {

        //if (instance != null) {
        //    Destroy(gameObject);
        //    return;
        //}

        //instance = this;
        //DontDestroyOnLoad(gameObject);

        for (int i = 0; i < PlayerMovement.Lives; i++) {
            LivesArray[i].SetActive(true);
        }
    }

    void Update() {

        RoundText.text = "Round: " + PlayerMovement.Round;

        ScoreText.text = PlayerMovement.Score.ToString();

    }
}
