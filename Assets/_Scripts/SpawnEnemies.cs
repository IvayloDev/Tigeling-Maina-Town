using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {


    public Vector3 FirstCheckpoint, SecondCheckpoint, ThirdCheckpoint, ForthCheckpoint;

    public Vector3 startSpawnPoint;

    void Start() {

        //method --> submethods for different enemies

        SpawnRewardLevel0();
        SpawnEnemyLevel0();

        SpawnRewardLevel1();

        SpawnRewardLevel2();
        SpawnEnemyLevel2();

        SpawnRewardLevel3();
        SpawnEnemyLevel3();

        SpawnRewardLevel4();
        SpawnEnemyLevel4();


    }

    void SpawnEnemyLevel0() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(16, 20);

        GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
            targetVector,
            Quaternion.identity) as GameObject;

    }
    void SpawnRewardLevel0() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(21, 27);

        for (int i = 0; i < Random.Range(3, 6); i++) {

            targetVector.x += 3;

            GameObject myEnemy = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
        }
    }

    void SpawnRewardLevel1() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(90, 100);

        for (int i = 0; i < Random.Range(3, 6); i++) {

            targetVector.x += 4;
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
        }
    }

    void SpawnRewardLevel2() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(170, 175);

        for (int i = 0; i < Random.Range(3, 5); i++) {

            targetVector.x += 3;
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
        }

        Vector3 targetVector2 = startSpawnPoint;

        targetVector2.x = Random.Range(220, 230);

        for (int i = 0; i < Random.Range(4, 6); i++) {

            targetVector2.x += 3;
            startSpawnPoint = targetVector;

            GameObject myEnemy2 = Instantiate(Resources.Load("Reward"),
                targetVector2,
                Quaternion.identity) as GameObject;
        }
    }
    void SpawnEnemyLevel2() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(185, 195);

        for (int i = 0; i < Random.Range(3, 5); i++) {

            targetVector.x += 9;
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
                targetVector,
                Quaternion.identity) as GameObject;
        }

    }

    void SpawnRewardLevel3() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(270, 285);

        for (int i = 0; i < Random.Range(3, 5); i++) {

            targetVector.x += 4f;
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
        }


        Vector3 targetVector2 = startSpawnPoint;

        targetVector2.x = Random.Range(330, 335);

        for (int i = 0; i < Random.Range(2, 4); i++) {

            targetVector2.x += 2f;
            startSpawnPoint = targetVector2;

            GameObject myEnemy = Instantiate(Resources.Load("Reward"),
                targetVector2,
                Quaternion.identity) as GameObject;
        }

    }
    void SpawnEnemyLevel3() {


        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(300, 310);


        for (int i = 0; i < Random.Range(2, 4); i++) {

            targetVector.x += 8;
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
                targetVector,
                Quaternion.identity) as GameObject;
        }
    }

    void SpawnEnemyLevel4() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(375, 380);

        for (int i = 0; i < Random.Range(3, 6); i++) {

            targetVector.x += 7;
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
                targetVector,
                Quaternion.identity) as GameObject;
        }
    }
    void SpawnRewardLevel4() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(395, 405);

        for (int i = 0; i < Random.Range(4, 8); i++) {

            targetVector.x += 1.5f;
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
        }
    }

}
