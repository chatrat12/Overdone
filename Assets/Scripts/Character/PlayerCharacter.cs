using UnityEngine;

public class PlayerCharacter : Character
{
    public PlayerID ID { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerLocomotion Locomotion { get; private set; }
    public PlayerInteractDetection InteractionDetection { get; private set; }

    public PlayerCharacter(PlayerID id, CharacterAvatar avatar) : base(avatar)
    {
        ID = id;
        Input = new PlayerInput(this);
        Locomotion = new PlayerLocomotion(this);
        InteractionDetection = new PlayerInteractDetection(this);
    }

    public void Update()
    {
        Locomotion.Move(Input.MoveVector);
        Locomotion.Update();
        InteractionDetection.Update();

        if (InteractionDetection.AvailableInteraction != null)
            Debug.Log(InteractionDetection.AvailableInteraction);
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        InteractionDetection.OnDrawGizmos();
    }
#endif

    public enum PlayerID
    {
        One, Two
    }
}
