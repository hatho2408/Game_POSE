using UnityEngine;

public class TileRow_2048 : MonoBehaviour
{
    public TileCell_2048[] cells { get; private set; }

    private void Awake()
    {
        cells = GetComponentsInChildren<TileCell_2048>();
    }

}
