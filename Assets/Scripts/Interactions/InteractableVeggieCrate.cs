using UnityEngine;

public class InteractableVeggieCrate : InteractableObject
{
    private Animator _animator;
    [SerializeField] private VeggieModel _veggieModel;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Interact(PlayerCharacter player)
    {
        if(!player.Hand.HasItem)
        {
            var veggie = Instantiate(_veggieModel);
            player.Hand.PlaceItem(veggie);
            _animator.SetTrigger("Interact");
        }
    }
}
