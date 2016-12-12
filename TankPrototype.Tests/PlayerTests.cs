using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;

using FluentAssertions;
using UnityEngine;

public class PlayerTests
{
    #region Class Level Variables

    Player player;

    #endregion Class Level Variables

    #region Setup

    public PlayerTests()
    {
        player = new Player(new GameObject());
    }

    #endregion Setup

    #region Tests

    [Fact]
    public void PLayer_StartWithDriverAsActiveController()
    {
        player.ActiveController.GetType().Should().BeAssignableTo<IPlayerController>();
    }

    [Fact]
    public void Player_CanChange_ActiveController()
    {
        player.SwitchRoles(PlayerRole.MainGun);

        player.ActiveController.GetType().Should().BeAssignableTo<PlayerController_MainGun>();
    }

    #endregion Tests
}
