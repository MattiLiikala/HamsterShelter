using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour, IDamageable {

	public int durability;

    public void TakeDamage(int amount)
    {
        durability -= amount;

        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }
	
}
