using UnityEngine;
using TMPro;

public class BillboardText : MonoBehaviour, IReset
{
    private TextMeshPro _billboardText;
    private RectTransform _billboardTransform;
    private Transform _mainCameraTransform;

    [SerializeField] private float _yOffset;
    [SerializeField] private float _fontSize;

    private void Awake()
    {
        _mainCameraTransform = Camera.main.transform;
        AddBillboardText();
    }

    private void Update()
    {
        _billboardTransform.LookAt(_billboardTransform.position + _mainCameraTransform.forward.normalized);
    }

    private void AddBillboardText()
    {
        var gameObject = new GameObject("BillboardText");
        _billboardText = gameObject.AddComponent<TextMeshPro>();
        _billboardTransform = gameObject.GetComponent<RectTransform>();
        _billboardTransform.SetParent(transform);
        ConfigureBillboardText();
    }

    private void ConfigureBillboardText()
    {
        _billboardTransform.SetLocalPositionAndRotation(new Vector3(0f, _yOffset, 0f), Quaternion.identity);
        _billboardText.alignment = TextAlignmentOptions.Midline;
        _billboardText.fontSize = _fontSize;
    }

    public void SetText(string text) => _billboardText.text = text;

    public void Reset() => _billboardText.text = "";
}
