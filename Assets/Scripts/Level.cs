using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level instance;

    [SerializeField]
    private GameObject _optionPrefab;

    private List<Option> _options;
    private int _activeOptions = 0;
    private int _solutionValue = 0;

    private const int _maxValue = 9;
    private const int _minValue = 0;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    public void GenerateRandomOptions(int optionsAmount)
    {
        //Generate random values for each options

        //Create instance prefabs for each options
    }

    public void OptionSelected(int selectedValue)
    {
        //Check if success/fail
        //Start the corresponding success/fails animations
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