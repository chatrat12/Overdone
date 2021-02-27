public class CustomerAvatar : CharacterAvatar
{
    public CustomerCharacter Customer { get; private set; }

    private void Awake()
    {
        Customer = new CustomerCharacter(this);
    }

    private void Start()
    {
        // Hide
        gameObject.SetActive(false);
    }

    private void Update() => Customer.Update();
}
