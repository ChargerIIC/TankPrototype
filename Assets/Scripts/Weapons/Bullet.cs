using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Class Level Variables

    public float speed = 200;
    public float range = 400;

    public GameObject ExploPtcl;

    private float dist;


    #endregion Class Level Variables

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Move Ball forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Record Distance.
        dist += Time.deltaTime * speed;

        // If reach to my range, Destroy. 
        if (dist >= range)
        {
            Instantiate(ExploPtcl, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // If hit something, Destroy. 
        Instantiate(ExploPtcl, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
