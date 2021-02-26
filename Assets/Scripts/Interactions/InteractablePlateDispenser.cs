using UnityEngine;

public class InteractablePlateDispenser : InteractableObject
{
    [SerializeField] private PlateModel _platePrefab;
    public override void Interact(PlayerCharacter player)
    {
        if (!player.Hand.HasItem)
            player.Hand.PlaceItem(Instantiate(_platePrefab));
    }
}