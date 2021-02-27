using System.Threading.Tasks;
using UnityAsync;
using UnityEngine;

public class CustomerLocomotion
{
    private const float STOPPING_DISTANCE = 0.05f;

    private CustomerCharacter _customer;

    public CustomerLocomotion(CustomerCharacter customer)
    {
        _customer = customer;
    }

    public async Task MoveTo(Vector3 position)
    {
        var t = _customer.Avatar.transform;
        var endDirection = (position - t.position).normalized;

        await Await.Until(() =>
        {
            var dir = (position - t.position).normalized;
            t.position += dir * Time.deltaTime * GameSettings.CustomerMoveSpeed;
            var targetRot = Quaternion.LookRotation(dir);
            t.rotation = Quaternion.RotateTowards(t.rotation, targetRot, 540 * Time.deltaTime);
            return Vector3.Distance(t.position, position) < STOPPING_DISTANCE;
        });
        //t.position = position;
        t.rotation = Quaternion.LookRotation(endDirection);
    }
}
