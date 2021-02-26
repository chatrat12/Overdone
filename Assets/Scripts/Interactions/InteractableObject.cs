using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public virtual string InteractMessage => "Interact";
    public abstract void Interact(PlayerCharacter player);
    public virtual bool CanInteract(PlayerCharacter player) => true;
}