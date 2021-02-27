using UnityEngine;

public class CustomerCharacter : Character
{
    public CustomerLocomotion Locomotion { get; private set; }
    public CustomerOrder Order { get; private set; }
    public OrderingUI OrderUI { get; private set; }

    public CustomerCharacter(CharacterAvatar avatar) : base(avatar)
    {
        Locomotion = new CustomerLocomotion(this);
        OrderUI = avatar.transform.parent.GetComponentInChildren<OrderingUI>();
    }

    public void PlaceOrder(SaladDish dish, float time)
    {
        Order = new CustomerOrder(dish, time);
        OrderUI.Order = Order;
    }

    public void RemoveOrder()
    {
        Order = null;
        OrderUI.Order = null;
    }

    public void Update()
    {
        Order?.DecrementTime(Time.deltaTime);
    }
}
