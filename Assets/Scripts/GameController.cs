using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public PlayerCharacter PlayerOne => _playerOne.Player;
    public PlayerCharacter PlayerTwo => _playerTwo.Player;
    public StateType State { get; private set; } = StateType.TitleScreen;


    [SerializeField] private PlayerAvatar _playerOne;
    [SerializeField] private PlayerAvatar _playerTwo;
    [SerializeField] private UIPlayerHUD _playerOneHUD;
    [SerializeField] private UIPlayerHUD _playerTwoHUD;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private UITitleScreen _titleScreen;
    [SerializeField] private OrderStationController _orderController;
    [SerializeField] private GameObject _playerHUD;
    [SerializeField] private UIEndScreen _endScreen;

    private void Awake()
    {
        Instance = this;
        _titleScreen.OnStartClicked.AddListener(() => StartGame());
        _endScreen.OnRestartClicked.AddListener(() => StartGame());
    }

    private void Start()
    {
        // Initialize player HUD
        _playerOneHUD.Player = _playerOne.Player;
        _playerTwoHUD.Player = _playerTwo.Player;

        // Set camera controller to track players
        _cameraController.ObjectToTrack = GetPlayers().Select(p => p.Avatar.transform);
        _playerHUD.SetActive(false);
        _endScreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(State == StateType.Playing)
        {
            // Check if all player timers expired
            if(!GetPlayers().Any(p => !p.Time.Expired))
                ShowEndScreen();
        }
    }

    public void StartGame()
    {
        State = StateType.Playing;
        _titleScreen.gameObject.SetActive(false);
        _endScreen.gameObject.SetActive(false);
        _cameraController.gameObject.SetActive(true);
        _playerHUD.gameObject.SetActive(true);
        Reset();
        _orderController.enabled = true;
    }

    private void ShowEndScreen()
    {
        State = StateType.EndScreen;
        _orderController.enabled = false;
        _playerHUD.SetActive(false);
        _endScreen.gameObject.SetActive(true);
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

    public enum StateType
    {
        TitleScreen,
        Playing,
        EndScreen
    }
}
