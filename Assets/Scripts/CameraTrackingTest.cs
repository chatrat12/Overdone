using UnityEngine;

public class CameraTrackingTest : MonoBehaviour
{
    [SerializeField] private Transform[] _objectsToTrack;
    [SerializeField] private CameraController _cameraController;

    private void Awake()
        => _cameraController.ObjectToTrack = _objectsToTrack;
}
