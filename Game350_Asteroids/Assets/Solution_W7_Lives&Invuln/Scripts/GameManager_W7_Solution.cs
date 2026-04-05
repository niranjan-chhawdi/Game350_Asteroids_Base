using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager_W7_Solution : MonoBehaviour
{
    public static GameManager_W7_Solution Instance;

    [SerializeField]
    private GameObject playerPrefab;
    private GameObject player;

    [SerializeField]
    private AsteroidSpawner_W7_Solution asteroidSpawner;

    // [SerializeField]
    // private TextMeshProUGUI livesTextNum;//TEMPORARY; will update the lives text

    [SerializeField]
    float playerRespawnDelay = 3f;

    [SerializeField]
    private GameObject gameOverText;


    private Bounds screenBounds;

    int score = 0;

    public int lives { get; private set; } = 3; //will track the lives of the player

    public delegate void PlayerLivesChanged();
    public static event PlayerLivesChanged OnPlayerLivesChanged;



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
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }

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
        Debug.Log("[GAME_MANAGER] Starting Asteroids Game");

        SetLives(lives);

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

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public Bounds GetBounds()
    {
        return screenBounds;
    }

    private void PlayerRespawn()
    {
        player.GetComponent<Player_W7_Solution>().ResetPlayer();
        player.gameObject.SetActive(true);
        Debug.Log("[GAME_MANAGER] PLAYER RESPAWNED");
    }

    private void SetLives(int lives)
    { 
        this.lives = lives;
        Debug.Log("GAME_MANAGER: Lives_" + lives);

        OnPlayerLivesChanged?.Invoke();;
    }

    public void OnPlayerDeath(Player_W7_Solution player)
    {

        Debug.Log("[GAME_MANAGER] PLAYER DIED");
        player.gameObject.SetActive(false);

        //explosion effect here

        //set lives here
        SetLives(lives - 1);

        if (lives >= 0)
        {
            Invoke(nameof(PlayerRespawn), playerRespawnDelay);
        }      
        else
            EndGame();

    }

    private void EndGame()
    {
        Debug.Log("[GAME_MANAGER] GAME OVER");
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }

        if (player != null)
        {
            PlayerInputManager inputManager = player.GetComponent<PlayerInputManager>();
            if (inputManager != null)
            {
                inputManager.enabled = false;
            }
        }


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
