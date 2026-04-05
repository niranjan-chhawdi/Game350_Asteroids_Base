using TMPro;
using UnityEngine;

public class UIManager_W7_Solution : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI livesTextNum;

    private void Awake()
    {
        GameManager_W7_Solution.OnPlayerLivesChanged += OnLivesChanged;
    }

    private void Start()
    {
        UpdateLivesText();
    }

    private void OnDestroy()
    {
        GameManager_W7_Solution.OnPlayerLivesChanged -= OnLivesChanged;
    }

    private void OnLivesChanged()
    {
        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        int currentLives = GameManager_W7_Solution.Instance.lives;
        livesTextNum.text = currentLives < 0 ? "0" : currentLives.ToString();
    }
}
