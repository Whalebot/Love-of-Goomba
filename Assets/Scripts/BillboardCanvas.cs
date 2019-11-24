using UnityEngine;
using System.Collections;

public class BillboardCanvas : MonoBehaviour
{
    GameObject camcam;
    public Camera m_Camera;

    private void Start()
    {
        camcam = GameObject.FindGameObjectWithTag("GMCam");
        if (camcam != null)
    
            m_Camera = camcam.GetComponent<Camera>();
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