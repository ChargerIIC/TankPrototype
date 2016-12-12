using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        ActiveController = new PlayerController_Driver();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    #region Public Methods

    public bool SwitchRoles(PlayerRole role)
    {
        var result = false;

        if(PlayerControllers.ContainsKey(role))
        {
            ActiveController = PlayerControllers[role];
        }

        return result;
    }

    #endregion Public Methods

    #region Public Properties

    public IPlayerController ActiveController;

    public PlayerControllerDictionary PlayerControllers;
    #endregion Public Properties
}
