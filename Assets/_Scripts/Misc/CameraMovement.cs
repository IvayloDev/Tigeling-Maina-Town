using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private Camera cam;

    private float cameraMoveSpeed;

    public GameObject Player;

    private Vector3 PlayerScreenPos, StartPos;

    public static bool maxReached;

    void CheckingPosition() {

        PlayerScreenPos = cam.WorldToScreenPoint(Player.transform.position);

        //Cannot run after this(limit max X position)
        if (PlayerScreenPos.x > Screen.width / 1.5f) {
            maxReached = true;

        } else if (PlayerScreenPos.x < Screen.width / 1.5f) {
            maxReached = false;
        }
    }

    void Awake() {

        cam = GetComponent<Camera>();

    }

    void Start() {

        cameraMoveSpeed = 5.2f;

        SettingInitialPos();

    }

    void SettingInitialPos() {

        PlayerScreenPos = cam.WorldToScreenPoint(Player.transform.position);

        StartPos = PlayerScreenPos;

        StartPos.x = Screen.width / 4;

        Player.transform.position = cam.ScreenToWorldPoint(StartPos);

    }

    void TranslateCamera() {

        cam.transform.Translate((Vector3.right * cameraMoveSpeed) * Time.deltaTime);

    }

    void Update() {


        CheckingPosition();


        if (!PlayerMovement.isDead && PlayerMovement.startMoving) {

            TranslateCamera();
        }
    }
}
