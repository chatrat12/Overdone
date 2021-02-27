using UnityEngine;

public class InteracableOrderStation : InteractableObject
{
    public OrderStationState State { get; private set; } = OrderStationState.Empty;

    [SerializeField] private CustomerAvatar _customerAvatar;
    [SerializeField] private Transform _customerOrderPosition;

    private CustomerCharacter _customer => _customerAvatar.Customer;
    private Vector3 _customerStartPosition;

    private void Start()
    {
        _customerStartPosition = _customerAvatar.transform.position;
    }

    private void Update()
    {
        if (State == OrderStationState.WaitingForOrder)
        {
            if (!_customer.Order.OrderComplete && _customer.Order.TimeExpired)
            {
                // Remove points
                CustomerLeave();
            }
        }
    }

    public async void AddCustomer(bool teleport = false)
    {
        if (State != OrderStationState.Empty) return;
        State = OrderStationState.Entering;
        if (!teleport)
            await _customer.Locomotion.MoveTo(_customerOrderPosition.position);
        else
        {
            _customerAvatar.transform.position = _customerOrderPosition.position;
            _customerAvatar.transform.rotation = _customerOrderPosition.rotation;
        }

        var dish = GameSettings.SaladDishes[Random.Range(0, GameSettings.SaladDishes.Length)];
        _customer.PlaceOrder(dish, GameSettings.OrderTime);
        State = OrderStationState.WaitingForOrder;
    }

    public async void CustomerLeave()
    {
        _customer.RemoveOrder();
        State = OrderStationState.Leaving;
        await _customer.Locomotion.MoveTo(_customerStartPosition);
        State = OrderStationState.Empty;
    }

    public override void Interact(PlayerCharacter player)
    {
        // Can only interact if customer is waiting for order
        if (State != OrderStationState.WaitingForOrder)
            return;

        // Does player have item
        if (player.Hand.HasItem)
        {
            if (_customer.Order.SubmitOrder(player.Hand.RemoveItem()))
            {
                // Add points;
                CustomerLeave();
            }
        }
    }

    public enum OrderStationState
    {
        Empty,
        Entering,
        WaitingForOrder,
        Leaving
    }
}
