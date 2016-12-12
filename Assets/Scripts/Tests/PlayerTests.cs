using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;

public class PlayerTests 
{
    #region Class Level Variables

    Player player;

    #endregion Class Level Variables

    #region Setup

    public PlayerTests()
    {
        player = new Player();
    }

    #endregion Setup

    #region Tests

    [Test]
    public void PLayer_StartWithDriverAsActiveController()
    {
        Assert.IsTrue(player.ActiveController.GetType() is IPlayerController);
    }

    [Test]
    public void Player_CanChange_ActiveController()
    {
        player.SwitchRoles(PlayerRole.MainGun);

        Assert.IsInstanceOfType(typeof(PlayerController_MainGun),player.ActiveController);
    }

    #endregion Tests
}
