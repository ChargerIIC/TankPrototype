using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

[Serializable]
public class PlayerController_Driver : NetworkBehaviour, IPlayerController
{

    #region Class Level Variables

    MainTrack leftTrack;
    MainTrack rightTrack;

    public float acceleration = 5;

    float currentVelocity = 0;
    public float maxSpeed = 25;

    public float rotationSpeed = 30;

    #endregion

    public PlayerController_Driver()
    {
    }

    #region Unity Methods

    /// <summary>
    /// Use this for initialization
    /// </summary>
    // Instiate Tank Tracks
    void Awake()
    {
    }

    void Start()
    {

    }

    void Update()
    {
        ProcessKeyboardInput();
    }
    #endregion Unity Methods

    #region Public Methods

    public void ProcessKeyboardInput()
    {
        //var networkId = gameObject.GetComponent<NetworkIdentity>();
        //if (!networkId.isLocalPlayer)
        //    return;

        if (Input.GetKey(KeyCode.W))
        {
            // plus speed
            if (currentVelocity <= maxSpeed)
                currentVelocity += acceleration * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            // minus speed
            if (currentVelocity >= -maxSpeed)
                currentVelocity -= acceleration * Time.deltaTime;

        }
        else
        {
            // No key input. 
            if (currentVelocity > 0)
                currentVelocity -= acceleration * Time.deltaTime;
            else if (currentVelocity < 0)
                currentVelocity += acceleration * Time.deltaTime;

        }


        // Turn off engine if currentVelocity is too small. 
        if (Mathf.Abs(currentVelocity) <= 0.05)
            currentVelocity = 0;

        // Move Tank by currentVelocity
        Tank.transform.Translate(new Vector3(0, 0, currentVelocity * Time.deltaTime));

        // Move Tracks by currentVelocity	 
        if (currentVelocity > 0)
        {
            // Move forward
            leftTrack.speed = currentVelocity;
            leftTrack.GearStatus = 1;
            rightTrack.speed = currentVelocity;
            rightTrack.GearStatus = 1;
        }
        else if (currentVelocity < 0)
        {
            // Move Backward
            leftTrack.speed = -currentVelocity;
            leftTrack.GearStatus = 2;
            rightTrack.speed = -currentVelocity;
            rightTrack.GearStatus = 2;
        }
        else
        {
            // No Move
            leftTrack.GearStatus = 0;
            rightTrack.GearStatus = 0;
        }


        // Turn Tank
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.S))
            {
                // Turn right
                Tank.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

                leftTrack.speed = rotationSpeed;
                leftTrack.GearStatus = 1;
                rightTrack.speed = rotationSpeed;
                rightTrack.GearStatus = 2;

            }
            else
            {
                // Turn left
                Tank.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));

                leftTrack.speed = rotationSpeed;
                leftTrack.GearStatus = 2;
                rightTrack.speed = rotationSpeed;
                rightTrack.GearStatus = 1;

            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.S))
            {
                // Turn left
                Tank.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
                leftTrack.speed = rotationSpeed;
                leftTrack.GearStatus = 2;
                rightTrack.speed = rotationSpeed;
                rightTrack.GearStatus = 1;

            }
            else
            {
                // Turn right
                Tank.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
                leftTrack.speed = rotationSpeed;
                leftTrack.GearStatus = 1;
                rightTrack.speed = rotationSpeed;
                rightTrack.GearStatus = 2;

            }
        }

    }

    public void ProcessMouseInput()
    {

    }

    public void UpdateGameObject()
    {

    }

    public void SetupTracks(GameObject gameObj)
    {
        Tank = gameObj;
        leftTrack = GameObject.Find(gameObj.name + "/Lefttrack").GetComponent(typeof(MainTrack)) as MainTrack;
        rightTrack = GameObject.Find(gameObj.name + "/Righttrack").GetComponent(typeof(MainTrack)) as MainTrack;
    }
    #endregion Publc Methods

    #region Public Properties

    public GameObject Tank;

    #endregion Public Properties

}
