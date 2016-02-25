using UnityEngine;
using System.Collections;

public class MeteorRain : MonoBehaviour {

    public static MeteorRain Instance;

	public GameObject Meteor;

	public Vector2 SpawnAreaSize, SpawnVelocity;
	public int Amount;
	public float WaitBetweenMeteors, WaitTimer;

    private bool started;

    public bool HasStarted
    {
        get { return started; }
    }
	
    void Awake()
    {
        Instance = this;
    }

	void Update() 
    {
        if (started) return;

        bool blocksLeft = UIManager.Instance.WallCounter.Count < UIManager.Instance.WallCounter.Total;

        WaitTimer -= Time.deltaTime;

        if (WaitTimer<0)
        {
            StartCoroutine(SpawnWaves());
            started = true;
        }
	}
	
	IEnumerator SpawnWaves () 
    {
		for (int i = 0; i < Amount; i++)
		{
			Vector3 spawnPosition = 
                new Vector3(
                    transform.position.x + Random.Range(-0.5f,0.5f) * SpawnAreaSize.x,
                    transform.position.y + Random.Range(-0.5f, 0.5f) * SpawnAreaSize.y, 
                    0.0f);

			var meteor = (GameObject)Instantiate(Meteor, spawnPosition, Quaternion.identity);

            meteor.GetComponent<Rigidbody2D>().velocity = SpawnVelocity;

			yield return new WaitForSeconds(WaitBetweenMeteors);
		}

        GameManager.Instance.RainEnded();
	}

    void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;

        //visualize the spawn area
        Gizmos.DrawWireCube(transform.position, new Vector3(SpawnAreaSize.x, SpawnAreaSize.y, 0.0f));

        //draw a line showing the initial direction of the meteors
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(SpawnVelocity.x, SpawnVelocity.y, 0.0f));
    }
	
	
}
