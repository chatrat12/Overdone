public class CustomerCharacter : Character
{
    public CustomerLocomotion Locomotion { get; private set; }

    public CustomerCharacter(CharacterAvatar avatar) : base(avatar)
    {
        Locomotion = new CustomerLocomotion(this);
    }
}
