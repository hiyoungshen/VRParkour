// using System.Collections;
// using System.Collections.Generic;
// using System.Threading;
// using System.Timers;
// using UnityEditor;
// using UnityEngine;

// public class ControlCamera : MonoBehaviour
// {
//     // Start is called before the first frame update
//     ControlPlayer m_ControlPlayer;
//     GameObject m_MainCamera;
//     public float m_DistanceAway = 2f;
//     public float m_DistanceHeight = 2f;
//     public float smooth = 2f;
//     private Vector3 m_TargetPosition;
//     Transform m_Follow;
//     public bool ThirdPersonCamera = true;

//     void Start()
//     {
//         m_Follow = GameObject.Find("UnityChan").transform;
//         m_ControlPlayer = GameObject.Find("UnityChan").GetComponent<ControlPlayer>();
//         m_MainCamera = GameObject.Find("Main Camera");
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (ThirdPersonCamera)
//         {
//             m_TargetPosition = m_Follow.position + m_Follow.up * m_DistanceHeight - m_Follow.forward * m_DistanceAway;
//             smooth = 2.0f;
//         }
//         else
//         {
//             m_TargetPosition = m_Follow.position + m_Follow.up * 1.33f + m_Follow.forward * (5.5f - 5.067089f);
//             if (smooth < 100f)
//             {
//                 smooth += 1f;
//             }
//         }
//         if (m_ControlPlayer.GetGameOver())
//         {
//             m_TargetPosition = m_Follow.position + m_Follow.up * m_DistanceHeight - m_Follow.forward * -2f;
// /*            Vector3 eulerAngle = new Vector3(0, 180, 0);
//             transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, eulerAngle, Time.deltaTime * smooth);*/
//         }
//         transform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * smooth);

//     }

//     public float GetCameraY()
//     {
//         return m_MainCamera.transform.eulerAngles.y;
//     }
// }
