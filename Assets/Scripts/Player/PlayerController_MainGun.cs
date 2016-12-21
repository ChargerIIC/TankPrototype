using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;
using UnityEngine.Networking;

public class PlayerController_MainGun : NetworkBehaviour, IPlayerController
{
    public Transform spawnPoint;
    public GameObject bulletObject;
    public GameObject fireEffect;
    private Transform Turret;
    private MainGun Cannon;

    public float turretSpeed = 30f;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        CmdProcessKeyboardInput();
    }

    public void SetupMainGun(GameObject gameObj)
    {
        Tank = gameObj;
        Cannon = Tank.GetComponentInChildren<MainGun>(); 
        Turret = Cannon.gameObject.transform;
        
        spawnPoint = Cannon.SpawnPoint;
        bulletObject = Resources.Load("Bullet", typeof(GameObject)) as GameObject;
        fireEffect = Resources.Load("FireEffect_MainGun", typeof(GameObject)) as GameObject;
    }

    [Command]
    public void CmdProcessKeyboardInput()
    {
        if (!isLocalPlayer)
            return;

        // Fire!
        if (Input.GetButtonDown("Fire1"))
        {
            // make fire effect.
            Instantiate(fireEffect, spawnPoint.position, spawnPoint.rotation);

            // make ball
            var bullet = Instantiate(bulletObject, spawnPoint.position, spawnPoint.rotation);
            NetworkServer.Spawn(bullet);
        }

        // Turn Right
        if (Input.GetKey(KeyCode.D))
            Turret.transform.Rotate(new Vector3(0, turretSpeed * Time.deltaTime, 0));

        // Turn Left
        if (Input.GetKey(KeyCode.A))
            Turret.transform.Rotate(new Vector3(0, -turretSpeed * Time.deltaTime, 0));

        // Gun Down
        if (Input.GetKey(KeyCode.W))
        {
            if (Cannon.CurRotation > -5)
            {
                Cannon.CannonTransform.Rotate(new Vector3(Cannon.Speed * Time.deltaTime, 0, 0));
                Cannon.CurRotation -= Cannon.Speed * Time.deltaTime;
            }
        }
        
        // Gun Up
        if (Input.GetKey(KeyCode.S))
        {
            if (Cannon.CurRotation < 45)
            {
                Cannon.CannonTransform.Rotate(new Vector3(-Cannon.Speed * Time.deltaTime, 0, 0));
                Cannon.CurRotation += Cannon.Speed * Time.deltaTime;
            }
        }

    }

    public void ProcessMouseInput()
    {
    }

    public void UpdateGameObject()
    {
    }

    #region Public Properties

    public GameObject Tank;

    #endregion Public Properties
}
