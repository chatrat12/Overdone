using UnityEngine;

public class PlayerCharacter : Character
{
    public PlayerID ID { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerLocomotion Locomotion { get; private set; }
    public PlayerInteractDetection InteractionDetection { get; private set; }
    public PlayerHand Hand { get; private set; }
    public PlayerScore Score { get; private set; }
    public PlayerTime Time { get; private set; }

    public PlayerCharacter(PlayerID id, CharacterAvatar avatar) : base(avatar)
    {
        ID = id;
        Input = new PlayerInput(this);
        Locomotion = new PlayerLocomotion(this);
        InteractionDetection = new PlayerInteractDetection(this);
        Hand = avatar.GetComponentInChildren<PlayerHand>();
        Score = new PlayerScore();
        Time = new PlayerTime();

        Reset();

        Time.Start();
    }

    public void Reset()
    {
        Score.Reset();
        Time.Reset();
        Hand.Reset();
        Locomotion.Reset();
        Time.Start();
    }

    public void Update()
    {
        Time.Update();
        DoInput();
        Locomotion.Update();
        InteractionDetection.Update();
    }

    private void DoInput()
    {
        var playing = GameController.Instance.State == GameController.StateType.Playing;
        if (playing && Application.isFocused && !Time.Expired)
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
