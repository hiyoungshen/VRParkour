using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    // 前进速度, 动画组件, 动画现在状态, 动画状态参照
    public float forwardSpeed;
    private Animator anim;
    ControlScenes controlScenes;
    // private AnimatorStateInfo currentState;
    // static int jumpState = Animator.StringToHash("Base Layer.jump");
    // static int slideState = Animator.StringToHash("Base Layer.slide");
    // static int runState = Animator.StringToHash("Base Layer.run");
    // static int idleState = Animator.StringToHash("Base Layer.idle");

    private float offset;
    bool isGameStart = true;
    bool isGameEnd = false;

    private int lastState;
    private int curState;
    private int runState=1;
    private int idleState=2;
    private int jumpState=3;
    private int slideState=4;
    private int leftState=5;
    private int rightState=6;


    // Use this for initialization
    void Start()
    {
        offset=-0.5f;
        transform.position=new Vector3(3f, 0f, -9.61544f+offset);
        lastState=0;
        curState=1; //run
        anim = GetComponent<Animator>();
        controlScenes = GameObject.Find("ControlScenes").GetComponent<ControlScenes>();
    }

    // Update is called once per frame
    void Update()
    {

        // Debug.Log("lastState: "+lastState);
        // Debug.Log("curState: "+curState);
        // Shorthand for writing Vector3(-1, 0, 0)
        transform.position += Vector3.left * forwardSpeed * Time.deltaTime;
        // currentState = anim.GetCurrentAnimatorStateInfo(0);

        if (curState==runState&&Input.GetKeyDown(KeyCode.W))
        {
            lastState=curState;
            curState=jumpState;
            anim.SetInteger("curState", curState);
            anim.SetInteger("lastState", lastState);

            // anim.SetBool("jump", true);
        }
        else if (curState==runState&&Input.GetKeyDown(KeyCode.S))
        {
            lastState=curState;
            curState=slideState;
            anim.SetInteger("curState", curState);
            anim.SetInteger("lastState", lastState);
            // anim.SetBool("slide", true);
        }
        else if (curState==runState&&Input.GetKeyDown(KeyCode.A))
        {
            ChangeRunway(true);
        }
        else if (curState==runState&&Input.GetKeyDown(KeyCode.D))
        {
            ChangeRunway(false);
        }
        // jumpState等执行完一次就要回到runState
        else if (curState==jumpState)
        {
            lastState=curState;
            curState=runState;
            anim.SetInteger("curState", curState);
            anim.SetInteger("lastState", lastState);
        }
        else if (curState==slideState)
        {
            lastState=curState;
            curState=runState;
            anim.SetInteger("curState", curState);
            anim.SetInteger("lastState", lastState);
        }
        // else if (curState==leftState)
        // {
        //     lastState=curState;
        //     curState=runState;
        //     anim.SetInteger("curState", curState);
        //     anim.SetInteger("lastState", lastState);
        // }
        // else if (curState==rightState)
        // {
        //     lastState=curState;
        //     curState=runState;
        //     anim.SetInteger("curState", curState);
        //     anim.SetInteger("lastState", lastState);
        // }
        else{
            // lastState=curState;
            // curState=runState;
            // anim.SetInteger("curState", curState);
            // anim.SetInteger("lastState", lastState);
        }
        
        // Debug.Log("aa: "+curState);
        // change current state
        // if (currentState.fullPathHash == jumpState)
        // {
        //     anim.SetBool("jump", false);
        // }
        // else if (currentState.fullPathHash == slideState)
        // {
        //     anim.SetBool("slide", false);
        // }
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
            GUIStyle style = new GUIStyle();
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 40;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "你输了~", style);
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
}
