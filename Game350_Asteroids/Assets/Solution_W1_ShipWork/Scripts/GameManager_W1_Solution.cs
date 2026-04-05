using UnityEngine;

public class GameManager_W1_Solution : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab; //prefab to spawn
    private GameObject player; //keep track of player object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        Debug.Log("[GM] Starting Asteroids Game");

        player = GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

    }

}
