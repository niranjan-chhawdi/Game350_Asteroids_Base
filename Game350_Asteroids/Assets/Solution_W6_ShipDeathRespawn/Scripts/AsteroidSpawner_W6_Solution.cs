using UnityEngine;

public class AsteroidSpawner_W6_Solution : MonoBehaviour
{
    public GameObject asteroidPrefab; //the asteroid itself

    [SerializeField]
    private float spawnDistance = 1f; //how far from a chosen point to spawn

    [SerializeField]
    private float spawnRate = 10f; //how often to spawn
    [SerializeField]
    private int amountPerSpawn = 1; //how many at a time to spawn

    public void Initialize()
    {
        Debug.Log("[ASTEROID_SPAWNER] Initializing");
        InvokeRepeating(nameof(Spawn), 2, spawnRate);
    }

    public void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            // 
            Vector3 randomPoint = Random.insideUnitCircle.normalized; //choose initial point inside of a circle
            Vector3 spawnPoint = transform.position + (randomPoint * spawnDistance); //note uses object position, make sure this is in the center
            //spawns from center - in a direction - a distance away

            GameObject asteroidGO = Instantiate(asteroidPrefab, spawnPoint, Quaternion.identity);
            asteroidGO.GetComponent<Asteroid_W6_Solution>().SetRandomSize();
        }
    }
}
