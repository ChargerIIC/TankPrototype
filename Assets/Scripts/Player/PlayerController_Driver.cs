using UnityEngine;
using System.Collections;
using System;

public class PlayerController_Driver : IPlayerController
{

    #region Class Level Variables

    MoveTrack leftTrack;
    MoveTrack rightTrack;

    public float acceleration = 5;

    float currentVelocity = 0;
    public float maxSpeed = 25;

    public float rotationSpeed = 30;
    private GameObject playerObject;

    #endregion

    #region Unity Methods
    /// <summary>
    /// Use this for initialization
    /// </summary>
    // Instiate Tank Tracks
    public PlayerController_Driver(GameObject gameObject)
    {
        playerObject = gameObject;
        // Get Track Controls
        leftTrack = GameObject.Find(gameObject.name + "/Lefttrack").GetComponent(typeof(MoveTrack)) as MoveTrack;
        rightTrack = GameObject.Find(gameObject.name + "/Righttrack").GetComponent(typeof(MoveTrack)) as MoveTrack;

    }

    #endregion Unity Methods

    #region Public Methods

    public void ProcessKeyboardInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // plus speed
            if (currentVelocity <= maxSpeed)
                currentVelocity += acceleration * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.DownArrow))
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
        playerObject.transform.Translate(new Vector3(0, 0, currentVelocity * Time.deltaTime));

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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                // Turn right
                playerObject.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

                leftTrack.speed = rotationSpeed;
                leftTrack.GearStatus = 1;
                rightTrack.speed = rotationSpeed;
                rightTrack.GearStatus = 2;

            }
            else
            {
                // Turn left
                playerObject.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));

                leftTrack.speed = rotationSpeed;
                leftTrack.GearStatus = 2;
                rightTrack.speed = rotationSpeed;
                rightTrack.GearStatus = 1;

            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                // Turn left
                playerObject.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
                leftTrack.speed = rotationSpeed;
                leftTrack.GearStatus = 2;
                rightTrack.speed = rotationSpeed;
                rightTrack.GearStatus = 1;

            }
            else
            {
                // Turn right
                playerObject.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
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
    #endregion Publc Methods
}
