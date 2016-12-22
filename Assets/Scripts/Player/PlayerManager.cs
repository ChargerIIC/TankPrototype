using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity, PlayerSharedObject.transform);
        player.name = "Player " + conn.connectionId + " - " + playerControllerId + ":" + conn.address;
        Debug.Log("Adding Player - " + player.name);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

        var role = findAvailableRole(player.name);
        //player.GetComponent<PlayerInstance>().SetRole(role);
        setRole(player, role);
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
        var slot = Players.First(kvp => kvp.Value == player.gameObject.name).Key;
        Players[slot] = string.Empty;
        Debug.Log("Removing Player - " + player.gameObject.name);
        base.OnServerRemovePlayer(conn, player);
    }


    private void setRole(GameObject player, PlayerRole role)
    {
        var playerInstance = player.GetComponent<PlayerInstance>();
        Debug.Log("Setting role: " + role.ToString() + ":" + player.name);
        playerInstance.Role = role;
        //Set Camera Location
        //Add Controller
        switch (role)
        {
            case PlayerRole.Driver:
                player.transform.SetParent(playerInstance.DriverCameraPos.transform);
                player.transform.localPosition = new Vector3(0, 0, 0);
                player.transform.Rotate(13.29f, 0, 0, 0);
                var driverController = player.AddComponent<PlayerController_Driver>();
                driverController.SetupTracks(playerInstance.TankGameObject);

                break;
            case PlayerRole.MainGun:
                player.transform.SetParent(playerInstance.MainGunCameraPos.transform);
                player.transform.localPosition = new Vector3(0, 0, 0);
                player.transform.Rotate(0, 0, 0, 0);

                var mainGunController = player.AddComponent<PlayerController_MainGun>();
                mainGunController.SetupMainGun(playerInstance.TankGameObject);
                break;
            default:
                Debug.Log("No Player role to set for " + player.name);
                break;
        }
    }

    private PlayerRole findAvailableRole(string playerId)
    {
        if (Players.ContainsValue(playerId)) //Slot already assigned
            return Players.First(kvp => kvp.Value == playerId).Key;

        if (Players.All(kvp => !String.IsNullOrEmpty(kvp.Value))) //No Slots left!
            return PlayerRole.None;


        var roleToTake = Players.First(kvp => String.IsNullOrEmpty(kvp.Value)).Key;
        Players[roleToTake] = playerId; //take the slot
        return roleToTake;
    }

    #region Public Properties

    public GameObject PlayerSharedObject;

    public PlayerRoleTrackingDictionary Players;

    #endregion Public Properties
}
