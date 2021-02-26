using System.Linq;
using UnityEngine;

// Finds interactable objects in front of a player
public class PlayerInteractDetection
{
    public InteractableObject AvailableInteraction { get; private set; }

    private static Vector3 _sphereOffset = Vector3.forward * 0.5f + Vector3.up * 0.25f;
    private static float _sphereRadius = 0.5f;

    // Buffer to hold objects overlapped
    private Collider[] _buffer = new Collider[10];
    private PlayerCharacter _player;

    public PlayerInteractDetection(PlayerCharacter player)
    {
        _player = player;
    }

    public void Update()
    {
        // Convert sphere offset from local space to world space
        var pos = _player.Avatar.transform.localToWorldMatrix.MultiplyPoint(_sphereOffset);
        // Use overlap sphere to find objects.
        var count = Physics.OverlapSphereNonAlloc(pos, _sphereRadius, _buffer);

        // Query the correct objects
        AvailableInteraction = _buffer.Where((o, i) => i < count)
                                      .Select(o => o.GetComponent<InteractableObject>())
                                      .Where(io => io != null && io.CanInteract(_player))
                                      .FirstOrDefault();
    }

#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        var pos = _player.Avatar.transform.localToWorldMatrix.MultiplyPoint(_sphereOffset);
        Gizmos.DrawWireSphere(pos, _sphereRadius);
    }
#endif
}