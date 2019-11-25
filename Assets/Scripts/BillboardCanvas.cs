using UnityEngine;
using System.Collections;

public class BillboardCanvas : MonoBehaviour
{
    GameObject camcam;
    public enum Owner { GameMaster, Player };
    public Owner owner = Owner.GameMaster;
    public Camera m_Camera;

    private void Start()
    {
        if (owner == Owner.GameMaster) {
            camcam = GameObject.FindGameObjectWithTag("GMCam");
            if (camcam != null)

                m_Camera = camcam.GetComponent<Camera>();
        }
        else if (owner == Owner.Player)
        {
            camcam = Camera.main.gameObject;
            if (camcam != null)

                m_Camera = camcam.GetComponent<Camera>();
        }

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