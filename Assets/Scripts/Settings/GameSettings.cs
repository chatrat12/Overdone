using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public static float PlayerMoveSpeed => GetInstance()._playerMoveSpeed;
    public static float BonusSpeedMultiplier => GetInstance()._bonusSpeedMultiplier;
    public static float CustomerMoveSpeed => GetInstance()._customerMoveSpeed;
    public static SaladDish[] SaladDishes => GetInstance()._saladDishes;
    public static float OrderTime => GetInstance()._orderTime;

    private static GameSettings _instance;

    [SerializeField] private float _playerMoveSpeed = 5f;
    [SerializeField] private float _bonusSpeedMultiplier = 2f;
    [SerializeField] private float _customerMoveSpeed = 2f;
    [SerializeField] private SaladDish[] _saladDishes;
    [SerializeField] private float _orderTime = 60f;

    private static GameSettings GetInstance()
    {
        if (_instance == null)
            _instance = Resources.Load<GameSettings>("GameSettings");
        return _instance;
    }
}

