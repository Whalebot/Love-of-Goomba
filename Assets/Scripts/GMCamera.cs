using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMCamera : MonoBehaviour
{
    Camera cam;
    public float offset;
    public float rightOffset;
    public float upOffset;
    public Vector3 startPos;
    public float panSpeed;
    public float panRangeX;
    public float panRangeY;
    public float x, y;
    public float screenX, screenY;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        startPos = transform.position;
    }


    private void Update()
    {
        x = Input.mousePosition.x;
        y = Input.mousePosition.y;
        screenX = Screen.width;
        screenY = Screen.height;

        if (Input.mousePosition.x < panRangeX * Screen.width) rightOffset -= panSpeed;
        else if (Input.mousePosition.x > Screen.width - (panRangeX * Screen.width)) rightOffset += panSpeed;

        if (Input.mousePosition.y < panRangeY * Screen.height) upOffset -= panSpeed;
        else if (Input.mousePosition.y > Screen.height - (panRangeY * Screen.height)) upOffset += panSpeed;

    }
    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = startPos + transform.forward * offset + transform.right * rightOffset + transform.up * upOffset;
    }
}
