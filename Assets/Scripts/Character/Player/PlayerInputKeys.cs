using UnityEngine.InputSystem;

public class PlayerInputKeys
{
    private static PlayerInputControls[] _controls = new PlayerInputControls[]
    {
        // Player One
        new PlayerInputControls
        (
            Key.A,        // Left
            Key.D,        // Right
            Key.W,        // Up 
            Key.S,        // Down
            Key.LeftCtrl  // Interact
        ),
        new PlayerInputControls
        (
            Key.LeftArrow,  // Left
            Key.RightArrow, // Right
            Key.UpArrow,    // Up
            Key.DownArrow,  // Down
            Key.Numpad0     // Interact
        )
    };

    public static PlayerInputControls GetControlsForPlayer(PlayerCharacter.PlayerID playerID)
        => _controls[(int)playerID];

    public struct PlayerInputControls
    {
        public Key Left { get; private set; }
        public Key Right { get; private set; }
        public Key Up { get; private set; }
        public Key Down { get; private set; }
        public Key Interact { get; private set; }

        public PlayerInputControls(Key left, Key right, Key up, Key down, Key interact)
        {
            Left = left;
            Right = right;
            Up = up;
            Down = down;
            Interact = interact;
        }
    }
}