using System.Collections.Generic;
using UnityEngine.UI;

public class LevelVisuals 
{
    private const int _levelAnimationsDurations = 2;

    public LevelVisuals()
    {
    }

    public void DisplaySolution(in List<Option> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            Text solutionText = options[i].GetOptionText();
            int solutionAnimBundleID = AnimationManager.CreateAnimationBundle();

            AnimationManager.GenerateTextAnimationForAnimationBundle(solutionAnimBundleID, solutionText, Animation.AnimationType.FadeIn, _levelAnimationsDurations);
            AnimationManager.GenerateTextAnimationForAnimationBundle(solutionAnimBundleID, solutionText, Animation.AnimationType.Static, _levelAnimationsDurations);
            AnimationManager.GenerateTextAnimationForAnimationBundle(solutionAnimBundleID, solutionText, Animation.AnimationType.FadeOut, _levelAnimationsDurations);
            AnimationManager.StartAnimationBundle(solutionAnimBundleID);
        }
    }

    public void DisplayOptions(in List<Option> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            Text optionText = options[i].GetOptionText();
            int optionAnimBundleID = AnimationManager.CreateAnimationBundle();

            AnimationManager.GenerateTextAnimationForAnimationBundle(optionAnimBundleID, optionText, Animation.AnimationType.FadeIn, _levelAnimationsDurations);
            AnimationManager.GenerateTextAnimationForAnimationBundle(optionAnimBundleID, optionText, Animation.AnimationType.Static, _levelAnimationsDurations);
            AnimationManager.StartAnimationBundle(optionAnimBundleID);
        }
    }

    public void HideOption(in Option option)
    {
        Text optionText = option.GetOptionText();
        int optionAnimBundleID = AnimationManager.CreateAnimationBundle();

        AnimationManager.GenerateTextAnimationForAnimationBundle(optionAnimBundleID, optionText, Animation.AnimationType.FadeOut, _levelAnimationsDurations);
        AnimationManager.StartAnimationBundle(optionAnimBundleID);
    }

    public void HideAllOptions(in List<Option> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            HideOption(options[i]);
        }
    }

    public void MarkSolutions(in List<Option> solutions)
    {
        for (int i = 0; i < solutions.Count; i++)
        {
            solutions[i].MarkSelectedOption(true);
        }
    }
}