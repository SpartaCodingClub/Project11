using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingObject : MonoBehaviour
{
    private int playerLayer;
    private Tilemap[] tilemaps;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer(Define.Player);
        tilemaps = gameObject.GetComponentsInChildren<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != playerLayer)
        {
            return;
        }

        foreach (var tilemap in tilemaps)
        {
            tilemap.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != playerLayer)
        {
            return;
        }

        foreach (var tilemap in tilemaps)
        {
            tilemap.color = Color.white;
        }
    }
}