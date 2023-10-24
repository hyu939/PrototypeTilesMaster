using UnityEngine;

public class GamePause : MonoBehaviour
{
    public static GamePause Instance;
    public bool isPause;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void PauseGame()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }
}
