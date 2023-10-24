using UnityEngine;


public class Collector_Spawn : MonoBehaviour
{
    private SpawnTile_Script spawnTile;

    // Vị trí điểm spawn ở các cái đĩa collect
    public Transform[] spawnLocal;
    private Collector collector;


    private void Awake()
    {
        spawnTile = GetComponent<SpawnTile_Script>();
        collector = GameObject.FindWithTag("Colletor").GetComponent<Collector>();

    }

    // Sinh ra vật thể mới chọn với tag Collected (tag này giúp tìm và hủy object, hủy bên collector)
    public void SpawnObjectWithTag(int id, int slot)
    {
        GameObject newObject = Instantiate(spawnTile.mapData.tiles[id].tilesPrefab, spawnLocal[slot].position, spawnLocal[slot].rotation);
        newObject.tag = "Collected";
    }

    // Sinh ra vật thể mới chọn với tag Collected
    public void SpawnObjectAfterDestroy(int id, int slot)
    {
        for (int i = 0; i < collector.colectTilesId.Length; i++)
        {
            GameObject newObject = Instantiate(spawnTile.mapData.tiles[id].tilesPrefab, spawnLocal[slot].position, spawnLocal[slot].rotation);
            newObject.tag = "Collected";
        }

    }
}
