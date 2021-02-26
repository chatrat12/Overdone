using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public static float PlayerMoveSpeed => GetInstance()._playerMoveSpeed;
    public static float BonusSpeedMultiplier => GetInstance()._bonusSpeedMultiplier;
    public static float CustomerMoveSpeed => GetInstance()._customerMoveSpeed;

    private static GameSettings _instance;

    [SerializeField] private float _playerMoveSpeed = 5f;
    [SerializeField] private float _bonusSpeedMultiplier = 2f;
    [SerializeField] private float _customerMoveSpeed = 2f;

    private static GameSettings GetInstance()
    {
        if (_instance == null)
            _instance = Resources.Load<GameSettings>("GameSettings");
        return _instance;
    }
}
