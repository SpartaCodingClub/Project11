using System.Collections;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    #region Inspector
    [SerializeField]
    private int chestCount = 10;
    #endregion

    private readonly string CHEST = "Chest";
    private readonly string CHESTEFFECT = "ChestEffect";
    private readonly float MIN_X = -20.5f, MAX_X = 24.5f;
    private readonly float MIN_Y = -4.1f, MAX_Y = 20.5f;

    //콜라이더 박스 사이즈
    private readonly Vector2 boxSize = new(3.0f, 3.0f);

    private readonly WaitForSeconds interval = new(0.2f);
    

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        for (int i = 0; i < chestCount; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                int layerMask = LayerMask.GetMask(Define.Player);
                Vector2 randomPos = GetRandomPosition();
                if (Physics2D.OverlapBox(randomPos, boxSize, 0))
                {
                    continue;
                }
                if (Physics2D.OverlapCircle(randomPos, 9f, layerMask))
                {
                    Managers.Audio.Play(Clip.SoundFX_CreateItem);
                }
                Managers.Resource.Instantiate(CHEST, null, randomPos);
                Managers.Resource.Instantiate(CHESTEFFECT, null, randomPos, Define.EFFECT).GetComponent<ObjectController>().Death();
                yield return interval;
                break;
            }
        }
    }

    //랜덤 생산구역
    public Vector2 GetRandomPosition()
    {
        return new Vector2(
            Random.Range(MIN_X, MAX_X),
            Random.Range(MIN_Y, MAX_Y)
        );
    }
}