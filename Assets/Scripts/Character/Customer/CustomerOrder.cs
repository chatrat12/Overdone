using System.Collections.Generic;
using UnityEngine;

public class CustomerOrder : ScriptableObject
{
    public IEnumerable<VeggieType> Veggies => _veggies;

    [SerializeField] private VeggieType[] _veggies;
}
