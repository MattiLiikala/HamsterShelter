using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour, IDamageable
{

	public int Durability;
    private int health;

    public GameObject SpawnWhenDestroyed;

    public Sprite[] Sprites;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (Sprites.Length==0)
        {
            Debug.LogError("No sprites configured for wall, using default sprite");
            Sprites = new Sprite[] { spriteRenderer.sprite };
        }

        health = Durability;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        int spriteIndex = Mathf.FloorToInt((1.0f - ((float)health / Durability)) * Sprites.Length);

        spriteRenderer.sprite = Sprites[Mathf.Clamp(spriteIndex, 0, Sprites.Length-1)];

        //todo: add some visual effect when the wall is destroyed (particles, pieces flying off or something)
        if (health <= 0)
        {
            if (SpawnWhenDestroyed != null)
            {
                Instantiate(SpawnWhenDestroyed, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
	
}
