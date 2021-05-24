using UnityEngine;

public class Animation 
{
    public enum AnimationType { FadeIn, Static, FadeOut};

    protected AnimationType _animationType;

    protected float _duration = 0f;
    protected float _elapsedTime = 0f;

    protected const float _maxAlpha = 1;

    public Animation() { }

    public virtual bool RunAnimation(float deltaTime)
    {
        _elapsedTime += deltaTime;

        switch (_animationType)
        {
            case AnimationType.FadeIn:
            case AnimationType.FadeOut:
                RunFadeInOut();
                break;
            case AnimationType.Static:
                RunStatic();
                break;
            default:
                break;
        }

        return CheckAnimationCompleted();
    }

    protected virtual void RunFadeInOut()
    {
    }

    protected virtual void RunStatic()
    {

    }

    protected float CalculateNextAlphaValue()
    {
        float nextAlpha = 0;

        if (_animationType == AnimationType.FadeIn)
        {
            nextAlpha = (_elapsedTime * _maxAlpha) / _duration;
        }
        else if (_animationType == AnimationType.FadeOut)
            nextAlpha = _maxAlpha - ( (_elapsedTime * _maxAlpha) / _duration);

        return nextAlpha;
    }

    protected bool CheckAnimationCompleted()
    {
        bool completed = false;

        if (_elapsedTime >= _duration)
            completed = true;

        return completed;
    }
}