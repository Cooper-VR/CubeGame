using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderSpawn : MonoBehaviour
{
    #region publicVariables
    public Transform playerPosition;
    public GameObject collisionPrefab;
    public GameObject[] allObjects = new GameObject[500];
    #endregion

    #region privateVariabes
    private System.Random randomPositionCreator = new System.Random();
    private Vector3 randomPosition;
    private GameObject[] initalSpawns = new GameObject[10];
    private float playerPositonZ;
    private float startPositonZ;
    private int index;
    private float startTime;
    #endregion

    private void Start() {
        initalSpawn(allObjects);
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("object").Length; i++)
        {
            allObjects[i] = GameObject.FindGameObjectsWithTag("object")[i];
        }

        DestroyFurthestObject(allObjects);
        playerPositonZ = playerPosition.position.z;

        if (Time.time - startTime > 1/ startTime / 1.4f){
            startTime = Time.time;
            randomPosition.x = randomPositionCreator.Next(-10, 10);
            randomPosition.z = playerPosition.position.z + randomPositionCreator.Next(150, 250);
            for (int i = 0; i < allObjects.Length; i++){
                if (allObjects[i] == null){
                    allObjects[i] = Object.Instantiate(collisionPrefab, randomPosition, new Quaternion(0, 0, 0, 0));
                    break;
                }
            }
            
        }
    }

    public void initalSpawn(GameObject[] allObjects){
        startPositonZ = playerPosition.position.z;

        startTime = Time.time;
        randomPosition.y = 0.5f;

        for (int i  = 0; i < initalSpawns.Length; i++){
            randomPosition.x = randomPositionCreator.Next(-8, 8);
            randomPosition.z = playerPosition.position.z + randomPositionCreator.Next(20, 150);
            initalSpawns[i] = Object.Instantiate(collisionPrefab, randomPosition, new Quaternion(0, 0, 0, 0));
            allObjects[i] = initalSpawns[i];
        }
    }

    void DestroyFurthestObject(GameObject[] allObjects){
        if (FindFurthestObject(allObjects).transform.position.z - transform.position.z < -10){
            Debug.Log(FindFurthestObject(allObjects).transform.position);
            Destroy(FindFurthestObject(allObjects));
        }
        
    }

    GameObject FindFurthestObject(GameObject[] allObjects){
        float lowestValue = 20000;
        float[] objectZ = new float[500];

        for (int i = 0; i < allObjects.Length; i++){

            if (allObjects[i] != null){
                objectZ[i] = allObjects[i].transform.position.z;
            
                if (i > 0){
                    if (objectZ[i] < lowestValue){
                        lowestValue = objectZ[i];
                    }
                }
            }
        }

        try
        {
            return allObjects[System.Array.IndexOf(objectZ, lowestValue)];
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.Log("index out of range");
            return allObjects[System.Array.IndexOf(objectZ, lowestValue + 1)];
        }
        
    }

}
