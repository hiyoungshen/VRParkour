// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.XR;
// using UnityEngine.UI;
// using TMPro;

// public class ControlPlayer : MonoBehaviour
// {
//     ControlScene m_ControlScenes;
//     ControlCamera m_ControlCamera;
//     public float m_ForwardSpeeed = 4.0f;
//     public float m_LeftRightSpeeed = 7.0f;
//     private Animator m_Anim;
//     private AnimatorStateInfo m_CurrentBaseState;
//     static int m_jumpState = Animator.StringToHash("Base Layer.JUMP00");
//     private bool keyADown = false;
//     private bool keyDDown = false;
//     private bool m_GameOver = false;
//     private bool m_FirstTimeOnRoad1 = false;
//     private bool m_FirstTimeOnRoad2 = true;
//     private bool m_FirstTimeOnRoad3 = true;
//     private bool cameraLeft = false;
//     private bool cameraRight = false;
//     public TextMeshPro scoreText;
//     public TextMeshPro GameOverText;	// Reference to a UI element to output the score to
//     public TextMeshPro BombText;
//     public GameObject RestartButton;
//     public GameObject ExitButton;
//     private int score = 0;
//     public int ClickTimes = 3;
//     public AudioSource bgm;
//     public AudioClip[] bgmList;

//     // Start is called before the first frame update
//     void Start()
//     {
//         m_Anim = GetComponent<Animator>();
//         m_ControlScenes = GameObject.Find("ControlScene").GetComponent<ControlScene>();
//         m_ControlCamera = GameObject.Find("Player").GetComponent<ControlCamera>();
//         GameOverText.gameObject.SetActive(false);
//         SetBombText();
//         SetScoreText();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (!bgm.isPlaying)
//         {
//             int index = Random.Range(0, bgmList.Length);
//             bgm.clip = bgmList[index];
//             bgm.Play();
//         }
//         if (keyADown)
//         {
//             m_LeftRightSpeeed = 7.0f;
//             if (transform.position.x - m_LeftRightSpeeed * Time.deltaTime > -10.74f)
//                 transform.position = new Vector3(transform.position.x - m_LeftRightSpeeed * Time.deltaTime, transform.position.y, transform.position.z);
//         }
//         if (keyDDown)
//         {
//             m_LeftRightSpeeed = 7.0f;
//             if (transform.position.x - m_LeftRightSpeeed * Time.deltaTime < -1.751f)
//                 transform.position = new Vector3(transform.position.x + m_LeftRightSpeeed * Time.deltaTime, transform.position.y, transform.position.z);
//         }
//         if (cameraLeft)
//         {
//             m_LeftRightSpeeed = (360 - m_ControlCamera.GetCameraY()) / 90.0f * 7.0f;
//             if (transform.position.x - m_LeftRightSpeeed * Time.deltaTime > -10.74f)
//                 transform.position = new Vector3(transform.position.x - m_LeftRightSpeeed * Time.deltaTime, transform.position.y, transform.position.z);
//         }
//         if (cameraRight)
//         {
//             m_LeftRightSpeeed = m_ControlCamera.GetCameraY() / 90.0f * 7.0f;
//             if (transform.position.x - m_LeftRightSpeeed * Time.deltaTime < -1.751f)
//                 transform.position = new Vector3(transform.position.x + m_LeftRightSpeeed * Time.deltaTime, transform.position.y, transform.position.z);
//         }
//         transform.position += Vector3.forward * m_ForwardSpeeed * Time.deltaTime;

//         // 
//         if (!m_GameOver)
//         {
//             Vector3 eulerAngle = new Vector3(transform.eulerAngles.x, 0, 0);
//             transform.eulerAngles = eulerAngle;
//             //Lock the forward angle
//             m_CurrentBaseState = m_Anim.GetCurrentAnimatorStateInfo(0);

//             if (Input.GetKeyDown(KeyCode.Space))
//             {
//                 m_Anim.SetBool("Jump", true);
//             }
//             else if (Input.GetKeyDown(KeyCode.A))
//             {
//                 m_Anim.SetBool("RunLeft", true);
//                 m_Anim.SetBool("RunRight", false);
//                 keyADown = true;
//                 keyDDown = false;
//             }
//             else if (Input.GetKeyUp(KeyCode.A))
//             {
//                 m_Anim.SetBool("RunLeft", false);
//                 keyADown = false;
//             }
//             else if (Input.GetKeyDown(KeyCode.D))
//             {
//                 m_Anim.SetBool("RunRight", true);
//                 m_Anim.SetBool("RunLeft", false);
//                 keyDDown = true;
//                 keyADown = false;
//             }
//             else if (Input.GetKeyUp(KeyCode.D))
//             {
//                 m_Anim.SetBool("RunRight", false);
//                 keyDDown = false;
//             }

//             if (m_ControlCamera.GetCameraY() < (360f - 7.5f) && m_ControlCamera.GetCameraY() > 270f)
//             {
//                 m_Anim.SetBool("RunLeft", true);
//                 cameraLeft = true;
//             }
//             else if(m_ControlCamera.GetCameraY() < 90f && m_ControlCamera.GetCameraY() > 7.5f)
//             {
//                 m_Anim.SetBool("RunRight", true);
//                 cameraRight = true;
//             }
//             else if (m_ControlCamera.GetCameraY() > (360f - 7.5f) && m_ControlCamera.GetCameraY() < 360f || m_ControlCamera.GetCameraY() < 7.5f && m_ControlCamera.GetCameraY() > 0f || m_ControlCamera.GetCameraY() > 90f && m_ControlCamera.GetCameraY() < 270f)
//             {
//                 m_Anim.SetBool("RunLeft", false);
//                 m_Anim.SetBool("RunRight", false);
//                 cameraLeft = false;
//                 cameraRight = false;
//             }
//             if (m_CurrentBaseState.fullPathHash == m_jumpState)
//             {
//                 m_Anim.SetBool("Jump", false);
//             }
//         }
//         if (m_GameOver)
//         {
//             scoreText.gameObject.SetActive(false);
//             GameOverText.gameObject.SetActive(true);
//             GameOverText.text = "Game Over! \nYour Final Score is: " + score.ToString() + ".";
//             GameOverText.transform.position = m_ControlCamera.transform.position + new Vector3(0, 0, 1f);
//             RestartButton.transform.position = m_ControlCamera.transform.position + new Vector3(0, -0.19f, 1f);
//             ExitButton.transform.position = m_ControlCamera.transform.position + new Vector3(0, -0.26f, 1f);
//             Quaternion rotation = new Quaternion(0, 0, 0, 0);
//             transform.rotation = rotation;
//             bgm.Stop();
//             //Lock the forward angle and rotation
//         }
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.tag == "Obstacle0" || other.gameObject.tag == "Obstacle1" || other.gameObject.tag == "Obstacle2")
//         {
//             m_GameOver = true;
//             m_ForwardSpeeed = 0;
//             m_Anim.SetBool("Idle", true);
//         }
//         if (other.gameObject.tag == "Boom0" || other.gameObject.tag == "Boom1" || other.gameObject.tag == "Boom2")
//         {
//             other.gameObject.SetActive(false);
//             ClickTimes += 1;
//             SetBombText();
//         }
//         if (other.gameObject.tag == "Pickup0" || other.gameObject.tag == "Pickup1" || other.gameObject.tag == "Pickup2")
//         {
//             other.gameObject.SetActive(false);
//             score += 1;
//             SetScoreText();
//         }
//         if (other.gameObject.name == "MonitorPos0" && m_FirstTimeOnRoad1)
//         {
//             m_FirstTimeOnRoad1 = false;
//             m_FirstTimeOnRoad3 = true;
//             m_ControlScenes.ChangeRoad(2);
//         }
//         else if (other.gameObject.name == "MonitorPos1" && m_FirstTimeOnRoad2)
//         {
//             m_FirstTimeOnRoad2 = false;
//             m_FirstTimeOnRoad1 = true;
//             m_ControlScenes.ChangeRoad(0);
//         }
//         else if (other.gameObject.name == "MonitorPos2" && m_FirstTimeOnRoad3)
//         {
//             m_FirstTimeOnRoad3 = false;
//             m_FirstTimeOnRoad2 = true;
//             m_ControlScenes.ChangeRoad(1);
//             if (m_ForwardSpeeed < 8.0f)
//                 m_ForwardSpeeed = Mathf.Lerp(m_ForwardSpeeed, m_ForwardSpeeed + 1f, Time.deltaTime * 10000);
//             if (m_ControlScenes.obstacleRate < 100f)
//             {
//                 m_ControlScenes.obstacleRate += 10;
//             }
//             else if (m_ControlScenes.EndObstacle > 5)
//             {
//                 m_ControlScenes.EndObstacle -= 1;
//             }
//         }
//     }

//     public bool GetGameOver()
//     {
//         return m_GameOver;
//     }

//     void SetScoreText()
//     {
//         scoreText.text = "Score: " + score.ToString();
//     }
//     public void SetBombText()
//     {
//         BombText.text = "Bomb: " + ClickTimes.ToString();
//     }
// }
