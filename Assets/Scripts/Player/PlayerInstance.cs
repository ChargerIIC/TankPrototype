using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInstance : NetworkBehaviour
{
    #region Class Level Variables

    private MonoBehaviour playerController;

    #endregion Class Level Variable

    #region Unity Methods

    // Use this for initialization
    void Start ()
    {
        // Disable components that should only be
        // active on the player that we control
        if (!isLocalPlayer)
        {
            disableUneededComponents();
            gameObject.layer = LayerMask.NameToLayer("Remote"); //TODO: Pull from common singleton so magic string is shared
        }
        else
        {
            // we are the local player: disable the scene camera
            var sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);

            // Disable player graphics for local player

            //Set Role

            // Create Player UI

            // Configure Player UI

        }

       //GetComponent<Player>().Setup();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    #endregion Unity Methods

    #region Private Methods
    
    private void disableUneededComponents()
    {
        //playerController.enabled = false;
        Camera.enabled = false;
    }

    #endregion Private Methods

    #region Public Methods
    /// <summary>
    /// By being instantiated in PlayerManager there is a potiental race condition between the Start() method and 
    /// the CmdSetRole method. We call this sequentially from the Player Manager
    /// </summary>
    public void Initialize()
    {
        
    }

    public void SetRole(PlayerRole role)
    {
        Debug.Log("Setting role: " + role.ToString() + ":"+ gameObject.name);
        Role = role;
        //Set Camera Location
        //Add Controller
        switch (role)
        {
            case PlayerRole.Driver:
                transform.SetParent(DriverCameraPos.transform);
                transform.localPosition = new Vector3(0, 0, 0);
                transform.Rotate(13.29f, 0, 0, 0);
                var driverController = gameObject.AddComponent<PlayerController_Driver>();
                driverController.SetupTracks(TankGameObject);

                break;
            case PlayerRole.MainGun:
                transform.SetParent(MainGunCameraPos.transform);
                transform.localPosition = new Vector3(0, 0, 0);
                transform.Rotate(0, 0, 0, 0);

                var mainGunController = gameObject.AddComponent<PlayerController_MainGun>();
                mainGunController.SetupMainGun(TankGameObject);
                break;
            default:
                break;
        }
    }

    #endregion Public Methods

    #region Public Properties

    private GameObject thirdPersonCameraPos;
    public GameObject ThirdPersonCameraPos
    {
        get { return thirdPersonCameraPos ?? (thirdPersonCameraPos = GameObject.Find("ThirdPartyCamera")); }
    }

    private GameObject driverCamera;
    public GameObject DriverCameraPos
    {
        get { return driverCamera ?? (driverCamera = GameObject.Find("DriverCamera")); }
    }

    private GameObject mainGunCameraPos;
    public GameObject MainGunCameraPos
    {
        get { return mainGunCameraPos ?? (mainGunCameraPos = GameObject.Find("MainGunCamera")); }
    }

    private Camera camera;
    public Camera Camera
    {
        get { return camera ?? (camera = GetComponent<Camera>()); }
    }

    private GameObject tankGameObject;

    public GameObject TankGameObject
    {
        get { return tankGameObject ?? (tankGameObject = GameObject.Find("T105 SuperHeavy Tank")); }
    }

    public PlayerRole Role;
    
    #endregion Public Properties
}
