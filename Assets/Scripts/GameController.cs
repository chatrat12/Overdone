using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public PlayerCharacter PlayerOne => _playerOne.Player;
    public PlayerCharacter PlayerTwo => _playerTwo.Player;

    [SerializeField] private PlayerAvatar _playerOne;
    [SerializeField] private PlayerAvatar _playerTwo;
    [SerializeField] private UIPlayerHUD _playerOneHUD;
    [SerializeField] private UIPlayerHUD _playerTwoHUD;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private GameObject _titleScree;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Initialize player HUD
        _playerOneHUD.Player = _playerOne.Player;
        _playerTwoHUD.Player = _playerTwo.Player;

        // Set camera controller to track players
        _cameraController.ObjectToTrack = GetPlayers().Select(p => p.Avatar.transform);
    }

    public void Reset()
    {
        _playerOne.Player.Reset();
        _playerTwo.Player.Reset();
        InteractableManager.Reset();

    }

    private IEnumerable<PlayerCharacter> GetPlayers()
    {
        yield return _playerOne.Player;
        yield return _playerTwo.Player;
    }
}
