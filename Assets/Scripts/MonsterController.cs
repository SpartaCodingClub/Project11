using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 몬스터 죽을 때 처리
        Destroy(gameObject);
    }
}