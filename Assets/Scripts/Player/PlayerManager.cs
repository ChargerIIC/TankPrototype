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
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.name = "Player " + conn.connectionId + " - " + playerControllerId + ":" + conn.address;
        var role = findAvailableRole(Convert.ToInt16(conn.connectionId));
        
        player.transform.SetParent(PlayerSharedObject.transform);

        player.GetComponent<PlayerInstance>().SetRole(role);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    private PlayerRole findAvailableRole(short playerId)
    {
        if (Players.ContainsValue(playerId)) //Slot already assigned
            return Players.First(kvp => kvp.Value == playerId).Key;

        if (Players.All(kvp => kvp.Value != -1)) //No Slots left!
            return PlayerRole.None;


        var roleToTake = Players.First(kvp => kvp.Value == -1).Key;
        Players[roleToTake] = playerId; //take the slot
        return roleToTake;
    }

    #region Public Properties

    public GameObject PlayerSharedObject;

    public PlayerRoleTrackingDictionary Players;

    #endregion Public Properties
}
