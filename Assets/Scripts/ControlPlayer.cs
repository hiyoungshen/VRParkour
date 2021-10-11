using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using Google.XR.Cardboard;

public class ControlPlayer : MonoBehaviour
{
    // 前进速度, 动画组件, 动画现在状态, 动画状态参照
    public float forwardSpeed=3f;
    public float turnSpeed=6f;
    private float offset=0;

    private Animator anim;
    private AnimatorStateInfo currentAnimState;
    
    // // 判断WASD是否被按下，一个时刻只能有一个为true，和unity中的状态变量需要保持一致
    // private bool isWPressed=false;
    // private bool isAPressed=false;
    // private bool isSPressed=false;
    // private bool isDPressed=false;

    ControlScenes controlScenes;
    ControlCamera controlCamera;

    bool isGameStart = true;
    bool isGameEnd = false;

    private int lastState;
    private int curState=0;
    private int runState=1;
    private int idleState=2;
    private int jumpState=3;
    private int slideState=4;
    private int leftState=5;
    private int rightState=6;

    // 手机上运行跑酷游戏时，做临时变量用
    private bool isTurnLeft=false;
    private bool isTurnRight=false;
    private bool isInMid=false;
    
    // 向左向右移动的控制变量
    private bool runTurnLeftAnim=false;
    private bool runTurnRightAnim=false;
    private bool cameraLeft=false;
    private bool cameraRight=false;
    private float cameraAngle=0f;

    private int score = 0;
    public TextMeshPro scoreText;
    public TextMeshPro bombText;
    public TextMeshPro textGameOver;

    public GameObject buttonRestart;
    public GameObject buttonExit;
    public int bombNum = 3;
    // public AudioSource bgm;
    // public AudioClip[] bgmList;


    // Use this for initialization
    void Start()
    {
        offset=-0.5f;
        transform.position=new Vector3(3f, 0f, -9.61544f+offset);
        lastState=curState;
        curState=runState; //run
        anim = GetComponent<Animator>();
        controlScenes = GameObject.Find("ControlScenes").GetComponent<ControlScenes>();
        controlCamera = GameObject.Find("Main Camera").GetComponent<ControlCamera>();
        textGameOver.gameObject.SetActive(false);
        buttonRestart.gameObject.SetActive(false);
        buttonExit.gameObject.SetActive(false);
        setTextBomb();
        setTextScore();
    }

    // Update is called once per frame
    void Update()
    {

        // Shorthand for writing Vector3(-1, 0, 0)
        transform.position += Vector3.left * forwardSpeed * Time.deltaTime;
        Debug.Log("lastState: " + lastState + " curState: " + curState);
        
        if (runTurnLeftAnim){
            if (transform.position.z - turnSpeed * Time.deltaTime > -15.61544f+offset){
                transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z - turnSpeed * Time.deltaTime);
            }
        }
        if (runTurnRightAnim){
            if (transform.position.z - turnSpeed * Time.deltaTime < -3.61544f+offset){
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + turnSpeed * Time.deltaTime);
            }
        }
        if (cameraLeft){
            // turnSpeed = (360 - controlCamera.getCameraLeftRightAngle()) / 90.0f * 7.0f;
            if (transform.position.z - turnSpeed * Time.deltaTime > -15.61544f+offset){
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - turnSpeed * Time.deltaTime);
            }
        }
        if (cameraRight){
            // turnSpeed = controlCamera.getCameraLeftRightAngle() / 90.0f * 7.0f;
            if (transform.position.z - turnSpeed * Time.deltaTime < -3.61544f+offset){
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + turnSpeed * Time.deltaTime);
            }
        }
        
        if(!isGameEnd){
            if (curState==runState&&Input.GetKeyDown(KeyCode.W))
            {
                lastState=curState;
                curState=jumpState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
            }
            else if (curState==runState&&Input.GetKeyDown(KeyCode.S))
            {
                lastState=curState;
                curState=slideState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
            }
            // 任何状态按下左转
            else if (Input.GetKeyDown(KeyCode.A))
            {
                lastState=curState;
                curState=leftState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
                runTurnLeftAnim=true;
                runTurnRightAnim=false;
                // ChangeRunway(true);
            }
            else if(Input.GetKeyUp(KeyCode.A)){
                lastState=curState;
                curState=runState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
                runTurnLeftAnim=false;
                runTurnRightAnim=false;
            }
            // else if (curState==runState&&Input.GetKeyDown(KeyCode.A))
            // {
            //     lastState=curState;
            //     curState=leftState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            //     runTurnLeftAnim=true;
            //     runTurnRightAnim=false;
            //     // ChangeRunway(true);
            // }
            // else if(curState==runState&&Input.GetKeyUp(KeyCode.A)){
            //     lastState=curState;
            //     curState=leftState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            //     runTurnLeftAnim=false;
            //     runTurnRightAnim=false;
            // }
            // 任何状态按下右转
            else if (Input.GetKeyDown(KeyCode.D))
            {
                lastState=curState;
                curState=rightState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
                runTurnLeftAnim=false;
                runTurnRightAnim=true;
                // ChangeRunway(false);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                lastState=curState;
                curState=runState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
                runTurnLeftAnim=false;
                runTurnRightAnim=false;
                // ChangeRunway(false);
            }
            // else if (curState==runState&&Input.GetKeyDown(KeyCode.D))
            // {
            //     lastState=curState;
            //     curState=rightState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            //     runTurnLeftAnim=false;
            //     runTurnRightAnim=true;
            //     // ChangeRunway(false);
            // }
            // else if (curState==runState&&Input.GetKeyUp(KeyCode.D))
            // {
            //     lastState=curState;
            //     curState=rightState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            //     runTurnLeftAnim=false;
            //     runTurnRightAnim=false;
            //     // ChangeRunway(false);
            // }
            // curState=jump
            // else if (curState==jumpState&&Input.GetKeyDown(KeyCode.A))
            // {
            //     lastState=curState;
            //     curState=leftState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            //     ChangeRunway(true);
            // }
            // else if (curState==jumpState&&Input.GetKeyDown(KeyCode.D))
            // {
            //     lastState=curState;
            //     curState=rightState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            //     ChangeRunway(false);
            // }
            
            // curState=slide
            // else if (curState==slideState&&Input.GetKeyDown(KeyCode.A))
            // {
            //     lastState=curState;
            //     curState=leftState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            //     ChangeRunway(true);
            // }
            // else if (curState==slideState&&Input.GetKeyDown(KeyCode.D))
            // {
            //     lastState=curState;
            //     curState=rightState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            //     ChangeRunway(false);
            // }
            
            // curState=left
            else if (curState==leftState&&Input.GetKeyDown(KeyCode.W))
            {
                lastState=curState;
                curState=jumpState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
            }
            else if (curState==leftState&&Input.GetKeyDown(KeyCode.S))
            {
                lastState=curState;
                curState=slideState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
            }
            // leftState和rightState之间不存在转移
            
            // curState=right
            else if (curState==rightState&&Input.GetKeyDown(KeyCode.W))
            {
                lastState=curState;
                curState=jumpState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
            }
            else if (curState==rightState&&Input.GetKeyDown(KeyCode.S))
            {
                lastState=curState;
                curState=slideState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
            }

            // 合并向runState的转移
            else if(curState==jumpState||curState==slideState){
                lastState=curState;
                curState=runState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
            }
            // else if(curState==jumpState){
            //     lastState=curState;
            //     curState=runState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            // }
            // else if(curState==slideState){
            //     lastState=curState;
            //     curState=runState;
            //     anim.SetInteger("curState", curState);
            //     anim.SetInteger("lastState", lastState);
            // }
            // 结束
            else if(curState==idleState){
                lastState=curState;
                curState=idleState;
            }

            // Debug.Log("Angle: "+ controlCamera.getCameraLeftRightAngle());
            // 控制手机上根据摄像头的旋转
            cameraAngle=(float)((int)(controlCamera.getCameraLeftRightAngle()+360)%360);
            isTurnLeft = cameraAngle > 135f && cameraAngle < 225f;
            isTurnRight=(cameraAngle > 315f && cameraAngle < 360f || 
            cameraAngle >= 0f && cameraAngle < 45f);
            isInMid= cameraAngle >= 45f && cameraAngle <= 135f ||
            cameraAngle >=225f && cameraAngle <= 315f;
            // isTurnRight=false;
            // isInMid=false;
            if (isTurnLeft){
                lastState=curState;
                curState=leftState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
                cameraLeft = true;
                cameraRight = false;
            }
            else if(isTurnRight){
                lastState=curState;
                curState=rightState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
                cameraLeft=false;
                cameraRight = true;
            }
            else if(isInMid){
                lastState=curState;
                curState=runState;
                anim.SetInteger("curState", curState);
                anim.SetInteger("lastState", lastState);
                cameraLeft=false;
                cameraRight = false;
            }
        }
        // usGameEnd=true
        else{
            curState=idleState;
            anim.SetBool("idle", true);
            // scoreText.gameObject.SetActive(false);

            // textGameOver.gameObject.SetActive(true);
            // textGameOver.text = "Game Over! \nYour Score is: " + score.ToString() + ".";
            // textGameOver.transform.position = controlCamera.transform.position + new Vector3(0, 0, 1f);
            
            // // 重新开始和退出按钮
            // buttonRestart.transform.position = controlCamera.transform.position + new Vector3(0, -0.19f, 1f);
            // buttonExit.transform.position = controlCamera.transform.position + new Vector3(0, -0.26f, 1f);
            
            // // 调整屏幕端正
            // Quaternion rotation = new Quaternion(0, 0, 0, 0);
            // transform.rotation = rotation;
        }
    }

    private bool myAbs(float x,float y){
        float z=x-y;
        return z>0?(bool)(z<0.1):(bool)(z>-0.1);
    }
    public void ChangeRunway(bool isChangeLeft)
    {
        if (isChangeLeft)
        {
            // left
            if (myAbs(transform.position.z, -15.61544f+offset)){
                return;
            }
            // mid
            else if (myAbs(transform.position.z, -13.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -15.61544f+offset);
            }
            else if (myAbs(transform.position.z, -11.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -13.61544f+offset);
            }
            else if (myAbs(transform.position.z, -9.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -11.61544f+offset);
            }
            else if (myAbs(transform.position.z, -7.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -9.61544f+offset);
            }
            else if (myAbs(transform.position.z, -5.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -7.61544f+offset);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -5.61544f+offset);
            }
        }
        else
        {
            // right
            if (myAbs(transform.position.z, -3.61544f+offset)){
                return;
            }
            else if (myAbs(transform.position.z, -5.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -3.61544f+offset);
            }
            else if (myAbs(transform.position.z, -7.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -5.61544f+offset);
            }
            else if (myAbs(transform.position.z, -9.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -7.61544f+offset);
            }
            else if (myAbs(transform.position.z, -11.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -9.61544f+offset);
            }
            else if (myAbs(transform.position.z, -13.61544f+offset))
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -11.61544f+offset);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -13.61544f+offset);
            }

        }
    }

    void OnGUI()
    {
        if (isGameEnd)
        {
            forwardSpeed = 0;
            anim.SetBool("idle", true);

            scoreText.gameObject.SetActive(false);
            bombText.gameObject.SetActive(false);
            buttonRestart?.gameObject.SetActive(true);
            buttonExit?.gameObject.SetActive(true);
            textGameOver.gameObject.SetActive(true);
            textGameOver.text = "Game Over! \nYour Score is: " + score.ToString() + ".";

            // GUIStyle style = new GUIStyle();
            // style.alignment = TextAnchor.MiddleCenter;
            // style.fontSize = 40;
            // style.normal.textColor = Color.red;
            // GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "你输了~", style);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("name: "+other.gameObject.name);
        if (other.gameObject.name == "RealObstacle")
        {
            isGameEnd = true;
            forwardSpeed = 0;
            anim.SetBool("idle", true);
        }
        if (other.gameObject.name == "Cube1")
        {
            if(isGameStart){
                isGameStart=false;
            }
            else{
                controlScenes.SwitchScene(2);
            }
        }
        else if (other.gameObject.name == "Cube2")
        {
            controlScenes.SwitchScene(0);
        }
        else if (other.gameObject.name == "Cube3")
        {
            controlScenes.SwitchScene(1);
        }
    }
    void setTextScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    public void setTextBomb()
    {
        bombText.text = "Bomb: " + bombNum.ToString();
    }
}