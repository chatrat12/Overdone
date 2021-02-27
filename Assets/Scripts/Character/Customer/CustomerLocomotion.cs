using System.Threading.Tasks;
using UnityAsync;
using UnityEngine;

// Handles locomotion for customer
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

    // Async method for moving character.
    // _cancelWalk can be set high to break out of the walk.
    public async Task MoveTo(Vector3 position)
    {
        var t = _customer.Avatar.transform;
        // Get final direction
        var endDirection = (position - t.position).normalized;

        await Await.Until(() =>
        {
            if (_cancelWalk)
                return true;
            // Get direction to look and walk
            var dir = (position - t.position).normalized;
            // move forward
            t.position += dir * Time.deltaTime * GameSettings.CustomerMoveSpeed;
            var targetRot = Quaternion.LookRotation(dir);
            // Update rotation
            t.rotation = Quaternion.RotateTowards(t.rotation, targetRot, 540 * Time.deltaTime);
            // Return true if we are within the stopping distance.
            return Vector3.Distance(t.position, position) < STOPPING_DISTANCE;
        });
        // Reset cancel walk
        _cancelWalk = false;
    }

    public async void Reset()
    {
        Stop();
        // Put character at start position
        _customer.Avatar.transform.position = _startPosition;
        _customer.Avatar.transform.rotation = _startRotation;
       
        // HACK: wait a frame, and set cancel walk low.
        await Await.NextLateUpdate();
        _cancelWalk = false;
    }

    public void Stop() => _cancelWalk = true;
}
