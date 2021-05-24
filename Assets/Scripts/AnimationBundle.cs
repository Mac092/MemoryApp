using System.Collections.Generic;
using UnityEngine.UI;

public class AnimationBundle 
{
    public enum AnimationBundleStatus { Initialized, Ongoing, Finished}

    private int _id;
    private int _currentAnimationIndex = 0;
    private List<Animation> _animations;

    private AnimationBundleStatus _animationBundleStatus = AnimationBundleStatus.Initialized;

    public AnimationBundle()
    {
        _animations = new List<Animation>();
        _currentAnimationIndex = 0;
    }

    public void StartRunning()
    {
        _animationBundleStatus = AnimationBundleStatus.Ongoing;
    }

    public void AddTextAnimation(in Text text, in Animation.AnimationType animationType, in float duration)
    {
        TextAnimation textAnimation = new TextAnimation(text, animationType, duration);
        _animations.Add(textAnimation);
    }

    public void RunCurrentAnimation(float deltaTime)
    {
        if (_animationBundleStatus == AnimationBundleStatus.Ongoing)
        {
            if (_animations[_currentAnimationIndex].RunAnimation(deltaTime))
                FinishAnimation();
        }
    }

    public void FinishAnimation()
    {
        _currentAnimationIndex += 1;
        if (_currentAnimationIndex >= _animations.Count)
            _animationBundleStatus = AnimationBundleStatus.Finished;
    }
}