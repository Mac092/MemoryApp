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

    private void Awake()
    {
        _selectBtn.onClick.AddListener(OptionSelected);
    }

    public void InitializeOption(int value)
    {
        _assignedValue = value;
        _optionText.text = _assignedValue.ToString();
    }

    public void MarkSelectedOption(bool correct)
    {
        //Change color for selected option based on selection success/fail
    }

    private void OptionSelected()
    {
        //Select option and check success
    }
}