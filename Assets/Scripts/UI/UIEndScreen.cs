using UnityEngine;
using UnityEngine.UI;

public class UIEndScreen : MonoBehaviour
{
    public Button.ButtonClickedEvent OnRestartClicked => _restartButton.onClick;

    [SerializeField] private UIPlayerHUD _playerOne;
    [SerializeField] private UIPlayerHUD _playerTwo;
    [SerializeField] private Button _restartButton;
 
    private void Start()
    {
        _playerOne.Player = GameController.Instance.PlayerOne;
        _playerTwo.Player = GameController.Instance.PlayerTwo;
    }
}
