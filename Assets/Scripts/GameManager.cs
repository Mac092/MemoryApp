using UnityEngine;

public class GameManager : MonoBehaviour
{
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
}