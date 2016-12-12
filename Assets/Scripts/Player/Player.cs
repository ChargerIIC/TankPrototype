using UnityEngine;
using System.Collections;

public class Player 
{

	// Use this for initialization
	public Player(GameObject gameObject)
    {
        ActiveController = new PlayerController_Driver(gameObject);
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
