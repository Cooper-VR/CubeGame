using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderSpawn : MonoBehaviour
{
    #region publicVariables
    public Transform playerPosition;
    public GameObject collisionPrefab;

    public float startTime;
    #endregion

    #region privateVariabes
    private System.Random randomPositionCreator = new System.Random();
    private Vector3 randomPosition;
    private GameObject[] initalSpawns = new GameObject[20];
    private float playerPositonZ;
    public GameObject[] allObjects = new GameObject[500];
    private float startPositonZ;
    private int index;
    #endregion



    // Update is called once per frame
    void Update()
    {
        allObjects = GameObject.FindGameObjectsWithTag("object");
        DestroyFurthestObject();
        playerPositonZ = playerPosition.position.z;
        
        allObjects[index] = null;

        if (Time.time - startTime > 0.15f){
            startTime = Time.time;
            randomPosition.x = randomPositionCreator.Next(-10, 10);
            randomPosition.z = playerPosition.position.z + randomPositionCreator.Next(100, 200);
            for (int i = 0; i < allObjects.Length; i++){
                if (allObjects[i] == null){
                    allObjects[i] = Object.Instantiate(collisionPrefab, randomPosition, new Quaternion(0, 0, 0, 0));
                    break;
                }
            }
            
        }
    }

    public void initalSpawn(){
        startPositonZ = playerPosition.position.z;

        startTime = Time.time;
        randomPosition.y = 0.5f;

        for (int i  = 0; i < initalSpawns.Length; i++){
            randomPosition.x = randomPositionCreator.Next(-8, 8);
            randomPosition.z = playerPosition.position.z + randomPositionCreator.Next(20, 150);
            initalSpawns[i] = Object.Instantiate(collisionPrefab, randomPosition, new Quaternion(0, 0, 0, 0));
        }
    }

    void DestroyFurthestObject(){
        if (FindFurthestObject().transform.position.z - transform.position.z < -10){
            Debug.Log(FindFurthestObject().transform.position);
            Destroy(FindFurthestObject());
        }
        
    }

    GameObject FindFurthestObject(){
        GameObject[] allGameObject = allObjects;
        int lowestValue = 200;
        float[] objectZ = new float[allGameObject.Length];

        for (int i = 0; i < allGameObject.Length; i++){

            if (allGameObject[i] != null){
                objectZ[i] = allGameObject[i].transform.position.z;
            
                if (i > 0){
                    if (objectZ[i] < lowestValue){
                        lowestValue = (int)objectZ[i];
                    }
                }
            }
        }

        return allGameObject[System.Array.IndexOf(objectZ, lowestValue)];
    }

}
