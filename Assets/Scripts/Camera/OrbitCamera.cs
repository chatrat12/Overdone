using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] float _distance = 10;
    [SerializeField] float _speed = 180;
    [SerializeField] float _centerY = 1;
    [SerializeField] float _cameraY = 2;

    private Vector3 _pivotPoint;
    private float _angle = 0;

    void Start()
    {
        _pivotPoint = transform.position;
    }

    void Update()
    {
        _pivotPoint.y = _centerY;

        var normal = Quaternion.AngleAxis(_angle, Vector3.up) * Vector3.back;
        var targetPos = _pivotPoint + normal * _distance;
        targetPos.y = _cameraY;

        transform.position = targetPos;
        transform.LookAt(_pivotPoint);
        
        _angle += _speed * Time.deltaTime;
    }
}
