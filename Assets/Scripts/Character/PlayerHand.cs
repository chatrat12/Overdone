using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public ItemModel CurrentItem { get; private set; }
    public bool HasItem => CurrentItem != null;

    public void PlaceItem(ItemModel item)
    {
        if (CanPlaceItem(item))
        {
            CurrentItem = item;
            item.transform.parent = transform;
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