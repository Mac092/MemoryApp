using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField]
    private Text _optionText;
    [SerializeField]
    private Button _selectBtn;
    [SerializeField]
    private Color _successColor;
    [SerializeField]
    private Color _failColor;

    private int _assignedValue;

    public Text GetOptionText() { return _optionText; }
    public int GetAssignedVaue() { return _assignedValue; }

    private void Awake()
    {
        _selectBtn.onClick.AddListener(OptionSelected);
    }

    public void InitializeOption(int value)
    {
        _assignedValue = value;
        _optionText.text = _assignedValue.ToString();
    }

    public void EnableOptionInteraction(bool enable)
    {
        _selectBtn.enabled = enable;
    }

    public void MarkSelectedOption(bool correct)
    {
        if (correct)
            _optionText.color = _successColor;
        else
            _optionText.color = _failColor;
    }

    private void OptionSelected()
    {
        Level.instance.EvaluateSelectedOption(_assignedValue);
    }
}