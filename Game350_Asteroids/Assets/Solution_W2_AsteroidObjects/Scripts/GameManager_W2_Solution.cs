using UnityEngine;

public class GameManager_W2_Solution : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab, asteroidPrefab;
    private GameObject player;

    [SerializeField]
    float spawnDistance = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        Debug.Log("[GM] Starting Asteroids Game");

        player = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        InvokeRepeating(nameof(SpawnAsteroid), 2, 2);//TEMPORARY
    }

    void SpawnAsteroid() //TEMPORARY
    {

        //set spawn point based on random point in a unit circle
        Vector3 randomPoint = Random.insideUnitCircle;
        Vector3 spawnPoint = Vector3.zero + (randomPoint * spawnDistance);

        GameObject asteroidGO = Instantiate(asteroidPrefab, spawnPoint, Quaternion.identity);

    }

}
