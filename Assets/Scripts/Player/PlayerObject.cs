using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class PlayerObject : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    ActiveController = typeof(PlayerController_Driver);
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
                break;
            case PlayerRole.MainGun:
                var mainGunController = this.gameObject.AddComponent<PlayerController_MainGun>();
                mainGunController.SetupMainGun(gameObject);
                ActiveController = typeof(PlayerController_MainGun);
                break;
            default:
                break;
        }

        return result;
    }

    public System.Type ActiveController;

}
