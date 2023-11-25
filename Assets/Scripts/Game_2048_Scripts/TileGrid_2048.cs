using UnityEngine;

public class TileGrid_2048 : MonoBehaviour
{
    public TileRow_2048[] rows { get; private set; }
    public TileCell_2048[] cells { get; private set; }

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;

    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow_2048>();
        cells = GetComponentsInChildren<TileCell_2048>();

        for (int i = 0; i < cells.Length; i++) {
            cells[i].coordinates = new Vector2Int(i % width, i / width);
        }
    }

    public TileCell_2048 GetCell(Vector2Int coordinates)
    {
        return GetCell(coordinates.x, coordinates.y);
    }

    public TileCell_2048 GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height) {
            return rows[y].cells[x];
        } else {
            return null;
        }
    }

    public TileCell_2048 GetAdjacentCell(TileCell_2048 cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates);
    }

    public TileCell_2048 GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        while (cells[index].occupied)
        {
            index++;

            if (index >= cells.Length) {
                index = 0;
            }

            // all cells are occupied
            if (index == startingIndex) {
                return null;
            }
        }

        return cells[index];
    }

}
