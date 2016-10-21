using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class EndScreenScript : MonoBehaviour {


    public Text LikesText;
    public Text HeadJumpsText;
    public Text GustoMainasText;
    public Text TotalScoreText;


    public static int _FinalLikes = 0;
    public static int _FinalHeadJumps = 0;
    public static int _FinalGustoMainas = 0;
    private int _TotalScore = 0;

    private bool isPressed = true;

    private string DataPath = Application.dataPath + "GAME STATISTICS !!.txt";

    private string endTime = System.DateTime.Now.ToString();

    public static string startTime;

    private string FinalStringHolder;

    TextWriter txtWriter;

    void SetupStatistics() {

        txtWriter = new StreamWriter(DataPath, true);

        FinalStringHolder = startTime + ";" + endTime;

    }

    void Start() {

        SetupStatistics();

        _TotalScore = _FinalLikes + _FinalHeadJumps + _FinalGustoMainas;

        LikesText.text = _FinalLikes.ToString();
        HeadJumpsText.text = _FinalHeadJumps.ToString();
        GustoMainasText.text = _FinalGustoMainas.ToString();
        TotalScoreText.text = _TotalScore.ToString();

    }

    void Update() {

        if (Input.GetAxis("GO") > 0 || Input.GetAxis("Walk1") > 0 ||
            Input.GetAxis("Walk2") > 0 || Input.GetAxis("BonusButton1") > 0 || Input.GetAxis("BonusButton2") > 0) {

            if (isPressed) {

                isPressed = false;

                if (!PlayerMovement.timeOver5Mins) {
                    // Push results to file
                    txtWriter.WriteLine(FinalStringHolder);

                    txtWriter.Close();
                }

                SceneManager.LoadScene(0);
            }
        }
    }

    public void OnRestartClick() {

        // Push results to file
        if (!PlayerMovement.timeOver5Mins) {
            txtWriter.WriteLine(FinalStringHolder);

            txtWriter.Close();
        }
        SceneManager.LoadScene(0);

    }


}
