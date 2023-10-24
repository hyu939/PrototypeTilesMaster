using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "MapData")]
public class MapData : ScriptableObject
{
    // Thông tin cơ bản của map
    public string mapname;
    public int lv;
    public int time;

    // Danh sách các tile có trong map
    [SerializeField] public List<Tile> tiles;
    
}

[System.Serializable]
public class Tile
{
    public GameObject tilesPrefab;
    public int chance;
}

