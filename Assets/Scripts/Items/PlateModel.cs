using System.Collections.Generic;
using UnityEngine;

public class PlateModel : ItemModel
{
    public bool HasLettuce => HasVeggie(VeggieType.Lettuce);
    public bool HasTomato => HasVeggie(VeggieType.Tomato);
    public bool HasMushroom => HasVeggie(VeggieType.Mushroom);

    [SerializeField] private GameObject _lettuceModel;
    [SerializeField] private GameObject _tomatoModel;
    [SerializeField] private GameObject _mushroomModel;

    private List<VeggieType> _veggies = new List<VeggieType>();

    private void Start()
    {
        UpdateModelVisibility();
    }

    public void AddVeggie(VeggieType veggie)
    {
        if (CanAddVeggie(veggie))
        {
            _veggies.Add(veggie);
            UpdateModelVisibility();
        }
    }
    public bool CanAddVeggie(VeggieType veggie)
        => !HasVeggie(veggie);

    public bool HasVeggie(VeggieType veggie)
        => _veggies.Contains(veggie);

    private void UpdateModelVisibility()
    {
        _lettuceModel.SetActive(HasLettuce);
        _tomatoModel.SetActive(HasTomato);
        _mushroomModel.SetActive(HasMushroom);
    }
}