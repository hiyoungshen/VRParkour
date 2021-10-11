using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    // 距离, 高度，平滑值, 目标点, 目标点, 参照点
    public float intervalDistance = 5f;
    public float height = 10f;
    public float smooth = 2f;               
    private Vector3 targetPosition;      
    
    ControlPlayer controlPlayer;
    GameObject mainCamera;  
    Transform playerPos;

    public bool thirdPersonPerspective;
          

    void Start()
    {
        controlPlayer=GameObject.Find("Player").GetComponent<ControlPlayer>();
        mainCamera = GameObject.Find("Main Camera");
        playerPos = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        if(thirdPersonPerspective){
            targetPosition = playerPos.position + Vector3.up * height - playerPos.forward * intervalDistance;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        }
        else{
            targetPosition = playerPos.position + playerPos.up * 1.33f + playerPos.forward * (5.5f - 5.067089f);
            if (smooth < 100f)
            {
                smooth += 1f;
            }
        }

        // if (controlPlayer.GetGameOver())
        // {
        //     targetPosition = playerPos.position + playerPos.up * height - playerPos.forward * (-2f);
        // }
        // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
    }
    
    public float getCameraLeftRightAngle()
    {
        return mainCamera.transform.eulerAngles.y;
    }
}
