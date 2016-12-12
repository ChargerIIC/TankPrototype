using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerObject : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    ActiveController = PlayerControllers.First().Value;
	}

    // Update is called once per frame
    void Update ()
    {
	 
	}

    public bool SwitchRoles(PlayerRole role)
    {
        var result = false;

        if (PlayerControllers.ContainsKey(role))
        {
            ActiveController = PlayerControllers[role];
        }

        return result;
    }

    //public Player PlayerClass;
    public GameObject ActiveController;

    [Inspectionary("PlayerRole", "PlayerController")]
    public PlayerControllerDictionary PlayerControllers;

}
