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

    void Start()
    {
        spawnObstacle(1);
        spawnObstacle(2);
        spawnObstacle(3);
    }
    
    public void spawnObstacle(int sceneIndex){
        // destroy original obstacle
        // GameObject[] obsPast = GameObject.FindGameObjectsWithTag("Obstacle"+sceneIndex);
        // Debug.Log("a: "+obsPast.Length);
        // Debug.Log("b: "+obstacleArray.Length);
        // Debug.Log("c: "+obstaclePosArray.Length);
        // for (int i = 0; i < obsPast.Length; i++){
        //     Destroy(obsPast[i]);
        // }
        // spawn obstacle
        for(int i=(sceneIndex-1)*4;i<obstaclePosArray.Length;i++)
        {
            // random choose a object
            // GameObject prefab = obstacleArray[i];
            Vector3 eulerAngle = new Vector3(0, Random.Range(0, 360), 0);
            // GameObject obj = Instantiate(prefab, obstaclePosArray[i].position, Quaternion.Euler(eulerAngle));
            obstacleArray[i].transform.localEulerAngles = eulerAngle; 
        }
    }
    public void SwitchScene(int sceneIndex){
        int lastIndex=(sceneIndex-1)%3;
        if(lastIndex==0){
            lastIndex=3;
        }
        // road
        for(int i=(lastIndex-1)*14, j=0;j<14;i++,j++){
            roadArray[i].transform.position = roadArray[i].transform.position - new Vector3(35, 0, 0);
        }

        // Powerpole
        for(int i=(lastIndex-1)*16, j=0;j<16;i++,j++){
            roadArray[i].transform.position = roadArray[i].transform.position - new Vector3(35, 0, 0);
        }
    }
    void Update()
    {
        
    }
}