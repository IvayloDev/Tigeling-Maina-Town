using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

    public GameObject TreesHolder, LampsHolder, SpawnedObjectHolder;
    public GameObject ParentForSpawnedPeopleFront, ParentForSpawnedPeopleBack, ParentForSpawnedBuilding;

    public Sprite[] BuildingsSprites, PeopleFrontSprites, PeopleBackSprites;



    public Vector3 BuildingsPos;
    public Vector3 PeopleFrontPos, PeopleBackPos;


    void Start() {

        //Generate Terain


        //Buildings
        SpawnBuildings();

        //People
        SpawnPeople();


    }

    void SpawnBuildings() {

        float prevWidth = 1;

        Vector3 nextPos = BuildingsPos;

        for (int i = 0; i < 140; i++) {

            Sprite sprite = BuildingsSprites[Random.Range(0, 5)];
            float currentWidth = sprite.bounds.size.x;

            nextPos += new Vector3(currentWidth / 2 + prevWidth / 2, 0, 0);

            GameObject Background = (GameObject)Instantiate(SpawnedObjectHolder, nextPos, Quaternion.identity);

            Background.GetComponent<SpriteRenderer>().sprite = sprite;

            prevWidth = currentWidth;

            Background.transform.parent = ParentForSpawnedBuilding.transform;

        }
    }

    void SpawnPeople() {


        Vector3 nextPos = PeopleBackPos;

        //Front
        for (int i = 0; i < 100; i++) {

            Sprite sprite = PeopleBackSprites[Random.Range(0, 7)];

            GameObject PeopleFront = (GameObject)Instantiate(SpawnedObjectHolder, nextPos, Quaternion.identity);

            PeopleFront.GetComponent<SpriteRenderer>().sprite = sprite;
            PeopleFront.transform.parent = ParentForSpawnedPeopleFront.transform;

            nextPos += new Vector3(3, 0, 0);

        }


        nextPos = PeopleFrontPos;

        //Back
        for (int i = 0; i < 100; i++) {

            Sprite sprite = PeopleFrontSprites[Random.Range(0, 7)];

            GameObject PeopleBack = (GameObject)Instantiate(SpawnedObjectHolder, nextPos, Quaternion.identity);

            PeopleBack.GetComponent<SpriteRenderer>().sprite = sprite;
            PeopleBack.transform.parent = ParentForSpawnedPeopleBack.transform;

            nextPos += new Vector3(3, 0, 0);

        }


    }

    void Update() {

        if (PlayerMovement.startMoving && !PlayerMovement.isDead) {

            if (!CollisionManager.bonusLevelActive) {
                ScrollObject(TreesHolder, 0.045f);
                ScrollObject(LampsHolder, 0.045f);
                ScrollObject(ParentForSpawnedPeopleFront, 0.025f);
                ScrollObject(ParentForSpawnedPeopleBack, 0.042f);
                ScrollObject(ParentForSpawnedBuilding, 0.05f);

            }
        }

    }

    void ScrollObject(GameObject go, float speed) {

        go.transform.Translate(new Vector3(speed, 0, 0));

    }


}
