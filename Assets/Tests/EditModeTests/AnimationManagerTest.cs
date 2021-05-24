using NUnit.Framework;

public class AnimationManagerTest
{
    [Test]
    public void TestCreateAnimationManager()
    {
        int oldAnimationBundlesArraySize = 0;
        int newAnimationBundleArraySize = 0;

        AnimationManager.InitializeAnimationManager();
        oldAnimationBundlesArraySize = AnimationManager.GetAnimationsBundleSize();
        AnimationManager.CreateAnimationBundle();
        newAnimationBundleArraySize = AnimationManager.GetAnimationsBundleSize();

        Assert.AreEqual(oldAnimationBundlesArraySize + 1, newAnimationBundleArraySize);
    }
}
