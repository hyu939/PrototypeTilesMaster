using System;
using System.Linq;
using UnityEngine;



public class Collector : MonoBehaviour
{
    public int[] colectTilesId = new int[0];
    private int size;
    [SerializeField]private Score score;
    public Collector_Spawn collector_Spawn;

    [SerializeField] private SpawnTile_Script spawn_Script;
    private int tileCollected = 0;

    public GameObject winWindow, loseWindow;

    void Awake()
    {
        colectTilesId = new int[0];
        score = GameObject.FindWithTag("Spawn").GetComponent<Score>();
        collector_Spawn = GameObject.FindWithTag("Spawn").GetComponent<Collector_Spawn>();
        spawn_Script = GameObject.FindWithTag("Spawn").GetComponent<SpawnTile_Script>();
    }

    public void TilesArray(int id)
    {
        SoundCtrl.Instance.AudioPick();

        // Tăng thêm kích thức cho chuỗi 
        size++;
        Array.Resize<int>(ref colectTilesId, size);

        // Thêm dữ liệu vào chuỗi ở vị trí cuối
        colectTilesId[size - 1] = id;

        DestroyCollected();

        // Sắp xếp mảng theo thứ tự tăng dần
        Array.Sort(colectTilesId);

        SpawnObject();

        for (int i = 0; i < colectTilesId.Length; i++)
        {
            // Đếm số lần xuất hiện của phần tử hiện tại
            int count = 1;
            while (i < colectTilesId.Length - 1 && colectTilesId[i] == colectTilesId[i + 1])
            {
                count++;
                i++;
            }

            // Nếu số lần xuất hiện bằng 3, in ra phần tử trùng lặp 3 lần
            if (count == 3)
            {
                SoundCtrl.Instance.AudioScorePlus();
                Debug.Log("Phần tử trùng lặp 3 lần: " + colectTilesId[i]);
                score.ScorePlus();
                DestroyCollected();

                colectTilesId = colectTilesId.Where(x => x != colectTilesId[i]).ToArray();
                size = size - 3;

                SpawnObject();
            }
        }

        // Nếu chuỗi có 6 phần tử => thua
        if(size >= 6)
        {
            SoundCtrl.Instance.AudioLose();
            GamePause.Instance.PauseGame();
            loseWindow.SetActive(true);
        }

        // Kiểm tra điều kiện thắng
        WinCondition();
        
    }

    // Hủy các vật thể đã spawn của colectTilesId
    void DestroyCollected()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Collected");
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    // Sinh các vật thể có trong chuỗi colectTilesId
    void SpawnObject()
    {
        
        for (int j = 0; j < colectTilesId.Length; j++)
        {
            collector_Spawn.SpawnObjectWithTag(colectTilesId[j], j);
        }
    }

    void WinCondition()
    {
        tileCollected++;
        // Kiểm tra xem tất cả tile đã nhặt đủ chưa
        if (tileCollected == spawn_Script.mapData.tiles.Sum(tiles => tiles.chance)*3)
        {
            SoundCtrl.Instance.AudioWin();
            GamePause.Instance.PauseGame();
            winWindow.SetActive(true);
        }
    }

}