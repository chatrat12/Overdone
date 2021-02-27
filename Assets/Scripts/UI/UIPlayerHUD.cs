using TMPro;
using UnityEngine;

public class UIPlayerHUD : MonoBehaviour
{
    public PlayerCharacter Player { get; set; }

    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _timeText;

    private void Update()
    {
        if (Player != null)
        {
            _scoreText.text = Player.Score.Value.ToString();
            if (_timeText != null)
                _timeText.text = Player.Time.ToString();
        }
    }

}
