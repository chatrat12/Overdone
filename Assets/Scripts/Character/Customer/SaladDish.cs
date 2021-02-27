using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class SaladDish : ScriptableObject
{
    public IEnumerable<VeggieType> Veggies => _veggies;
    public bool HasLettuce => HasVeggie(VeggieType.Lettuce);
    public bool HasTomato => HasVeggie(VeggieType.Tomato);
    public bool HasMushroom => HasVeggie(VeggieType.Mushroom);

    public float WaitTime
    {
        get
        {
            var result = GameSettings.OrderTime;
            result += _veggies.Length - 1 * GameSettings.OrderTimeIngrediantBonus;
            return result;
        }
    }

    [SerializeField] private VeggieType[] _veggies;

    public bool HasVeggie(VeggieType veggie)
       => _veggies.Contains(veggie);

    public bool MatchesPlate(PlateModel plate)
    {
        return HasLettuce  == plate.HasLettuce &&
               HasTomato   == plate.HasTomato  &&
               HasMushroom == plate.HasMushroom;
    }
}
