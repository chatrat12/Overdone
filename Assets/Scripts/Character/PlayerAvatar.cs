using UnityEngine;

public class PlayerAvatar : CharacterAvatar
{
    public PlayerCharacter Player { get; private set; }

    [SerializeField] private PlayerCharacter.PlayerID _id;

    private void Awake()
    {
        Player = new PlayerCharacter(_id, this);
    }

    private void Update()
        => Player.Update();
}
