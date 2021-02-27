using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OrderingUI : MonoBehaviour
{
    public CustomerOrder Order
    {
        get => _order;
        set
        {
            if(value == null)
                Clear();
            else
            {
                _lettuceIcon.gameObject.SetActive(value.Dish.HasLettuce);
                _tomatoIcon.gameObject.SetActive(value.Dish.HasTomato);
                _mushroomIcon.gameObject.SetActive(value.Dish.HasMushroom);
                _timeBar.gameObject.SetActive(true);
                UpdateBar();
            }
            _order = value;
        }
    }

    [SerializeField] private Image _lettuceIcon;
    [SerializeField] private Image _tomatoIcon;
    [SerializeField] private Image _mushroomIcon;
    [SerializeField] private UIProgressBar _timeBar;

    private CustomerOrder _order;

    private void Start()
    {
        Clear();
    }

    private void Update()
    {
        UpdateBar();
    }

    private void Clear()
    {
        _lettuceIcon.gameObject.SetActive(false);
        _tomatoIcon.gameObject.SetActive(false);
        _mushroomIcon.gameObject.SetActive(false);
        _timeBar.gameObject.SetActive(false);
    }

    private void UpdateBar()
    {
        if(_order != null)
            _timeBar.Value = _order.TimeRemainingNormalized;
    }
}
