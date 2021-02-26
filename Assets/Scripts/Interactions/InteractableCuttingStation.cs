using UnityAsync;
using UnityEngine;

public class InteractableCuttingStation : InteractableCounterTop
{
    private Animator _animator;
    private bool _cutting = false;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public override async void Interact(PlayerCharacter player)
    {
        // If someone is already using this board, don't do anything
        if (_cutting) return;

        // If player hand is empty and a veggie is ready to chop, chop veggie
        if (!player.Hand.HasItem && CurrentItem is VeggieModel veggie && !veggie.Sliced)
        {
            // Disable player movement
            player.Locomotion.Disable();
            _cutting = true;
            // Trigger cutting animation
            _animator.SetTrigger("Interact");
            // Wait until cutting flag is flipped low. This will happen via 
            // an animation event.
            await Await.Until(() => !_cutting);
            // Slice the veggie :D
            veggie.Slice();
            // Enable player movement
            player.Locomotion.Enable();
        }
        // Fallback to default behavior.
        else
            base.Interact(player);
    }

    // Can only place sliced veggies
    public override bool CanPlaceItem(ItemModel item)
        => base.CanPlaceItem(item) && item is VeggieModel veggie && !veggie.Sliced;

    // Animation Event: Finished Cutting
    private void AnimEvent_FinishedCutting() => _cutting = false;
}