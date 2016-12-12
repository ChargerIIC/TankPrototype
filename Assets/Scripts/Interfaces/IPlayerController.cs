using UnityEngine;
using System.Collections;

public interface IPlayerController
{

    void ProcessKeyboardInput();

    void ProcessMouseInput();

    void UpdateGameObject();
}
