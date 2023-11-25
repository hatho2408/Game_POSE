using UnityEngine;

public class TileCell_2048 : MonoBehaviour
{
    public Vector2Int coordinates { get; set; }
    public Tile_2048 tile { get; set; }

    public bool empty => tile == null;
    public bool occupied => tile != null;
}
