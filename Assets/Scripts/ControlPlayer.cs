using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    // 前进速度, 动画组件, 动画现在状态, 动画状态参照
    public float forwardSpeed = 1.0f;
    private Animator anim;
    private AnimatorStateInfo currentState;
    static int jumpState = Animator.StringToHash("Base Layer.jump");
    static int slideState = Animator.StringToHash("Base Layer.slide");
    private float offset;

    // Use this for initialization
    void Start()
    {
        offset=-0.5f;
        transform.position=new Vector3(3f, 0f, -9.61544f+offset);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Shorthand for writing Vector3(-1, 0, 0)
        transform.position += Vector3.left * forwardSpeed * Time.deltaTime;
        currentState = anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("jump", true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("slide", true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeRunway(true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeRunway(false);
        }

        // change current state
        if (currentState.fullPathHash == jumpState)
        {
            anim.SetBool("jump", false);
        }
        else if (currentState.fullPathHash == slideState)
        {
            anim.SetBool("slide", false);
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
}
