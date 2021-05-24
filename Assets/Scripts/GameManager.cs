using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Level.instance.GenerateRandomOptions(3);
        Level.instance.InstantiateGeneratedOptions();
    }
}