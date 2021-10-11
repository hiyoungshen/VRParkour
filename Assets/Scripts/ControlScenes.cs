using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScenes : MonoBehaviour
{
    public GameObject[] obstacleArray;
    public Transform[] obstaclePosArray;

    public GameObject[] roadArray;
    public Transform[] roadPosArray;

    public GameObject[] powerPoleArray;
    public Transform[] powerPolePosArray;

    public GameObject[] cubeArray;
    public Transform[] cubePosArray;

    public GameObject[] gemArray;

    void Start()
    {
        spawnObstacle(0);
        spawnObstacle(1);
        spawnObstacle(2);
    }
    
    public void spawnObstacle(int sceneIndex){
        // spawn obstacle
        for(int i=sceneIndex*4, j=0;j<4;i++,j++)
        {
            Vector3 eulerAngle = new Vector3(0, Random.Range(0, 360), 0);
            obstacleArray[i].transform.localEulerAngles = eulerAngle; 
        }
        // spawn gem
        for(int i=sceneIndex*8, j=0;j<8;i++,j++){
            Vector3 eulerAngle = new Vector3(0, Random.Range(0, 360), 0);
            gemArray[i].transform.localEulerAngles = eulerAngle; 
        }
    }

    public void SwitchScene(int sceneIndex){
        int lastIndex=sceneIndex%3;
        
        // road
        Debug.Log("change location of roads 1. ");
        for(int i=lastIndex*14, j=0;j<14;i++,j++){
            // Debug.Log(roadArray[i].transform.position+"    "+(roadArray[i].transform.position - new Vector3(105, 0, 0)));
            roadArray[i].transform.position = roadArray[i].transform.position - new Vector3(105, 0, 0);
            // Debug.Log(roadArray[i].transform.position);
        }

        // Powerpole
        for(int i=lastIndex*18, j=0;j<18;i++,j++){
            powerPoleArray[i].transform.position = powerPoleArray[i].transform.position - new Vector3(105, 0, 0);
        }

        // Cube
        cubeArray[lastIndex].transform.position=cubePosArray[lastIndex].transform.position-new Vector3(105,0,0);

        // Obstacle
        for(int i=lastIndex*4,j=0;j<4;i++,j++)
        {
            // random choose a object
            // GameObject prefab = obstacleArray[i];
            Vector3 eulerAngle = new Vector3(0, Random.Range(0, 360), 0);
            // GameObject obj = Instantiate(prefab, obstaclePosArray[i].position, Quaternion.Euler(eulerAngle));
            obstacleArray[i].transform.localEulerAngles = eulerAngle; 
            obstacleArray[i].transform.position=obstacleArray[i].transform.position-new Vector3(105,0,0);
        }

        // gem
        for(int i=lastIndex*8,j=0;j<8;i++,j++)
        {
            // random choose a object
            Vector3 eulerAngle = new Vector3(0, Random.Range(0, 360), 0);
            // GameObject obj = Instantiate(prefab, obstaclePosArray[i].position, Quaternion.Euler(eulerAngle));
            gemArray[i].transform.localEulerAngles = eulerAngle; 
            gemArray[i].transform.position=gemArray[i].transform.position-new Vector3(105,0,0);
        }
    }
    // void Update()
    // {
        
    // }
}