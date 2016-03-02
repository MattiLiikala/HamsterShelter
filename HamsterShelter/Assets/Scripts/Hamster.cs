using UnityEngine;
using System.Collections;

public class Hamster : MonoBehaviour, IDamageable 
{
	public int health;
    private bool isDead;
    public GameObject SpawnOnContact;

    void Start()
    {
        ScoreCounter.Instance.Count++;
    }
	
	public void TakeDamage(int amount) 
    {
        if (isDead) return;

        health -= amount;

        if (health > 0) return;
        
        ScoreCounter.Instance.Count--;

        if (ScoreCounter.Instance.Count <= 0)
        {
            GameManager.Instance.GameOver();
        }

        isDead = true;

        if (SpawnOnContact != null)
        {
            Instantiate(SpawnOnContact, transform.position, transform.rotation);
        }

        Destroy(gameObject);
	}
}