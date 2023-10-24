using UnityEngine;


public class Tile_Script : MonoBehaviour
{
    // id sẽ đc tự quy định thông qua MapData
    public int id;
    private Collector collector;

    private void Awake()
    {
        collector = GameObject.FindWithTag("Colletor").GetComponent<Collector>();
        

    }
    private void OnMouseDown()
    {
        if (!GamePause.Instance.isPause)
        {
            // Thêm id vào chuỗi collect
            collector.TilesArray(id);
            Destroy(gameObject);
        }
    }
}
