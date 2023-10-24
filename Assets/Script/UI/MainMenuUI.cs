using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    private int playbtCount=0;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI displayLV;

    private int currentLV  = 0;
    private float volumeValue = 1;

    // Save Load của MainMenu và chức năng của nút tấm trong MainMenu

    private void Start()
    {
        LoadData();
    }

    public void PlayButton()
    {
        ButtonSound();
        playbtCount++;
        if(playbtCount == 3)
        {
            SceneManager.LoadScene(1);
            SaveData();
        }
    }


    public void SliderToMixer()
    {
        ButtonSound();
        volumeValue = volumeSlider.value;
        SoundCtrl.Instance.ChangeVolume(volumeValue);
    }
    public void ButtonSound()
    {
        SoundCtrl.Instance.AudioButton();

    }


    public void SaveData()
    {
        string filePath = Application.persistentDataPath + "/data.bin";

        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            writer.Write(currentLV);
            writer.Write(volumeValue);
        }
    }



    public void LoadData()
    {
        string filePath = Application.persistentDataPath + "/data.bin";

        if (File.Exists(filePath))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                currentLV = reader.ReadInt32();
                volumeValue = reader.ReadSingle();

                volumeSlider.value = volumeValue;


                int displaylv = currentLV + 1;
                displayLV.text = (displaylv).ToString();
            }

            SoundCtrl.Instance.ChangeVolume(volumeValue);
        }
        else
        {
            SaveData();
        }
    }
}
