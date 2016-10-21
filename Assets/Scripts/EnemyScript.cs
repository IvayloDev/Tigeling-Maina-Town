using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {


    private int randomInt;

    void Start() {

        randomInt = Random.Range(0, 2);

        switch (randomInt) {
            case 0:
                GetComponent<Animator>().SetBool("animation0", true);
                break;
            case 1:
                GetComponent<Animator>().SetBool("animation1", true);
                break;
        }
    }

}
