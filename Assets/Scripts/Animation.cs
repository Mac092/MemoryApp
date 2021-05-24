using UnityEngine;

public class Animation 
{
    public enum AnimationType { FadeIn, Static, FadeOut};

    protected AnimationType _animationType;

    protected float _duration;
    protected float _elapsedTime;

    protected const float _maxAlpha = 1;

    public Animation() { }

    public virtual bool RunAnimation(float deltaTime)
    {
        //Run corresponding animation based on AnimationType

        //Check if animation completed
        return false;
    }

    protected virtual void RunFadeInOut()
    {
        //Run FadeIn or Fade OutAnimation
    }

    protected float CalculateNextAlphaValue()
    {
        // Calculate next alpha value on Fades animations based on animation elapsed time
        return 0;
    }

    protected bool CheckAnimationCompleted()
    {
        //Check if elapsed time matches duration and so animation has finished
        return false;
    }
}