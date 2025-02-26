using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : BaseController
{
    #region Inspector
    [SerializeField]
    private Tile newTile;
    #endregion

    private Tilemap tilemap;

    protected override void Initialize()
    {
        base.Initialize();
        tilemap = transform.GetChild(0).GetChild(0).GetComponent<Tilemap>();

        GenerateMap();
    }

    public void GenerateMap()
    {
        BoundsInt bounds = tilemap.cellBounds;
        for (int i = 0; i < 20; i++)
        {
            int x = Random.Range(bounds.xMin, bounds.xMax);
            int y = Random.Range(bounds.yMin, bounds.yMax);
            Vector3Int randomPosition = new(x, y);

            tilemap.SetTile(randomPosition, newTile);
        }
    }
}