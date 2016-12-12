using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization;
using UnityEditor;

public class PlayerController_MainGun : MonoBehaviour, IPlayerController
{
    public Transform spawnPoint;
    public GameObject bulletObject;
    public GameObject fireEffect;
    private GameObject Turret;
    private MainGun Cannon;

    public float turretSpeed = 30f;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessKeyboardInput();
    }

    public void SetupMainGun(GameObject gameObj)
    {
        Turret = GameObject.Find(gameObject.name + "/Turret");
        Cannon = Turret.GetComponent(typeof(MainGun)) as MainGun;
        
        spawnPoint = Cannon.SpawnPoint;
        bulletObject = Resources.Load("Bullet", typeof(GameObject)) as GameObject;
        fireEffect = Resources.Load("FireEffect_MainGun", typeof(GameObject)) as GameObject;
    }

    public void ProcessKeyboardInput()
    {
        // Fire!
        if (Input.GetButtonDown("Fire1"))
        {
            // make fire effect.
            Instantiate(fireEffect, spawnPoint.position, spawnPoint.rotation);

            // make ball
            Instantiate(bulletObject, spawnPoint.position, spawnPoint.rotation);
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
}
