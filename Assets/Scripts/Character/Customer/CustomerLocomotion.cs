using System;
using System.Threading.Tasks;
using UnityAsync;
using UnityEngine;

public class CustomerLocomotion
{
    private const float STOPPING_DISTANCE = 0.05f;

    private CustomerCharacter _customer;
    private bool _cancelWalk = false;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    public CustomerLocomotion(CustomerCharacter customer)
    {
        _customer = customer;
        _startPosition = customer.Avatar.transform.position;
        _startRotation = customer.Avatar.transform.rotation;
    }

    public async Task MoveTo(Vector3 position)
    {
        var t = _customer.Avatar.transform;
        var endDirection = (position - t.position).normalized;

        await Await.Until(() =>
        {
            if (_cancelWalk)
                return true;
            var dir = (position - t.position).normalized;
            t.position += dir * Time.deltaTime * GameSettings.CustomerMoveSpeed;
            var targetRot = Quaternion.LookRotation(dir);
            t.rotation = Quaternion.RotateTowards(t.rotation, targetRot, 540 * Time.deltaTime);
            return Vector3.Distance(t.position, position) < STOPPING_DISTANCE;
        });
        if (!_cancelWalk)
            t.rotation = Quaternion.LookRotation(endDirection);
        _cancelWalk = false;
    }

    public async void Reset()
    {
        Stop();
        _customer.Avatar.transform.position = _startPosition;
        _customer.Avatar.transform.rotation = _startRotation;
        await Await.NextLateUpdate();
        _cancelWalk = false;
    }

    public void Stop() => _cancelWalk = true;
}
