using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;

using FluentAssertions;
using UnityEngine;

public class PlayerTests
{
    #region Class Level Variables

    //Player player;
    private GameObject playerPrefab;

    #endregion Class Level Variables

    #region Setup

    public PlayerTests()
    {
        //playerPrefab = UnityEngine.Object.Instantiate(Resources.Load("T105 SuperHeavy Tank", typeof(GameObject))) as GameObject;
        playerPrefab = Resources.Load("T105 SuperHeavy Tank", typeof(GameObject)) as GameObject;

    }

    #endregion Setup

    #region Tests

    [Fact]
    public void PLayer_StartWithDriverAsActiveController()
    {
        //player.ActiveController.GetType().Should().BeAssignableTo<IPlayerController>();
    }

    [Fact]
    public void Player_CanChange_ActiveController()
    {
        //player.SwitchRoles(PlayerRole.MainGun);

        //player.ActiveController.GetType().Should().BeAssignableTo<PlayerController_MainGun>();
    }

    #endregion Tests
}
