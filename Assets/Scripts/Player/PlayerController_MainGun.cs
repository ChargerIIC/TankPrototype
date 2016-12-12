using UnityEngine;
using System.Collections;

public class PlayerController_MainGun : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bulletObject;
    public GameObject fireEffect;
    public float turretSpeed = 30f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
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
            transform.Rotate(new Vector3(0, turretSpeed * Time.deltaTime, 0));

        // Turn Left
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, -turretSpeed * Time.deltaTime, 0));


    }
}
