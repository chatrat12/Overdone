using UnityEngine;

public class InteractableVeggieCrate : InteractableObject
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Interact(PlayerCharacter player)
    {
        _animator.SetTrigger("Interact");
    }
}
