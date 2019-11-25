using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] float damping;

    public float yOffset;
    public float ySensitivity;
    public float yMax;
    public float yMin;
    public Transform cameraLookTarget;
    public Player localPlayer;
    PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        cameraLookTarget = localPlayer.transform.Find("cameraLookTarget");
        playerInput = PlayerManager.Instance.PlayerInput;
    }

    // Update is called once per frame
    void Update()
    {

        yOffset += -playerInput.LookInput.y * ySensitivity;
        yOffset = Mathf.Clamp(yOffset, yMin, yMax);

        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z + localPlayer.transform.up * (cameraOffset.y - yOffset) + localPlayer.transform.right * cameraOffset.x;
        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);

        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(cameraLookTarget.position.x, cameraLookTarget.position.y + yOffset, cameraLookTarget.transform.position.z) - targetPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);

    }
}
