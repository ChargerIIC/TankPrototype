using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    private Camera playerCamera;
	// Use this for initialization
	void Start ()
	{
	    playerCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isLocalPlayer)
        {
            playerCamera.enabled = true;
        }
        else
        {
            playerCamera.enabled = false;
        }
	}

    public bool LocalPlayer
    {
        get
        {
            return gameObject.GetComponent<NetworkIdentity>().isLocalPlayer;
            
        }
    }
}
