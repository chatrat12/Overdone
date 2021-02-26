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
        // If player hand is empty and a veggie is ready to chop, chop veggie
        if (!player.Hand.HasItem && CurrentItem is VeggieModel veggie && !veggie.Sliced)
        {
            _animator.SetTrigger("Interact");
            veggie.Slice();
        }
        // Fallback to default behavior.
        else
            base.Interact(player);
    }

    // Can only place sliced veggies
    public override bool CanPlaceItem(ItemModel item)
        => base.CanPlaceItem(item) && item is VeggieModel veggie && !veggie.Sliced;
}
