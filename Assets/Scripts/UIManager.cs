using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button _exitButton;

    private void Start()
    {
        _exitButton.onClick.AddListener(GameManager.instance.ExitGame);
    }
}