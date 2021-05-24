using NUnit.Framework;

public class AnimationManagerTest
{
    [Test]
    public void AnimationManagerTestSimplePasses()
    {
        int oldAnimationBundlesArraySize = 0;
        int newAnimnationBundleArraySize = 0;

        AnimationManager.InitializeAnimationManager();
        oldAnimationBundlesArraySize = AnimationManager.GetAnimationsBundleSize();
        AnimationManager.CreateAnimationBundle();
        newAnimnationBundleArraySize = AnimationManager.GetAnimationsBundleSize();

        Assert.AreEqual(oldAnimationBundlesArraySize + 1, newAnimnationBundleArraySize);
    }
}
