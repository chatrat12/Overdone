
using UnityEngine;

public class InteractableCuttingStation : InteractableCounterTop
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }



    public override void Interact(PlayerCharacter player)
    {
        _animator.SetTrigger("Interact");
    }
}
