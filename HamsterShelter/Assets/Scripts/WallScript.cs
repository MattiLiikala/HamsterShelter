using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour, IDamageable
{

	public int Durability;
    private int health;

    public GameObject SpawnWhenDestroyed;

    public Sprite[] Sprites;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigidBody;

    //Determines which wall is currently selected, used with rotating btn
    private static GameObject SelectedWall = null;

    void Start()
    {

        spriteRenderer  = GetComponent<SpriteRenderer>();
        rigidBody       = GetComponent<Rigidbody2D>();

        if (Sprites.Length==0)
        {
            Debug.LogError("No sprites configured for wall, using default sprite");
            Sprites = new Sprite[] { spriteRenderer.sprite };
        }

        health = Durability;
    }

    void Update()
    {
        //rigidBody.isKinematic = MeteorRain.Instance == null || !MeteorRain.Instance.HasStarted;
        if (MeteorRain.Instance.HasStarted) rigidBody.isKinematic = false;
        else rigidBody.isKinematic = true;

        //If a wall is selected, and mouse is clicked outside of any walls, unselect the selected
        //TODO
    }

    void OnMouseDown()
    {
        SelectWall(this.gameObject);
    }

    public void SelectWall(GameObject wall)
    {
        if (SelectedWall != null) SelectedWall.GetComponent<SpriteRenderer>().color = Color.white;
        SelectedWall = wall;
        SelectedWall.GetComponent<SpriteRenderer>().color = Color.blue;
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

    public void RotateWall()
    {
        SelectedWall.transform.Rotate(0, 0, 90);
    }
	
}
