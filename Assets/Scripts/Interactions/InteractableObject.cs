using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{

    protected virtual void Start() => InteractableManager.Register(this);
    public abstract void Interact(PlayerCharacter player);
    public virtual bool CanInteract(PlayerCharacter player) => true;

    public virtual void Reset() { }
}