using UnityEngine;
using System.Collections;

public class cameraShake : MonoBehaviour {


    Vector3 originPosition;

    float shake_decay = 0.1f;
    public float shake_intensity;
    float coef_shake_intensity = 0.4f;

    void Update() {
        if (shake_intensity > 0) {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            shake_intensity -= shake_decay;
        } else {
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 0, -10);
        }
    }

    public void Shake() {
        originPosition = transform.position;
        shake_intensity = coef_shake_intensity;
    }
}
