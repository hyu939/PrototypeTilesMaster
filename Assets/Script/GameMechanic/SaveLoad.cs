using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class SaveLoad : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private int currentLV  = 0;
    [SerializeField] private float volumeValue;
    [SerializeField] private SpawnTile_Script spawnTiles;
    [SerializeField] private MapData[] mapData;

    private void Start()
    {
        LoadData();

        // Gắn MapData tương ứng của FileSave vào SpawnTile_Script
        spawnTiles.mapData = mapData[currentLV];
        SoundCtrl.Instance.ChangeVolume(volumeValue);
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
                
            }
        }
        else
        {
            SaveData();
        }
    }

    // Thay đổi giá trị của AudioMixer theo slider
    public void SliderToMixer()
    {
        
        volumeValue = volumeSlider.value;
        SoundCtrl.Instance.ChangeVolume(volumeValue);
    }

    // Nút NextLV
    public void NextLVSave()
    {
        if (currentLV == 2)
            currentLV = 0;
        else
            currentLV++;

        SaveData();
        LoadData();
    }


    public void ButtonSound()
    {
        SoundCtrl.Instance.AudioButton();

    }

}
