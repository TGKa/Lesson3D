using TMPro;
using UnityEngine;

public class ViewCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueText;
    [SerializeField] private Counter _counter;

    private void OnEnable() =>
        _counter.ValueChanged += OnValueChanged;

    private void OnDisable() =>
        _counter.ValueChanged -= OnValueChanged;

    private void OnValueChanged(int value) =>
        _valueText.text = value.ToString();
}
