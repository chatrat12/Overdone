using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public static float PlayerMoveSpeed => GetInstance()._playerMoveSpeed;
    public static float BonusSpeedMultiplier => GetInstance()._bonusSpeedMultiplier;
    public static float CustomerMoveSpeed => GetInstance()._customerMoveSpeed;
    public static SaladDish[] SaladDishes => GetInstance()._saladDishes;
    public static float OrderTime => GetInstance()._orderTime;
    public static float OrderTimeIngrediantBonus => GetInstance()._orderTimeIngrediantBonus;
    public static float PlayTime => GetInstance()._playTime;
    public static int OrderPoints => GetInstance()._orderPoints;
    public static int MissedOrderPenalty => GetInstance()._missedOrderPenalty;

    private static GameSettings _instance;

    [SerializeField] private float _playerMoveSpeed = 5f;
    [SerializeField] private float _bonusSpeedMultiplier = 2f;
    [SerializeField] private float _customerMoveSpeed = 2f;
    [SerializeField] private SaladDish[] _saladDishes;
    [SerializeField] private float _orderTime = 60f;
    // Extra time per extra ingrediant
    [SerializeField] private float _orderTimeIngrediantBonus = 10;
    [SerializeField] private float _playTime = 300f;
    [SerializeField] private int _orderPoints = 20;
    [SerializeField] private int _missedOrderPenalty = 5;

    private static GameSettings GetInstance()
    {
        if (_instance == null)
            _instance = Resources.Load<GameSettings>("GameSettings");
        return _instance;
    }
}

