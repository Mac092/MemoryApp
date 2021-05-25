using NUnit.Framework;

public class LevelTest
{
    [Test]
    public void TestLevelOptionsNumCreated()
    {
        int levelOptionsNum = 9;
        Level level = new Level();

        level.Awake();
        //level.GenerateRandomOptions(levelOptionsNum);

        Assert.AreEqual(levelOptionsNum, Level.instance.GetOptionsValues().Count);
    }

    [Test]
    public void TestLevelOptionsUnique()
    {
        int levelOptionsNum = 9;
        int levelSolutionsNum = 1;
        Level level = new Level();

        level.Awake();
        level.InitializeNewLevel(levelOptionsNum, levelSolutionsNum);

        Assert.AreNotEqual(Level.instance.GetOptionsValues()[0], Level.instance.GetOptionsValues()[1]);
        Assert.AreNotEqual(Level.instance.GetOptionsValues()[0], Level.instance.GetOptionsValues()[2]);
        Assert.AreNotEqual(Level.instance.GetOptionsValues()[0], Level.instance.GetOptionsValues()[3]);
        Assert.AreNotEqual(Level.instance.GetOptionsValues()[0], Level.instance.GetOptionsValues()[4]);
        Assert.AreNotEqual(Level.instance.GetOptionsValues()[0], Level.instance.GetOptionsValues()[5]);
        Assert.AreNotEqual(Level.instance.GetOptionsValues()[0], Level.instance.GetOptionsValues()[6]);
        Assert.AreNotEqual(Level.instance.GetOptionsValues()[0], Level.instance.GetOptionsValues()[7]);
        Assert.AreNotEqual(Level.instance.GetOptionsValues()[0], Level.instance.GetOptionsValues()[8]);
    }
}
