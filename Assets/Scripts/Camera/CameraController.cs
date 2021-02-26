using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Camera controller that can track multiple objects        e.
public class CameraController : MonoBehaviour
{
    public IEnumerable<Transform> ObjectToTrack { get; set; } = null;

    [Range(1, 15)]
    [SerializeField] private float _distance = 2f;
    [Range(0, 45)]
    [SerializeField] private float _angle = 15f;
    [Range(0, 10)]
    [Tooltip("How many extra meters to add to the zoom region")]
    [SerializeField] private float _zoomRegionPadding = 2f;
    [Range(0, 20)]
    [Tooltip("The minimum squared size of the zoom region in meters")]
    [SerializeField] private float _minZoomRegionSize = 5f;
    
    private Camera _camera;

    private void Awake()
        => _camera = GetComponent<Camera>();

    // Camera code goes in late update
    private void LateUpdate()
    {
        // Nothing to track, don't do anthing
        if (ObjectToTrack == null || !ObjectToTrack.Any())
            return;

        // Get the bounds of the objects that we are tracking.
        var bounds = GetBounds();

        UpdateTransform(bounds);
        UpdateFOV(bounds);
    }

    private Bounds GetBounds()
    {
        // Calculate bound of all objects we are tracking.
        var result = new Bounds(ObjectToTrack.First().position, Vector3.zero);
        foreach (var obj in ObjectToTrack)
            result.Encapsulate(obj.position);

        // Add zoom region padding to bounds
        var size = result.size;
        size.x += _zoomRegionPadding;
        size.z += _zoomRegionPadding;
        result.size = size;

        return result;
    }

    private void UpdateTransform(Bounds bounds)
    {
        // Center camera 
        var pos = bounds.center;
        // Normal to offset the camera by
        var normal = Quaternion.AngleAxis(-_angle, Vector3.right) * Vector3.up;
        // Back the camera by the normal times the specified distance.
        pos += normal * _distance;

        transform.position = pos;
        // Update so it's forward vector looks along the normal we defined in the previous step
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, -normal);
    }

    private void UpdateFOV(Bounds bounds)
    {
        // Region width and height comes from the bounds we calculated.
        _camera.fieldOfView = GetTargetFOV(bounds.size.x, bounds.size.z, _distance);
    }

    private float GetTargetFOV(float regionWidth, float regionHeight, float distance)
    {
        // Figure out the target frustum height
        var targetHeight = Mathf.Max(regionHeight, regionWidth * _camera.aspect);
        // Make sure our target frustum height is larger than the min zoom region size
        targetHeight = Mathf.Max(_minZoomRegionSize, targetHeight);
        // Generate FOV from target height and camera distance.
        return 2.0f * Mathf.Atan(targetHeight * 0.5f / distance) * Mathf.Rad2Deg;
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Draw bounds of tracked object for debugging purposes
        var bounds = GetBounds();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        // Restore default Gizmo color.0
        Gizmos.color = Color.white;
    }
#endif
}
