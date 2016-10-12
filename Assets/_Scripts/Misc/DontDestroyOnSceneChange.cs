using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DontDestroyOnSceneChange : MonoBehaviour {

    private static DontDestroyOnSceneChange instance = null;

    void Start() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update() {

        if (SceneManager.GetActiveScene().buildIndex == 2 ||
            SceneManager.GetActiveScene().buildIndex == 3) {

            GetComponent<AudioSource>().mute = true;

        } else {
            GetComponent<AudioSource>().mute = false;
        }

    }

}
