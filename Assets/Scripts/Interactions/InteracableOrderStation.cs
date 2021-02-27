using UnityEngine;

public class InteracableOrderStation : InteractableObject
{
    public OrderStationState State { get; private set; } = OrderStationState.Empty;
    
    [SerializeField] private CustomerAvatar _customer;
    [SerializeField] private Transform _customerOrderPosition;

    private Vector3 _customerStartPosition;

    private void Awake()
    {
        _customerStartPosition = _customer.transform.position;
    }

    private void Start()
    {
        AddCustomer();
    }

    public async void AddCustomer()
    {
        if (State != OrderStationState.Empty) return;
        State = OrderStationState.Entering;
        await _customer.Customer.Locomotion.MoveTo(_customerOrderPosition.position);
        State = OrderStationState.WaitingForOrder;
        Debug.Log("Place Order");
    }
    
    public async void CustomerLeave()
    {
        State = OrderStationState.Leaving;
        await _customer.Customer.Locomotion.MoveTo(_customerStartPosition);
        State = OrderStationState.Empty;
    }

    public override void Interact(PlayerCharacter player)
    {
        
    }

    public enum OrderStationState
    {
        Empty,
        Entering,
        WaitingForOrder,
        Leaving
    }
}
