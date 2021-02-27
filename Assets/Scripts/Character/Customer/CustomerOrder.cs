using UnityEngine;

public class CustomerOrder
{
    public SaladDish Dish { get; private set; }
    public float TimeRemaining { get; private set; }
    public float TimeRemainingNormalized => TimeRemaining / _timeToComplete;
    public bool TimeExpired => TimeRemaining <= 0;
    public int IncorrectOrdersReceived { get; private set; }
    public bool OrderComplete { get; private set; } = false;

    private float _timeToComplete;

    public CustomerOrder(SaladDish dish, float timeToComplete)
    {
        Dish = dish;
        _timeToComplete = timeToComplete;
        TimeRemaining = _timeToComplete;
    }

    public void DecrementTime(float deltaTime)
    {
        // If received a bad order, double decrement time.
        if (IncorrectOrdersReceived > 0)
            deltaTime *= 2;
        TimeRemaining -= deltaTime;
        TimeRemaining = Mathf.Max(0, TimeRemaining);
    }

    public bool SubmitOrder(ItemModel item)
    {
        var result = false;
        // If a plate was submitted, make sure the ingrediants are correct
        if (item is PlateModel plate)
            result = Dish.MatchesPlate(plate);

        if (result)
            OrderComplete = true;
        else
            IncorrectOrdersReceived++;

        // Destroy object submitted
        GameObject.Destroy(item.gameObject);
        return result;
    }
}
