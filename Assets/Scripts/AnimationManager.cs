using System;
using UnityEngine.UI;

public static class AnimationManager
{
    private static AnimationBundle[] _animationsBundle;
    private static int _animationsBundleSize;

    public static int GetAnimationsBundleSize() { return _animationsBundleSize; }

    public static void InitializeAnimationManager()
    {
        _animationsBundle = new AnimationBundle[0];
    }

    public static int CreateAnimationBundle()
    {
        int animationBundleID = 0;
        AnimationBundle animationBundle = new AnimationBundle();
        animationBundle.Subscribe(Level.instance);

        Array.Resize(ref _animationsBundle, _animationsBundle.Length + 1);
        _animationsBundleSize = _animationsBundle.Length;
        _animationsBundle[_animationsBundleSize - 1] = animationBundle;

        animationBundleID = _animationsBundleSize - 1;

        return animationBundleID;
    }

    public static void GenerateTextAnimationForAnimationBundle(int bundleID, in Text text, in TextAnimation.AnimationType animationType, in float animationDuration)
    {
        _animationsBundle[bundleID].AddTextAnimation(text, animationType, animationDuration);
    }

    public static void StartAnimationBundle(int bundleID)
    {
        _animationsBundle[bundleID].StartRunning();
    }

    public static void RunAnimationBundles(float deltaTime)
    {
        for (int i = 0; i < _animationsBundle.Length; i++)
        {
            _animationsBundle[i].RunCurrentAnimation(deltaTime);
        }
    }
}