using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Destroyable : NetworkBehaviour
{
    [SyncVar]
    public int Health = 50;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision pCollision)
    {
        if (pCollision.gameObject.tag == "Bullet")
        {
            //Health -= (pCollision.gameObject as Bullet).HitValue;
            var bullet = pCollision.gameObject.GetComponent<DamageAttribute>();
            Health -= bullet.HitValue;

            if (Health >= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider pCollision)
    {
        if (pCollision.gameObject.tag == "Bullet")
        {
            //Health -= (pCollision.gameObject as Bullet).HitValue;
            var bullet = pCollision.gameObject.GetComponent<DamageAttribute>();
            Health -= bullet.HitValue;

            if (Health >= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}