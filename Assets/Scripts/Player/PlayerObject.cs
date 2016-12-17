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

    private GameObject ThirdPersonCamera;
    private GameObject DriverCamera;
    private GameObject MainGunCamera;

    #endregion Class Level Variables

    #region Unity Methods

    // Use this for initialization
    void Start ()
	{
	    ThirdPersonCamera = GameObject.Find("ThirdPartyCamera");
        DriverCamera = GameObject.Find("DriverCamera");
        MainGunCamera = GameObject.Find("MainGunCamera");

	}

    // Update is called once per frame
    void Update ()
    {

    }

    #endregion Unity Methods

    #region Private Methods

    private void postiionCamera(GameObject player, PlayerRole role)
    {
        var p = player.GetComponent<Player>();
        if(Players[role] == player)
        {
            switch (role)
            {
                case PlayerRole.Driver:
                    p.transform.SetParent(DriverCamera.transform);
                    p.transform.localPosition = new Vector3(0, 0, 0);
                    p.transform.Rotate(13.29f, 0, 0, 0);

                    break;
                case PlayerRole.MainGun:
                    p.transform.SetParent(MainGunCamera.transform);
                    p.transform.localPosition = new Vector3(0, 0, 0);
                    p.transform.Rotate(0, 0, 0, 0);

                    break;
                default:
                    break;
            }
        }
    }

    #endregion Private Methods

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

                postiionCamera(player, PlayerRole.Driver);
                break;
            case PlayerRole.MainGun:
                var mainGunController = player.AddComponent<PlayerController_MainGun>();
                mainGunController.SetupMainGun(gameObject);

                postiionCamera(player, PlayerRole.MainGun);
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
