using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{

    private GameObject gameObject;

    private static PlayerManager m_Instance;
    public static PlayerManager Instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = new PlayerManager();
                m_Instance.gameObject = new GameObject("_playerManager");
                m_Instance.gameObject.AddComponent<PlayerInput>();
            }
            return m_Instance;
        }
    }

    private PlayerInput m_PlayerInput;
    public PlayerInput PlayerInput
    {
        get
        {
            if(m_PlayerInput == null)
            {
                m_PlayerInput = gameObject.GetComponent<PlayerInput>();
            }
            return m_PlayerInput;
        }
    }

    private Player m_localPlayer;
    public Player LocalPlayer
    {
        get
        {
            return m_localPlayer;
        }
        set
        {
            m_localPlayer = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
