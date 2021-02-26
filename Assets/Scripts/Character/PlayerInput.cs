using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    public Vector3 MoveVector
    {
        get
        {
            var result = Vector3.zero;
            result.x += Keyboard.current[_controls.Left] .isPressed ? -1 : 0;
            result.x += Keyboard.current[_controls.Right].isPressed ?  1 : 0;
            result.z += Keyboard.current[_controls.Up]   .isPressed ?  1 : 0;
            result.z += Keyboard.current[_controls.Down] .isPressed ? -1 : 0;
            return result.normalized;
        }
    }

    public bool Interact => Keyboard.current[_controls.Interact].wasPressedThisFrame;

    private PlayerInputKeys.PlayerInputControls _controls;

    public PlayerInput(PlayerCharacter player)
    {
        _controls = PlayerInputKeys.GetControlsForPlayer(player.ID);
    }

    

}
