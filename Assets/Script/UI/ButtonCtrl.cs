using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCtrl : MonoBehaviour
{
    // Trước năng các nút bấm trong Màn Chơi
    public void AgainButton()
    {
        ButtonSound();
        GamePause.Instance.Resume();
        SceneManager.LoadScene(1);
    }
    public void HomeButton()
    {
        ButtonSound();
        GamePause.Instance.Resume();
        SceneManager.LoadScene(0);
    }
    public void NextLVButton()
    {
        ButtonSound();
        GamePause.Instance.Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuButton()
    {
        ButtonSound();
        GamePause.Instance.PauseGame();
    }

    public void CloseMenuButton()
    {
        ButtonSound();
        GamePause.Instance.Resume();
    }

    public void ButtonSound()
    {
        SoundCtrl.Instance.AudioButton();

    }
}
