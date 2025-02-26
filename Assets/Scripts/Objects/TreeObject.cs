using UnityEngine;

public class TreeObject : MonoBehaviour
{
    private int playerLayer;
    private SpriteRenderer[] spriteRenderers;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer(Define.Player);
        spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != playerLayer)
        {
            return;
        }

        foreach (var spriteRederer in spriteRenderers)
        {
            spriteRederer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != playerLayer)
        {
            return;
        }

        foreach (var spriteRederer in spriteRenderers)
        {
            spriteRederer.color = Color.white;
        }
    }
}