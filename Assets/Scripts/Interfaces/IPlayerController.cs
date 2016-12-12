using System;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

public interface IPlayerController
{

    void ProcessKeyboardInput();

    void ProcessMouseInput();

    void UpdateGameObject();
}
