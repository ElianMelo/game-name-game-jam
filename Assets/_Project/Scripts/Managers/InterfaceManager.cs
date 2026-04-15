using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public static InterfaceManager Instance;

    [SerializeField] private Image kaijuHealthBar;
    [SerializeField] private Image progressBar;
    [SerializeField] private TMP_Text runTimer;
    [SerializeField] private TMP_Text kaijuKnowledgeField;
    [SerializeField] private TMP_Text troopDamageField;

    [Header("Game Flow")]
    [SerializeField] private GameObject startGameScreen;
    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private Button playGameButton;
    [SerializeField] private Button playAgainGameButton;

    private float currentProgress;
    private float maxProgress = 198f;

    private void Awake()
    {
        Instance = this;
        playGameButton.onClick.AddListener(OnPlayGameButton);
        playAgainGameButton.onClick.AddListener(OnPlayAgainGameButton);
    }

    private void OnDestroy()
    {
        playGameButton.onClick.RemoveAllListeners();
        playAgainGameButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        progressBar.fillAmount = 0;
        ShowStartGameScreen();
    }

    private void OnPlayGameButton()
    {
        HideStartGameScreen();
        GameManager.Instance.ChangeGameState(GameState.KaijuControl);
    }

    private void OnPlayAgainGameButton()
    {
        Application.Quit();
    }

    public void UpdateProgressBar(float amount = 1f)
    {
        currentProgress += amount;
        if (currentProgress >= maxProgress)
        {
            currentProgress = maxProgress;
            GameManager.Instance.FlagToEndGame();
        }
        progressBar.fillAmount = currentProgress / maxProgress;
    }
    public void ShowStartGameScreen()
    {
        startGameScreen.SetActive(true);
    }
    public void HideStartGameScreen()
    {
        startGameScreen.SetActive(false);
    }
    public void ShowEndGameScreen()
    {
        endGameScreen.SetActive(true);
    }

    public void UpdateKaijuHealth(float currentHealth, float maxHealth)
    {
        kaijuHealthBar.fillAmount = currentHealth / maxHealth;
    }

    public void UpdateTimer(float currentTimer)
    {
        runTimer.text = currentTimer.ToString("F0");
    }

    public void UpdateKaijuKnowledge(int coinAmount)
    {
        kaijuKnowledgeField.text = coinAmount.ToString();
    }

    public void UpdateTroopDamage(int coinAmount)
    {
        troopDamageField.text = coinAmount.ToString();
    }
}
