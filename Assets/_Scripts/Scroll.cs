using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

    public GameObject TreesHolder, LampsHolder, SpawnedObjectHolder;
    public GameObject ParentForSpawnedPeopleFront, ParentForSpawnedPeopleBack, ParentForSpawnedBuilding;

    public Sprite[] BuildingsSprites, PeopleFrontSprites, PeopleBackSprites;



    public Vector3 BuildingsPos;
    public Vector3 PeopleFrontPos, PeopleBackPos;

    private int r;
    private Sprite tmp;

    void Start() {


        //Buildings
        SpawnBuildings();

        //People
        SpawnPeople();


    }


    void SpawnBuildings() {

        for (int i = BuildingsSprites.Length - 1; i > 0; i--) {              // get the count of the array an shuffle all the elements   begin from end to start of the array 
            r = Random.Range(0, i);         //	get a random number from 0 to array count
            tmp = BuildingsSprites[i];                                              //	swap the random place (eg: 3) and assign it to tmp 
            BuildingsSprites[i] = BuildingsSprites[r];          // swap the i(current number) with tmp
            BuildingsSprites[r] = tmp;                                              // swap the tmp with the value of i
        }


        float prevWidth = 1;

        Vector3 nextPos = BuildingsPos;

        for (int i = 0; i < 110; i++) {

            if (r >= BuildingsSprites.Length) {
                for (int j = BuildingsSprites.Length - 1; j > 0; j--) {              // get the count of the array an shuffle all the elements   begin from end to start of the array 
                    r = Random.Range(0, j);         //	get a random number from 0 to array count
                    tmp = BuildingsSprites[j];                                              //	swap the random place (eg: 3) and assign it to tmp 
                    BuildingsSprites[j] = BuildingsSprites[r];          // swap the i(current number) with tmp
                    BuildingsSprites[r] = tmp;                                              // swap the tmp with the value of i
                }
            }

            Sprite sprite = BuildingsSprites[r++];

            //Sprite sprite = BuildingsSprites[Random.Range(0, 5)];

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
        for (int i = 0; i < 110; i++) {

            Sprite sprite = PeopleBackSprites[Random.Range(0, 7)];

            GameObject PeopleFront = (GameObject)Instantiate(SpawnedObjectHolder, nextPos, Quaternion.identity);

            PeopleFront.GetComponent<SpriteRenderer>().sprite = sprite;
            PeopleFront.transform.parent = ParentForSpawnedPeopleFront.transform;

            nextPos += new Vector3(3, 0, 0);

        }


        nextPos = PeopleFrontPos;

        //Back
        for (int i = 0; i < 110; i++) {

            Sprite sprite = PeopleFrontSprites[Random.Range(0, 7)];

            GameObject PeopleBack = (GameObject)Instantiate(SpawnedObjectHolder, nextPos, Quaternion.identity);

            PeopleBack.GetComponent<SpriteRenderer>().sprite = sprite;
            PeopleBack.transform.parent = ParentForSpawnedPeopleBack.transform;

            nextPos += new Vector3(3, 0, 0);

        }


    }

    void Update() {

        if (PlayerMovement.startMoving && !PlayerMovement.isDead) {

            ScrollObject(TreesHolder, 0.025f);
            ScrollObject(LampsHolder, 0.025f);
            ScrollObject(ParentForSpawnedPeopleFront, 0.00f);
            ScrollObject(ParentForSpawnedPeopleBack, 0.022f);
            ScrollObject(ParentForSpawnedBuilding, 0.055f);

        }

    }

    void ScrollObject(GameObject go, float speed) {

        go.transform.Translate(new Vector3(speed, 0, 0));

    }


}
