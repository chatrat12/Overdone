using UnityEngine;

public class InteractableCounterTop : InteractableObject
{
    public ItemModel CurrentItem { get; private set; }

    [SerializeField] private Transform _itemHolder;

    public override void Interact(PlayerCharacter player)
    {
        // Player has item in hand
        if(player.Hand.HasItem)
        {
            // If there is no item on the counter, place the item there
            if(CurrentItem == null && CanPlaceItem(player.Hand.CurrentItem))
                PlaceItem(player.Hand.RemoveItem());
            // If instead the item is a plate
            else if(CurrentItem is PlateModel plate)
            {
                // Does the player have a sliced veggie that can be added to the plate
                if(player.Hand.CurrentItem is VeggieModel veggie && veggie.Sliced && plate.CanAddVeggie(veggie.Type))
                {
                    // Add sliced veggie to plate
                    plate.AddVeggie(veggie.Type);
                    // Destroy veggie model
                    Destroy(veggie.gameObject);
                }
            }
        }
        // Player hand is empty and current item is not null
        else if(CurrentItem != null)
            // Have player pick up item
            player.Hand.PlaceItem(RemoveItem());
    }

    public void PlaceItem(ItemModel item)
    {
        if (CanPlaceItem(item))
        {
            CurrentItem = item;
            item.transform.parent = _itemHolder;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }
    }

    public virtual bool CanPlaceItem(ItemModel item) => CurrentItem == null;

    public ItemModel RemoveItem()
    {
        if (CurrentItem != null)
        {
            var result = CurrentItem;
            CurrentItem.transform.SetParent(null);
            CurrentItem = null;
            return result;
        }
        return null;
    }
}