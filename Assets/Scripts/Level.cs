using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour, Observer
{
    public static Level instance;

    public enum LevelStatus { Initialization, ShowingSolution, ShowingOptions, OptionsSelection, ShowingWrong, ShowingRight, GameWon, GameLost}

    [SerializeField]
    private GameObject _optionPrefab;
    [SerializeField]
    private RectTransform _optionsTRoot;

    private List<Option> _options;
    private List<Option> _solutions;
    private List<int> _optionsValues;
    private int _numOptions = 0;
    private int _numSolutions = 0;
    private int _activeOptions = 0;
    private LevelStatus _levelStatus = LevelStatus.Initialization;

    private int _alreadyDisplayedOptions = 0;

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
        _solutions = new List<Option>();
    }

    public void InitializeNewLevel(int optionsAmount, int solutionsAmount)
    {
        _numOptions = optionsAmount;
        _numSolutions = solutionsAmount;

        GenerateRandomOptionsValues();
        GenerateSolutions();
    }

    public void StartLevel()
    {
        DisplaySolution();
        MoveToNextLevelStatus();
    }

    public void MoveToNextLevelStatus()
    {
        switch (_levelStatus)
        {
            case LevelStatus.Initialization:
                _levelStatus = LevelStatus.ShowingSolution;
                break;
            case LevelStatus.ShowingSolution:
                _levelStatus = LevelStatus.ShowingOptions;
                GenerateOtherOptionsAsideFromSolutions();
                DisplayOptions();
                break;
            case LevelStatus.ShowingOptions:
                _alreadyDisplayedOptions += 1;
                if (_alreadyDisplayedOptions >= _numOptions)
                {
                    _levelStatus = LevelStatus.OptionsSelection;
                    EnableInteractionOnOptions(true);
                }
                break;
            case LevelStatus.OptionsSelection:
                break;
            case LevelStatus.ShowingWrong:
                break;
            case LevelStatus.ShowingRight:
                if (_numSolutions == 0)
                {
                    //gamewon
                }
                break;
            case LevelStatus.GameWon:
                break;
            case LevelStatus.GameLost:
                break;
            default:
                break;
        }
    }

    public void EvaluateSelectedOption(int selectedValue)
    {
        bool correct = false;
        Option solution = null;

        for (int i = 0; i < _numSolutions; i++)
        {
            if (selectedValue == _solutions[i].GetAssignedVaue())
            {
                correct = true;
                solution = _solutions[i];
                break;
            }
        }

        if (correct)
            RightSelection(ref solution);
        else
            WrongSelection(selectedValue);
    }

    public void RightSelection(ref Option solution)
    {
        solution.MarkSelectedOption(true);
        HideAllOptions();
        _options.Remove(solution);
        _optionsValues.Remove(solution.GetAssignedVaue());
        _numSolutions -= 1;
        //TODO Increment success counter
        _levelStatus = LevelStatus.ShowingRight;
    }

    public void WrongSelection(int selectedValue)
    {
        Option wrongOption = null;

        for (int i = _numSolutions; i < _numOptions; i++)
        {
            if (selectedValue == _options[i].GetAssignedVaue())
            {
                wrongOption = _options[i];
                wrongOption.MarkSelectedOption(false);
                HideOption(wrongOption);
                _options.Remove(wrongOption);
                _optionsValues.Remove(wrongOption.GetAssignedVaue());
                break;
            }
        }
        _numOptions -= 1;
        _levelStatus = LevelStatus.ShowingWrong;
        //TODO Increment fail counter
    }

    public void NotifyUpdate()
    {
        MoveToNextLevelStatus();
    }

    private void GenerateRandomOptionsValues()
    {
        for (int i = 0; i < _numOptions; i++)
        {
            bool randomUniqueValue = false;
            while (!randomUniqueValue)
            {
                randomUniqueValue = GenerateNewRandomUniqueValue();
            }
        }
    }

    private void GenerateOtherOptionsAsideFromSolutions()
    {
        for (int i = _numSolutions; i < _numOptions; i++)
        {
            InstantiateOption(i);
        }
    }

    private void GenerateSolutions()
    {
        for (int i = 0; i < _numSolutions; i++)
        {
            InstantiateOption(i);
        }
        _solutions = _options;
    }

    private void InstantiateOption(int optionIndex)
    {
        GameObject optionGO = Instantiate(_optionPrefab, _optionsTRoot);
        _options.Add(optionGO.GetComponent<Option>());
        _options[optionIndex].InitializeOption(_optionsValues[optionIndex]);

        //Randomize display orther by randomely setting new "option" as the next sibling or as first sibling
        if (Random.Range(0, 2) == 1)
            _options[optionIndex].transform.SetSiblingIndex(0);       
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

    private void DisplaySolution()
    {
        for (int i = 0; i < _numSolutions; i++)
        {
            Text solutionText = _options[i].GetOptionText();
            int solutionAnimBundleID = AnimationManager.CreateAnimationBundle();

            AnimationManager.GenerateTextAnimationForAnimationBundle(solutionAnimBundleID, solutionText, Animation.AnimationType.FadeIn, _levelAnimationsDurations);
            AnimationManager.GenerateTextAnimationForAnimationBundle(solutionAnimBundleID, solutionText, Animation.AnimationType.Static, _levelAnimationsDurations);
            AnimationManager.GenerateTextAnimationForAnimationBundle(solutionAnimBundleID, solutionText, Animation.AnimationType.FadeOut, _levelAnimationsDurations);
            AnimationManager.StartAnimationBundle(solutionAnimBundleID);
        }
    }

    private void DisplayOptions()
    {
        for (int i = 0; i < _numOptions; i++)
        {
            Text optionText = _options[i].GetOptionText();
            int optionAnimBundleID = AnimationManager.CreateAnimationBundle();

            AnimationManager.GenerateTextAnimationForAnimationBundle(optionAnimBundleID, optionText, Animation.AnimationType.FadeIn, _levelAnimationsDurations);
            AnimationManager.GenerateTextAnimationForAnimationBundle(optionAnimBundleID, optionText, Animation.AnimationType.Static, _levelAnimationsDurations);
            AnimationManager.StartAnimationBundle(optionAnimBundleID);
        }
    }

    private void HideOption(Option option)
    {
        Text optionText = option.GetOptionText();
        int optionAnimBundleID = AnimationManager.CreateAnimationBundle();

        AnimationManager.GenerateTextAnimationForAnimationBundle(optionAnimBundleID, optionText, Animation.AnimationType.FadeOut, _levelAnimationsDurations);
        AnimationManager.StartAnimationBundle(optionAnimBundleID);
    }

    private void HideAllOptions()
    {
        for (int i = 0; i < _numOptions; i++)
        {
            HideOption(_options[i]);
        }
    }

    private void EnableInteractionOnOptions(bool enable)
    {
        for (int i = 0; i < _options.Count; i++)
        {
            _options[i].EnableOptionInteraction(enable);
        }
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