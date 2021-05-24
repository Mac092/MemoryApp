using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int _solutionIndex = 0;

    private const int _maxValue = 9;
    private const int _minValue = 0;

    private const int _levelAnimationsDurations = 2;

    public List<Option> GetOptions() { return _options; }
    public List<int> GetOptionsValues() { return _optionsValues; }  

    public void Awake()
    {
        if (instance == null) { instance = this; }

        _optionsValues = new List<int>();
        _options = new List<Option>();
    }

    public void InitializeNewLevel(int optionsAmount)
    {
        GenerateRandomOptions(optionsAmount);
        SelectRandomOptionAsSolution();
        InstantiateGeneratedOptions();
    }

    public void StartLevel()
    {
        DisplaySolution();
    }

    public void OptionSelected(int selectedValue)
    {
        //Check if success/fail
        //Start the corresponding success/fails animations
    }

    private void GenerateRandomOptions(int optionsAmount)
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

    private void SelectRandomOptionAsSolution()
    {
        _solutionIndex = Random.Range(0, _optionsValues.Count);
    }

    private void InstantiateGeneratedOptions()
    {
        for (int i = 0; i < _numOptions; i++)
        {
            GameObject optionGO = Instantiate(_optionPrefab, _optionsTRoot);
            _options.Add(optionGO.GetComponent<Option>());
            _options[i].InitializeOption(_optionsValues[i]);
        }
    }

    private bool GenerateNewRandomUniqueValue()
    {
        bool randomUniqueNumGenerated = false;
        int newOptionValue;

        newOptionValue = Random.Range(_minValue, _maxValue + 1);
        if (!_optionsValues.Contains(newOptionValue))
        {
            _optionsValues.Add(newOptionValue);
            randomUniqueNumGenerated = true;
        }

        return randomUniqueNumGenerated;
    }

    public void DisplaySolution()
    {
        Text solutionText = _options[_solutionIndex].GetOptionText();
        int solutionAnimBundleID = AnimationManager.CreateAnimationBundle();

        AnimationManager.GenerateTextAnimationForAnimationBundle(solutionAnimBundleID, solutionText, Animation.AnimationType.FadeIn, _levelAnimationsDurations);
        AnimationManager.GenerateTextAnimationForAnimationBundle(solutionAnimBundleID, solutionText, Animation.AnimationType.Static, _levelAnimationsDurations);
        AnimationManager.GenerateTextAnimationForAnimationBundle(solutionAnimBundleID, solutionText, Animation.AnimationType.FadeOut, _levelAnimationsDurations);
        AnimationManager.StartAnimationBundle(solutionAnimBundleID);
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