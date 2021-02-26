public class PlayerCharacter : Character
{
    public PlayerID ID { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerLocomotion Locomotion { get; private set; }

    public PlayerCharacter(PlayerID id, CharacterAvatar avatar) : base(avatar)
    {
        ID = id;
        Input = new PlayerInput(this);
        Locomotion = new PlayerLocomotion(this);
    }

    public void Update()
    {
        Locomotion.Move(Input.MoveVector);
        Locomotion.Update();
    }

    public enum PlayerID
    {
        One, Two
    }
}
