using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        PlayerSharedObject.GetComponent<PlayerObject>().AddPlayer(player, PlayerRole.None);
        //player.GetComponent<Player>().color = Color.Red;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    #region Public Properties

    public GameObject PlayerSharedObject;

    #endregion Public Properties
}
