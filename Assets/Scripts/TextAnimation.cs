using UnityEngine.UI;

public class TextAnimation : Animation
{
    private Text _animationText;

    public TextAnimation(Text animationText, AnimationType animationType, float duration)
    {
        //Assign values to class variables
    }

    protected override void RunFadeInOut()
    {
        //Change text alpha color based on animationtype and animation current progress
    }
}