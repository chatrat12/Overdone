using System;
using UnityEngine;

public class PlayerTime
{
    public float Remaining { get; private set; }
    public bool Expired => Remaining <= 0;

    private bool _running = false;

    public void Reset()
    {
        Remaining = GameSettings.PlayTime;
        _running = false;
    }
    public void Start() => _running = true;
    public void Stop() => _running = false;

    public void Update()
    {
        if (_running && !Expired)
        {
            Remaining -= Time.deltaTime;
            Remaining = Mathf.Max(0, Remaining);
        }
    }

    public void AddTime(float amount) => Remaining += amount;
    public override string ToString() => TimeSpan.FromSeconds(Remaining).ToString(@"m\:ss");
}
