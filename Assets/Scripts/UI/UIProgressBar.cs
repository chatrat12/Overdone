using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIProgressBar : MonoBehaviour
{
    [SerializeField] protected Image _barImage;
    [Range(0f, 1f)]
    [SerializeField] private float _value = 1;

    public float Value
    {
        get { return _value; }
        set
        {
            if (float.IsNaN(value))
                _value = 0;
            else
                _value = Mathf.Clamp01(value);
            UpdateBarSize();
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (_barImage == null) return;
        if (!UnityEditor.EditorApplication.isPlaying && _barImage != null)
            UpdateBarSize();
    }
#endif

    private void UpdateBarSize()
    {
        var anchorMax = _barImage.rectTransform.anchorMax;
        anchorMax.x = _value;
        _barImage.rectTransform.anchorMax = anchorMax;
    }
}