using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10)]
public class InteractableManager : MonoBehaviour
{
    private static InteractableManager _instance;
    private List<InteractableObject> _interactables = new List<InteractableObject>();

    private void Awake()
    {
        _instance = this;
    }

    public static void Register(InteractableObject interactable)
        => _instance._interactables.Add(interactable);
    public static void Reset()
    {
        foreach (var interactable in _instance._interactables)
            interactable.Reset();
    }
}
