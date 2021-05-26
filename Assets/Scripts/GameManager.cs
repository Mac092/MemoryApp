using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private bool _infiniteGame;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    private void Start()
    {
        AnimationManager.InitializeAnimationManager();

        Level.instance.InitializeNewLevel(3,1);
        Level.instance.StartLevel();
    }

    private void Update()
    {
        AnimationManager.RunAnimationBundles(Time.deltaTime);
    }

    public void FinishGame()
    {
        if (_infiniteGame)
        {
            AnimationManager.RestartAnimationManager();
            Level.instance.RestartLevel(3, 1);
        }
        else
            ExitGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}