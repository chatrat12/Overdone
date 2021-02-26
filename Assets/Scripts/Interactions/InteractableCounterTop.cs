using UnityEngine;

public class InteractableCounterTop : InteractableObject
{
    public override void Interact(PlayerCharacter player)
    {
        Debug.Log("Interacted");
    }
}
