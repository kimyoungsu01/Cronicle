using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int hp = 100;
    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0) Die();
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
