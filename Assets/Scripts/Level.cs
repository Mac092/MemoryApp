using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level instance;

    [SerializeField]
    private GameObject _optionPrefab;
    [SerializeField]
    private RectTransform _optionsTRoot;

    private List<Option> _options;
    private List<int> _optionsValues;
    private int _numOptions = 0;
    private int _activeOptions = 0;
    private int _solutionValue = 0;

    private const int _maxValue = 9;
    private const int _minValue = 0;

    public List<Option> GetOptions() { return _options; }
    public List<int> GetOptionsValues() { return _optionsValues; }  

    public void Awake()
    {
        if (instance == null) { instance = this; }

        _optionsValues = new List<int>();
        _options = new List<Option>();
    }

    public void GenerateRandomOptions(int optionsAmount)
    {
        _numOptions = optionsAmount;

        for (int i = 0; i < _numOptions; i++)
        {
            bool randomUniqueValue = false;
            while (!randomUniqueValue)
            {
                randomUniqueValue = GenerateNewRandomUniqueValue();
            }
        }
    }

    public void InstantiateGeneratedOptions()
    {
        for (int i = 0; i < _numOptions; i++)
        {
            GameObject optionGO = Instantiate(_optionPrefab, _optionsTRoot);
            _options.Add(optionGO.GetComponent<Option>());
            _options[i].InitializeOption(_optionsValues[i]);
        }
    }

    public void OptionSelected(int selectedValue)
    {
        //Check if success/fail
        //Start the corresponding success/fails animations
    }

    private bool GenerateNewRandomUniqueValue()
    {
        bool randomUniqueNumGenerated = false;
        int newOptionValue;

        newOptionValue = Random.Range(_minValue, _maxValue);
        if (!_optionsValues.Contains(newOptionValue))
        {
            _optionsValues.Add(newOptionValue);
            randomUniqueNumGenerated = true;
        }

        return randomUniqueNumGenerated;
    }

    private void SelectRandomOptionAsSolution()
    {
        //Choose randomely one option from the options list as solution
    }

    private void DisplaySolution()
    {
        //Show solution on UI
    }

    private void DisplayOptions()
    {
        //Show all options on UI
    }

    private void EnableInteractionOnOptions(bool enable)
    {
        //Enable or disable button interaction for the options based on request
    }

    private void ResetLevel()
    {
        //Clean and reset variables and values for a new level
    }

    private void DestroyOptions()
    {
        //Destroy options prefabs isntances
    }
}