using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class PlayerObject : NetworkBehaviour
{
    #region Class Level Variables

    private Camera ThirdPersonCamera;
    private Camera DriverCamera;
    private Camera MainGunCamera;

    #endregion Class Level Variables

    #region Unity Methods

    // Use this for initialization
    void Start ()
	{
	    //ActiveController = typeof(PlayerController_Driver);
	    var cameras = gameObject.GetComponentsInChildren<Camera>();
	    ThirdPersonCamera = cameras.First(x => x.name == "ThirdPartyCamera");
        DriverCamera = cameras.First(x => x.name == "DriverCamera");
        MainGunCamera = cameras.First(x => x.name == "MainGunCamera");

	}

    // Update is called once per frame
    void Update ()
    {
        //if (Input.GetKeyUp(KeyCode.F2))
        //{
        //    SwitchRoles(PlayerRole.Driver);
        //}
        //else if (Input.GetKeyUp(KeyCode.F3))
        //{
        //    SwitchRoles(PlayerRole.MainGun);
        //}

    }

    #endregion Unity Methods

    #region Public Methods

    public bool SwitchRoles(GameObject player, PlayerRole role)
    {
        var result = false;
        //Remove any existing role
        if (Players.ContainsValue(player))
        {
            var oldRole = Players.First(kvp => kvp.Value == player).Key;
            RemovePlayer(player, oldRole);
        }

        AddPlayer(player, role);

        return result;
    }

    public void RemovePlayer(GameObject player, PlayerRole role)
    {
        switch (role)
        {
            case PlayerRole.Driver:
                var driverController = player.GetComponent<PlayerController_Driver>();
                GameObject.Destroy(driverController);
                break;
            case PlayerRole.MainGun:
                var mainGunController = player.GetComponent<PlayerController_MainGun>();
                GameObject.Destroy(mainGunController);
                break;
            default:
                break;
        }
        Players[role] = null;
        player.transform.parent = null;
    }

    public void AddPlayer(GameObject player, PlayerRole role)
    {
        player.transform.parent = gameObject.transform;
        
            if (role == PlayerRole.None && Players.Any(kvp => kvp.Value == null))
            {
                role = Players.First(kvp => kvp.Value == null).Key;
            }

            Players[role] = player;
            switch (role)
            {
                case PlayerRole.Driver:
                    var driverController = player.AddComponent<PlayerController_Driver>();
                    driverController.SetupTracks(gameObject);

                    ThirdPersonCamera.enabled = false;
                    DriverCamera.enabled = true;
                    MainGunCamera.enabled = false;
                    break;
                case PlayerRole.MainGun:
                    var mainGunController = player.AddComponent<PlayerController_MainGun>();
                    mainGunController.SetupMainGun(gameObject);
                    ThirdPersonCamera.enabled = false;
                    DriverCamera.enabled = false;
                    MainGunCamera.enabled = true;
                    break;
                default:
                    break;
            }
       
    }

    #endregion Public Methods

    #region Public Properties

    public PlayerControllerDictionary Players;

    #endregion Public Properties

}
