using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [System.Serializable]
    public class StickInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }
    [SerializeField] float speed;
    [SerializeField] StickInput StickControl;

    MoveController m_MoveController;
    public MoveController MoveController
    {
        get
        {
            if(m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController>();
            }
            return m_MoveController;
        }
    }
    PlayerInput playerInput;
    Vector2 lookInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = PlayerManager.Instance.PlayerInput;
        PlayerManager.Instance.LocalPlayer = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        MoveController.Move(direction);

        lookInput.x = Mathf.Lerp(lookInput.x, playerInput.LookInput.x, 1f / StickControl.Damping.x);
        transform.Rotate(Vector3.up * lookInput.x * StickControl.Sensitivity.x);

    }
}
