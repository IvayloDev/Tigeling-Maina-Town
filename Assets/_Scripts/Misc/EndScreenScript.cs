using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour {


    public Text LikesText;
    public Text HeadJumpsText;
    public Text GustoMainasText;
    public Text TotalScoreText;


    public static int _FinalLikes = 0;
    public static int _FinalHeadJumps = 0;
    public static int _FinalGustoMainas = 0;
    private int _TotalScore = 0;


    void Start() {

        _TotalScore = _FinalLikes + _FinalHeadJumps + _FinalGustoMainas;

        LikesText.text = _FinalLikes.ToString();
        HeadJumpsText.text = _FinalHeadJumps.ToString();
        GustoMainasText.text = _FinalGustoMainas.ToString();
        TotalScoreText.text = _TotalScore.ToString();

    }

    void Update() {

        if (Input.GetAxis("GO") > 0) {
            SceneManager.LoadScene(0);
        }

    }

    public void OnRestartClick() {
        SceneManager.LoadScene(0);
    }


}
