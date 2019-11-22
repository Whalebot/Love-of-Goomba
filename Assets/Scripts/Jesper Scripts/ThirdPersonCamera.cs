using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float damping;

    public Transform cameraLookTarget;
    public Player localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        cameraLookTarget = localPlayer.transform.Find("cameraLookTarget");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z + localPlayer.transform.up * cameraOffset.y + localPlayer.transform.right * cameraOffset.x;
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);

    }
}
