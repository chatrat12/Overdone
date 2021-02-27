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
                _lettuceIcon.gameObject.SetActive(value.Veggies.Contains(VeggieType.Lettuce));
                _tomatoIcon.gameObject.SetActive(value.Veggies.Contains(VeggieType.Tomato));
                _mushroomIcon.gameObject.SetActive(value.Veggies.Contains(VeggieType.Mushroom));
            }
            _order = value;
        }
    }

    [SerializeField] private Image _lettuceIcon;
    [SerializeField] private Image _tomatoIcon;
    [SerializeField] private Image _mushroomIcon;

    private CustomerOrder _order;

    private void Start()
    {
        Clear();
    }

    private void Clear()
    {
        _lettuceIcon.gameObject.SetActive(false);
        _tomatoIcon.gameObject.SetActive(false);
        _mushroomIcon.gameObject.SetActive(false);
    }
}
