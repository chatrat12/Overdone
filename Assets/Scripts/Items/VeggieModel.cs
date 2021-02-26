using UnityEngine;

public class VeggieModel : ItemModel
{
    public VeggieType Type => _type;
    public bool Sliced { get; private set; } = false;

    [SerializeField] private VeggieType _type;
    [SerializeField] private GameObject _unslicedModel;
    [SerializeField] private GameObject _slicedModel;

    private void Start()
    {
        UpdateModelVisibility(Sliced);
    }

    public void Slice()
    {
        Sliced = true;
        UpdateModelVisibility(Sliced);
    }

    private void UpdateModelVisibility(bool sliced)
    {
        _unslicedModel.SetActive(!sliced);
        _slicedModel.SetActive(sliced);
    }

}
