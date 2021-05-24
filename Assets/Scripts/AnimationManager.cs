using System;
using UnityEngine;

public static class AnimationManager
{
    private static AnimationBundle[] _animationsBundle;
    private static int _animationsBundleSize;

    public static int GetAnimationsBundleSize() { return _animationsBundleSize; }

    public static void InitializeAnimationManager()
    {
        _animationsBundle = new AnimationBundle[0];
    }

    public static void CreateAnimationBundle()
    {
        AnimationBundle animationBundle = new AnimationBundle();

        Array.Resize(ref _animationsBundle, _animationsBundle.Length + 1);
        _animationsBundleSize = _animationsBundle.Length;
        _animationsBundle[_animationsBundleSize - 1] = animationBundle;
    }
}