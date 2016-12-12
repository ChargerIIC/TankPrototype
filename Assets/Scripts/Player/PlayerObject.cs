using UnityEngine;
using System.Collections;

public class PlayerObject : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    PlayerClass = new Player(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Player PlayerClass;
}
