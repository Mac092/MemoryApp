using UnityEngine.UI;

public class TextAnimation : Animation
{
    private Text _animationText;

    public TextAnimation(Text animationText, AnimationType animationType, float duration)
    {
        _animationText = animationText;
        _animationType = animationType;
        _duration = duration;
    }

    protected override void RunFadeInOut()
    {
        float nextTextAlpha = CalculateNextAlphaValue();
        _animationText.color = new UnityEngine.Color(_animationText.color.r, _animationText.color.g, _animationText.color.b, nextTextAlpha);
    }
}