using System.Collections.Generic;
using UnityEngine.UI;

public class AnimationBundle 
{
    public enum AnimationBundleStatus { Initialized, Ongoing, Finished}

    private int _id;
    private int _animationsAmount;
    private int _currentAnimationIndex = 0;

    private AnimationBundleStatus _animationBundleStatus;

    private List<Animation> _animations;

    public void CreateAnimationBundle()
    {
        _animations = new List<Animation>();
    }

    public void AddTextAnimation(in Text text, in Animation.AnimationType animationType, in float duration)
    {
       //Create new textAnimation object and add it to the animations list
    }

    public void RunCurrentAnimation(float deltaTime)
    {
       //Run current animation ande evaluate if animation finished
    }

    public void FinishAnimation()
    {
        //Check if current animation was last animation or not. Pass to next animation or finish animation bundle and notify it
    }
}