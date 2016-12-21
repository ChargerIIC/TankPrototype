using System;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

public interface IPlayerController
{

    void CmdProcessKeyboardInput();

    void ProcessMouseInput();

    void UpdateGameObject();
}
