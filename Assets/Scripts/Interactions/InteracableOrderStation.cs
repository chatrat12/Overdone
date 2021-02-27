using UnityEngine;

public class InteracableOrderStation : InteractableObject
{
    public OrderStationState State { get; private set; } = OrderStationState.Empty;
    public CustomerCharacter Customer => _customerAvatar.Customer;

    [SerializeField] private CustomerAvatar _customerAvatar;
    [SerializeField] private Transform _customerOrderPosition;

    private Vector3 _customerStartPosition;

    protected override void Start()
    {
        base.Start();
        _customerStartPosition = _customerAvatar.transform.position;
    }

    private void Update()
    {
        if (State == OrderStationState.WaitingForOrder)
        {
            // Time has expired
            if (!Customer.Order.OrderComplete && Customer.Order.TimeExpired)
            {
                // Penalize Players
                Customer.Order.PenalizePlayers();
                // Leave
                CustomerLeave();
            }
        }
    }

    public async void AddCustomer(bool teleport = false)
    {
        if (State != OrderStationState.Empty) return;
        State = OrderStationState.Entering;
        // Make customer visible
        Customer.Avatar.gameObject.SetActive(true);
        if (!teleport)
            await Customer.Locomotion.MoveTo(_customerOrderPosition.position);
        else
        {
            _customerAvatar.transform.position = _customerOrderPosition.position;
            _customerAvatar.transform.rotation = _customerOrderPosition.rotation;
        }

        // Hack so order UI does not get updated after game over.
        if (GameController.Instance.State == GameController.StateType.Playing)
        {
            var dish = GameSettings.SaladDishes[Random.Range(0, GameSettings.SaladDishes.Length)];
            Customer.PlaceOrder(dish);
            State = OrderStationState.WaitingForOrder;
        }
    }

    public async void CustomerLeave()
    {
        Customer.RemoveOrder();
        State = OrderStationState.Leaving;
        await Customer.Locomotion.MoveTo(_customerStartPosition);
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
            if (Customer.Order.SubmitOrder(player.Hand.RemoveItem(), player))
            {
                // Add points;
                CustomerLeave();
            }
        }
    }

    public override void Reset()
    {
        State = OrderStationState.Empty;
        Customer.Reset();
        base.Reset();
    }

    public enum OrderStationState
    {
        Empty,
        Entering,
        WaitingForOrder,
        Leaving
    }
}
