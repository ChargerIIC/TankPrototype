using UnityEngine;
using System.Collections;
using System;
using SPStudios;
using System.Collections.Generic;

[Serializable]
public class PlayerControllerDictionary : SerializableDictionary<PlayerRole, IPlayerController>
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
