using TMPro;
using UnityEngine;

public class UIManager_OB_Solution : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI livesTextNum;//text field for showing lives

    private GameManager_W7_Solution gameManager;

    private void Awake()
    {
        Debug.Log("[UIMANAGER] Awake");
        gameManager = GameManager_W7_Solution.Instance;

        GameManager_W7_Solution.OnPlayerLivesChanged += OnLivesChanged;

        Debug.Log("[UIMANAGER] Done");
    }

    private void OnDestroy()
    {
        GameManager_W7_Solution.OnPlayerLivesChanged -= OnLivesChanged;
    }

    private void OnLivesChanged()
    {
        Debug.Log("[UIMANAGER] Lives Changed");

        int newLife = GameManager_W7_Solution.Instance.lives;

        livesTextNum.text = newLife < 0 ? "0" : newLife.ToString();
    }
}
