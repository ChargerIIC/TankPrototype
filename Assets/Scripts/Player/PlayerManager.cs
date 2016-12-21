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
        var role = findAvailableRole(player);
        
        player.transform.SetParent(PlayerSharedObject.transform);
        
        player.GetComponent<PlayerInstance>().SetRole(role);

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    private PlayerRole findAvailableRole(GameObject player)
    {
        if(Players.Any(kvp => kvp.Value == null))
        {
            var roleToTake = Players.First(kvp => kvp.Value == null).Key;
            Players[roleToTake] = player; //take the slot
            return roleToTake;
        }

        return PlayerRole.None;
    }

    #region Public Properties

    public GameObject PlayerSharedObject;

    public PlayerControllerDictionary Players;

    #endregion Public Properties
}
