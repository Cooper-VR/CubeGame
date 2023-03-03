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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPositonZ = playerPosition.position.z;
        if (playerPositonZ - startPositonZ > 30f){
            startPositonZ = playerPositonZ;
            Destroy(allObjects[0]);
            for (int i = 0; i < allObjects.Length - 1; i++){
            // moving elements downwards, to fill the gap at [index]
                allObjects[i] = allObjects[i + 1];
            }
            for (int i = 0; i < allObjects.Length - 1; i++){
            // moving elements downwards, to fill the gap at [index]
                if (allObjects[i] == null){
                    index = i;
                }
            }
            allObjects[index] = null;
        }

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
            allObjects[i] = initalSpawns[i];
        }
    }
}
