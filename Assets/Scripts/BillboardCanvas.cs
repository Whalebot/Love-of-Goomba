using UnityEngine;
using System.Collections;

public class BillboardCanvas : MonoBehaviour
{
    public Camera m_Camera;

    private void Start()
    {
        m_Camera = GameObject.FindGameObjectWithTag("GMCam").GetComponent<Camera>();
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        if (m_Camera != null)
        {
            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
        }

    }
}