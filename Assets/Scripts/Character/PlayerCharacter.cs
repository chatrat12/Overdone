using UnityEngine;

public class PlayerCharacter : Character
{
    public PlayerID ID { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerLocomotion Locomotion { get; private set; }
    public PlayerInteractDetection InteractionDetection { get; private set; }
    public PlayerHand Hand { get; private set; }

    public PlayerCharacter(PlayerID id, CharacterAvatar avatar) : base(avatar)
    {
        ID = id;
        Input = new PlayerInput(this);
        Locomotion = new PlayerLocomotion(this);
        InteractionDetection = new PlayerInteractDetection(this);
        Hand = avatar.GetComponentInChildren<PlayerHand>();
    }

    public void Update()
    {
        DoInput();
        Locomotion.Update();
        InteractionDetection.Update();

    }

    private void DoInput()
    {
        if (Application.isFocused)
        {
            Locomotion.Move(Input.MoveVector);
            if (Input.Interact && InteractionDetection.AvailableInteraction != null)
                InteractionDetection.AvailableInteraction.Interact(this);
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            InteractionDetection.OnDrawGizmos();
    }
#endif

    public enum PlayerID
    {
        One, Two
    }
}
