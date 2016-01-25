using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour, IDamageable 
{
	public int health;
    private bool isDead;
	
	public void TakeDamage(int amount) 
    {
        if (isDead) return;

        health -= amount;

        if (health <= 0)
        {
            ScoreCounter.Instance.Count--;

            isDead = true;
            Destroy(gameObject);
        }
	}
}