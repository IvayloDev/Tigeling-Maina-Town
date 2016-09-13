using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour {

    public void OnRestartClick() {
        SceneManager.LoadScene(0);
    }
}
