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
    private LevelStatus _levelStatus;
    private int _alreadyDisplayedOptions = 0;
    private LevelVisuals _levelVisuals = null;

    private const int _maxValue = 9;
    private const int _minValue = 0;

    public List<Option> GetOptions() { return _options; }
    public List<int> GetOptionsValues() { return _optionsValues; }  

    public void Awake()
    {
        if (instance == null) { instance = this; }

        _optionsValues = new List<int>();
        _options = new List<Option>();
        _solutions = new List<Option>();
        _levelVisuals = new LevelVisuals();
    }

    public void InitializeNewLevel(int optionsAmount, int solutionsAmount)
    {
        _levelStatus = LevelStatus.Initialization;
        _numOptions = optionsAmount;
        _numSolutions = solutionsAmount;

        GenerateRandomOptionsValues();
        GenerateSolutions();
    }

    public void StartLevel()
    {
        _levelVisuals.DisplaySolution(_options);
        MoveToNextLevelStatus();
    }

    public void RestartLevel(int optionsAmount, int solutionsAmount)
    {
        ResetLevel();
        InitializeNewLevel(optionsAmount, solutionsAmount);
        StartLevel();
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
                _levelVisuals.DisplayOptions(_options);
                break;
            case LevelStatus.ShowingOptions:
                _alreadyDisplayedOptions += 1;
                if (_alreadyDisplayedOptions >= _numOptions)
                {
                    _levelStatus = LevelStatus.OptionsSelection;
                    EnableInteractionOnOptions(true);
                }
                break;
            case LevelStatus.ShowingWrong:
                    _levelStatus = LevelStatus.OptionsSelection;
                    EnableInteractionOnOptions(true);
                break;
            case LevelStatus.ShowingRight:
                    _levelStatus = LevelStatus.OptionsSelection;
                    EnableInteractionOnOptions(true); 
                break;
            case LevelStatus.GameWon:
                GameManager.instance.FinishGame();
                break;
            case LevelStatus.GameLost:
                GameManager.instance.FinishGame();
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
            if (selectedValue == _solutions[i].GetAssignedValue())
            {
                correct = true;
                solution = _solutions[i];
                break;
            }
        }

        if (correct)
            RightSelection(ref solution);
        else
        {
            Option wrongOption = null;
            for (int i = _numSolutions; i < _numOptions; i++)
            {
                if (selectedValue == _options[i].GetAssignedValue())
                {
                    wrongOption = _options[i];
                    break;
                }
            }
            WrongSelection(ref wrongOption);
        }
    }

    public void RightSelection(ref Option solution)
    {
        solution.MarkSelectedOption(true);
        EvaluateGameWon(ref solution);
    }

    public void WrongSelection(ref Option wrongOption)
    {
        wrongOption.MarkSelectedOption(false);
        EvaluateGameLost(ref wrongOption);
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
        _solutions.AddRange(_options);
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

    private void EnableInteractionOnOptions(bool enable)
    {
        for (int i = 0; i < _options.Count; i++)
        {
            _options[i].EnableOptionInteraction(enable);
        }
    }

    private void EvaluateGameWon(ref Option selectedRightOption)
    {
        if (_numSolutions == 1)
        {
            _levelVisuals.HideAllOptions(_options);
            _levelStatus = LevelStatus.GameWon;
        }
        else
        {
            _levelVisuals.HideOption(selectedRightOption);
            _levelStatus = LevelStatus.ShowingRight;
        }
        EnableInteractionOnOptions(false);
        _options.Remove(selectedRightOption);
        _solutions.Remove(selectedRightOption);
        _optionsValues.Remove(selectedRightOption.GetAssignedValue());
        _numSolutions -= 1;
    }

    private void EvaluateGameLost(ref Option selectedWrongOption)
    {      
        if (_numOptions == _numSolutions + 1)
        {
            _levelVisuals.HideAllOptions(_options);
            _levelVisuals.MarkSolutions(_solutions);
            _levelStatus = LevelStatus.GameLost;
        }
        else
        {
            _levelVisuals.HideOption(selectedWrongOption);
            _levelStatus = LevelStatus.ShowingWrong;
        }
        EnableInteractionOnOptions(false);
        _options.Remove(selectedWrongOption);
        _optionsValues.Remove(selectedWrongOption.GetAssignedValue());
        _numOptions -= 1;
    }

    private void ResetLevel()
    {
        DestroyOptions();
        _alreadyDisplayedOptions = 0;
        _options.Clear();
        _optionsValues.Clear();
        _solutions.Clear();
    }

    private void DestroyOptions()
    {
        for (int i = 0; i < _optionsTRoot.childCount; i++)
        {
            Destroy(_optionsTRoot.GetChild(i).gameObject);
        }
    }
}