using UnityEngine;

public class CustomerOrder
{
    public SaladDish Dish { get; private set; }
    public float TimeRemaining { get; private set; }
    public float TimeRemainingNormalized => TimeRemaining / _timeToComplete;
    public bool TimeExpired => TimeRemaining <= 0;
    public bool OrderComplete { get; private set; } = false;

    private float _timeToComplete;
    private bool _madAtPlayerOne = false;
    private bool _madAtPlayerTwo = false;

    public CustomerOrder(SaladDish dish)
    {
        Dish = dish;
        _timeToComplete = dish.WaitTime;
        TimeRemaining = _timeToComplete;
    }

    public void DecrementTime(float deltaTime)
    {
        // If received a bad order, double decrement time.
        if (_madAtPlayerOne || _madAtPlayerTwo)
            deltaTime *= 2;
        TimeRemaining -= deltaTime;
        TimeRemaining = Mathf.Max(0, TimeRemaining);
    }

    public bool SubmitOrder(ItemModel item, PlayerCharacter player)
    {
        var result = false;
        // If a plate was submitted, make sure the ingrediants are correct
        if (item is PlateModel plate)
            result = Dish.MatchesPlate(plate);

        if (result)
        {
            OrderComplete = true;
            player.Score.AddPoints(GameSettings.OrderPoints);
            if(TimeRemainingNormalized >= 0.7f)
            {
                Debug.Log("Give power up");
            }
        }
        else
        {
            if (player.ID == PlayerCharacter.PlayerID.One)
                _madAtPlayerOne = true;
            else
                _madAtPlayerTwo = true;
        }

        // Destroy object submitted
        GameObject.Destroy(item.gameObject);
        return result;
    }

    public void PenalizePlayers()
    {
        GameController.Instance.PlayerOne.Score.RemovePoints(GameSettings.MissedOrderPenalty * (_madAtPlayerOne ? 2 : 1));
        GameController.Instance.PlayerTwo.Score.RemovePoints(GameSettings.MissedOrderPenalty * (_madAtPlayerTwo ? 2 : 1));
    }
}
