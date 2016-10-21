using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {


    public Vector3 startSpawnPoint;

    [SerializeField]
    private GameObject rewardsHolder, enemiesHolder;

    void Start() {

        //method --> submethods for different enemies

        SpawnEnemyLevel0();
        SpawnRewardLevel0();

        SpawnRewardLevel1();
        SpawnEnemyLevel1();

        SpawnRewardLevel2();
        SpawnEnemyLevel2();

    }

    void SpawnEnemyLevel0() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(30, 30);

        GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
            targetVector,
            Quaternion.identity) as GameObject;
        myEnemy.transform.parent = enemiesHolder.transform;


        targetVector.x += 15;


        for (int i = 0; i < Random.Range(2, 4); i++) {

            targetVector.x += 6;

            GameObject myEnemy2 = Instantiate(Resources.Load("Enemy"),
    targetVector,
    Quaternion.identity) as GameObject;
            myEnemy2.transform.parent = enemiesHolder.transform;

        }

        targetVector.x += 10;


        for (int i = 0; i < Random.Range(2, 4); i++) {

            targetVector.x += 2;

            GameObject myEnemy2 = Instantiate(Resources.Load("Enemy"),
    targetVector,
    Quaternion.identity) as GameObject;
            myEnemy2.transform.parent = enemiesHolder.transform;

        }

    }

    void SpawnRewardLevel0() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(14, 14);

        for (int i = 0; i < Random.Range(3, 6); i++) {

            targetVector.x += 2;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }

        targetVector.x += 14;


        for (int i = 0; i < Random.Range(4, 7); i++) {

            targetVector.x += 5;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                new Vector3(targetVector.x, 0.5f),
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }

        targetVector.x += 26;

        for (int i = 0; i < Random.Range(3, 4); i++) {

            targetVector.x += 1;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }

    }

    void SpawnRewardLevel1() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(115, 125);

        for (int i = 0; i < Random.Range(2, 4); i++) {

            targetVector.x += 4;
            startSpawnPoint = targetVector;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }

        targetVector.x += 8;

        GameObject myReward2 = Instantiate(Resources.Load("RewardBoost"),
              targetVector,
              Quaternion.identity) as GameObject;
        myReward2.transform.parent = rewardsHolder.transform;

        targetVector.x += 15;

        for (int i = 0; i < Random.Range(2, 6); i++) {

            targetVector.x += 2;
            startSpawnPoint = targetVector;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                new Vector3(targetVector.x, 0.5f),
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }



        targetVector.x += 17;

        GameObject myReward3 = Instantiate(Resources.Load("Reward2"),
              targetVector,
              Quaternion.identity) as GameObject;
        myReward3.transform.parent = rewardsHolder.transform;


        targetVector.x += 15;

        GameObject myReward4 = Instantiate(Resources.Load("Reward"),
              new Vector3(targetVector.x, 0.5f),
              Quaternion.identity) as GameObject;
        myReward4.transform.parent = rewardsHolder.transform;


    }


    void SpawnEnemyLevel1() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(135, 138);

        for (int i = 0; i < Random.Range(2, 4); i++) {

            targetVector.x += 4;

            GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
    targetVector,
    Quaternion.identity) as GameObject;
            myEnemy.transform.parent = enemiesHolder.transform;

        }

        targetVector.x += 25;

        for (int i = 0; i < Random.Range(2, 8); i++) {

            targetVector.x += 4;


            GameObject myEnemy3 = Instantiate(Resources.Load("Enemy"),
    targetVector,
    Quaternion.identity) as GameObject;
            myEnemy3.transform.parent = enemiesHolder.transform;

        }

        targetVector.x += 2;

        GameObject myEnemy2 = Instantiate(Resources.Load("Enemy"),
  targetVector,
  Quaternion.identity) as GameObject;
        myEnemy2.transform.parent = enemiesHolder.transform;


    }


    void SpawnRewardLevel2() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(223, 230);

        GameObject myReward0 = Instantiate(Resources.Load("Reward"),
                targetVector,
                Quaternion.identity) as GameObject;
        myReward0.transform.parent = rewardsHolder.transform;

        for (int i = 0; i < Random.Range(1, 3); i++) {

            targetVector.x += 12;
            startSpawnPoint = targetVector;

            GameObject myReward = Instantiate(Resources.Load("RewardBoost"),
                targetVector,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }

        targetVector.x += 5;


        for (int i = 0; i < Random.Range(1, 3); i++) {

            targetVector.x += 7;
            startSpawnPoint = targetVector;

            GameObject myReward = Instantiate(Resources.Load("Reward2"),
                targetVector,
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }

        targetVector.x += 2;

        for (int i = 0; i < Random.Range(3, 6); i++) {

            targetVector.x += 3;
            startSpawnPoint = targetVector;

            GameObject myReward = Instantiate(Resources.Load("Reward"),
                new Vector3(targetVector.x, 0.5f),
                Quaternion.identity) as GameObject;
            myReward.transform.parent = rewardsHolder.transform;
        }
    }


    void SpawnEnemyLevel2() {

        Vector3 targetVector = startSpawnPoint;

        targetVector.x = Random.Range(230, 231);

        for (int i = 0; i < Random.Range(1, 3); i++) {

            targetVector.x += 2;
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
                targetVector,
                Quaternion.identity) as GameObject;
            myEnemy.transform.parent = enemiesHolder.transform;
        }

        targetVector.x += 15;


        for (int i = 0; i < Random.Range(4, 7); i++) {

            targetVector.x += Random.Range(1, 4);
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
                targetVector,
                Quaternion.identity) as GameObject;
            myEnemy.transform.parent = enemiesHolder.transform;

        }

        targetVector.x += 15;

        for (int i = 0; i < Random.Range(4, 7); i++) {

            targetVector.x += Random.Range(3, 7);
            startSpawnPoint = targetVector;

            GameObject myEnemy = Instantiate(Resources.Load("Enemy"),
                targetVector,
                Quaternion.identity) as GameObject;
            myEnemy.transform.parent = enemiesHolder.transform;

        }


    }
}
