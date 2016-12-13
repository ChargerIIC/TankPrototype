using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class PlayerObject : MonoBehaviour
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
	    ActiveController = typeof(PlayerController_Driver);
	    var cameras = gameObject.GetComponentsInChildren<Camera>();
	    ThirdPersonCamera = cameras.First(x => x.name == "ThirdPartyCamera");
        DriverCamera = cameras.First(x => x.name == "DriverCamera");
        MainGunCamera = cameras.First(x => x.name == "MainGunCamera");

        SwitchRoles(PlayerRole.Driver);
	}

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.F2))
        {
            SwitchRoles(PlayerRole.Driver);
        }
        else if (Input.GetKeyUp(KeyCode.F3))
        {
            SwitchRoles(PlayerRole.MainGun);
        }

    }

    #endregion Unity Methods

    #region Public Methods

    public bool SwitchRoles(PlayerRole role)
    {
        var result = false;

        var activeController = this.gameObject.GetComponent(ActiveController);
        if (activeController != null)
        {
            GameObject.Destroy(activeController);
        }

        switch (role)
        {
            case PlayerRole.Driver:
                var driverController = this.gameObject.AddComponent<PlayerController_Driver>();
                driverController.SetupTracks(gameObject);
                ActiveController = typeof(PlayerController_Driver);
                ThirdPersonCamera.enabled = false;
                DriverCamera.enabled = true;
                MainGunCamera.enabled = false;
                break;
            case PlayerRole.MainGun:
                var mainGunController = this.gameObject.AddComponent<PlayerController_MainGun>();
                mainGunController.SetupMainGun(gameObject);
                ActiveController = typeof(PlayerController_MainGun);
                ThirdPersonCamera.enabled = false;
                DriverCamera.enabled = false;
                MainGunCamera.enabled = true;
                break;
            default:
                break;
        }

        return result;
    }

    #endregion Public Methods

    #region Public Properties

    public System.Type ActiveController;

    #endregion Public Properties

}
