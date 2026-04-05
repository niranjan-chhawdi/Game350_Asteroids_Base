using UnityEngine;

public class GameManager_W6_Solution : MonoBehaviour
{
    public static GameManager_W6_Solution Instance;

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject player;

    [SerializeField]
    private AsteroidSpawner_W6_Solution asteroidSpawner;

    [SerializeField]
    float spawnDistance = 1f;
    [SerializeField]
    float playerRespawnDelay = 3f;

    private Bounds screenBounds;

    int score = 0;



    //NO LONGER NEEDED
    //[SerializeField]
    //float spawnDistance = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SetupBounds();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    void SetupBounds()
    {
        //Debug.Log("SETTING UP BOUNDS");
        //calculate level bounds by using camera and camera's screen bounds
        //Credit to: (Graham, 2021)
        screenBounds = new Bounds();
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(Vector3.zero));
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)));
    }

    void StartGame()
    {
        Debug.Log("[GM] Starting Asteroids Game");

        player = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        asteroidSpawner.Initialize();

        //NO LONGER NEEDED
        //InvokeRepeating(nameof(SpawnAsteroid), 2, 10);//TEMPORARY
    }

    public void ScoreOnAsteroidDestroy()
    {
        //TODO: play destroy effects, needs ref. to asteroid

        //TODO: calculate score based on asteroid size, needs ref. to asteroid
        score = score + 1; //note: could adjust scoring based on asteroid size, rewarding more points for smaller objects

        Debug.Log("SCORE: " + score);
    }

    public Bounds GetBounds()
    {
        return screenBounds;
    }

    private void PlayerRespawn()
    {
        player.GetComponent<Player_W6_Solution>().ResetPlayer();
        player.gameObject.SetActive(true);
        Debug.Log("[GAME MANAGER] PLAYER RESPAWNED");
    }

    public void OnPlayerDeath(Player_W6_Solution player)
    {

        Debug.Log("[GAME MANAGER] PLAYER DIED");
        player.gameObject.SetActive(false);

        //explosion effect here

        //set lives here

        Invoke(nameof(PlayerRespawn), playerRespawnDelay);

    }

    //NO LONGER NEEDED

    /*
    void SpawnAsteroid() //TEMPORARY
    {

        //set spawn point based on random point in a unit circle
        Vector3 randomPoint = Random.insideUnitCircle;
        Vector3 spawnPoint = Vector3.zero + (randomPoint * spawnDistance);

        GameObject asteroidGO = Instantiate(asteroidPrefab, spawnPoint, Quaternion.identity);
        asteroidGO.GetComponent<Asteroid_W4_Solution>().SetRandomSize();

    }*/

}
