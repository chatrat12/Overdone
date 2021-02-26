public class InteractableGarbageCan : InteractableObject
{
    public override void Interact(PlayerCharacter player)
    {
        if(player.Hand.HasItem)
            Destroy(player.Hand.RemoveItem().gameObject);
    }
}
