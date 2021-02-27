public class PlayerScore
{
    public int Value { get; private set; } = 0;

    public void Reset() => Value = 0;

    public void AddPoints(int amount) => Value += amount;
    public void RemovePoints(int amount) => Value -= amount;
}
