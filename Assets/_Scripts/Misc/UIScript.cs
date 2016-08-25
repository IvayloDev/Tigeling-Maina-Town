using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIScript : MonoBehaviour {

    public Text RoundText, ScoreText;

    void Start() {

    }

    void Update() {

        RoundText.text = "Round: " + PlayerMovement.Round;

        ScoreText.text = "Score: " + PlayerMovement.Score;

    }
}
