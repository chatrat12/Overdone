using System.Linq;
using UnityAsync;
using UnityEngine;

public class OrderStationController : MonoBehaviour
{
    [SerializeField] private float _minNextCustomerTime = 3;
    [SerializeField] private float _maxNextCustomerTime = 7;

    private InteracableOrderStation[] _orderStations;
    private float _nextCustomerTime;

    private void Awake()
    {
        _orderStations = GetComponentsInChildren<InteracableOrderStation>();
    }

    private void OnEnable()
    {
        SendCustomer(true);
        UpdateNextCustomerTime();
    }

    private void OnDisable()
    {
        foreach (var station in _orderStations)
            station.Customer.Locomotion.Stop();
    }

    private void Update()
    {
        if (Time.time >= _nextCustomerTime)
        {
            SendCustomer();
            UpdateNextCustomerTime();
        }
    }

    private void SendCustomer(bool teleport = false)
    {
        var freeStations = _orderStations.Where(s => s.State == InteracableOrderStation.OrderStationState.Empty);
        if(freeStations.Any())
        {
            var index = Random.Range(0, freeStations.Count());
            var station = freeStations.ElementAt(index);
            station.AddCustomer(teleport);
        }
    }

    private void UpdateNextCustomerTime()
    {
        _nextCustomerTime = Time.time + Random.Range(_minNextCustomerTime, _maxNextCustomerTime);
    }
}
