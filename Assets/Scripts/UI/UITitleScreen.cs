using UnityEngine;
using UnityEngine.UI;

class UITitleScreen : MonoBehaviour
{
    public Button.ButtonClickedEvent OnStartClicked => _startButton.onClick;
    [SerializeField] private Button _startButton;
}
