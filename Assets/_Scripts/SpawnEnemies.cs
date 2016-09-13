using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {


    public Vector3 startSpawnPoint;

    [SerializeField]
    private GameObject rewardsHolder, enemiesHolder;

    void Start() {

        //method --> submethods for different enemies

        SpawnRewardLevel0();
        SpawnEnemyLevel0();

        SpawnRewardLevel1();

        SpawnRewardLevel2();
        SpawnEnemyLevel2();

        SpawnRewardLevel3();
        SpawnEnemyLevel3();


    }

    void SpawnEnemyLevel0() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(16, 20);

        GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
            targetVector,
            Quaternion.identity) as GameObject;
        myEnemy.transform.parent = enemiesHolder.transform;


        // ADDED FOR TESTING ONLY !
        for (int i = 0; i < Random.Range(5, 15); i++) {

            targetVector.x += 9;
            startSpawnPoint = targetVector;

            GameObject myEnemy2 = Instantiate(Resources.Load("Enemy"),
                targetVector,
                Quaternion.identity) as GameObject;
            myEnemy2.transform.parent = enemiesHolder.transform;

        }
    }
    void SpawnRewardLevel0() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(21, 27);

        for (int i = 0; i < Random.Range(3, 6); i++) {

            targetVector.x += 3;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }

        targetVector.x += 10;

        GameObject myRewardTriangle = Instantiate(Resources.Load("Reward2"),
                targetVector,
                Quaternion.identity) as GameObject;
        myRewardTriangle.transform.parent = rewardsHolder.transform;

    }

    void SpawnRewardLevel1() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(90, 100);

        for (int i = 0; i < Random.Range(3, 6); i++) {

            targetVector.x += 4;
            startSpawnPoint = targetVector;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }
    }

    void SpawnRewardLevel2() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(170, 175);

        for (int i = 0; i < Random.Range(3, 5); i++) {

            targetVector.x += 3;
            startSpawnPoint = targetVector;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }

        Vector3 targetVector2 = startSpawnPoint;

        targetVector2.x = Random.Range(220, 230);

        for (int i = 0; i < Random.Range(4, 6); i++) {

            targetVector2.x += 3;
            startSpawnPoint = targetVector;

            GameObject myReward2 = Instantiate(Resources.Load("Reward"),
                targetVector2,
                Quaternion.identity) as GameObject;
            myReward2.transform.parent = rewardsHolder.transform;
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
            myEnemy.transform.parent = enemiesHolder.transform;

        }

    }

    void SpawnRewardLevel3() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(270, 285);

        for (int i = 0; i < Random.Range(3, 5); i++) {

            targetVector.x += 4f;
            startSpawnPoint = targetVector;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }


        Vector3 targetVector2 = startSpawnPoint;

        targetVector2.x = Random.Range(330, 335);

        for (int i = 0; i < Random.Range(2, 4); i++) {

            targetVector2.x += 2f;
            startSpawnPoint = targetVector2;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                targetVector2,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
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
            myEnemy.transform.parent = enemiesHolder.transform;

        }
    }

}
