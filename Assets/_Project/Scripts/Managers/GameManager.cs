using System;
using UnityEngine;

public enum GameState
{
    Upgrade,
    KaijuControl,
    Pause
}

public class GameManager : MonoBehaviour
{
    public float timerMaxAmount;
    public float timerCurrentAmount;
    public int coin;

    public GameState currentState;

    public static GameManager Instance;

    public static Action<GameState> OnGameStateChanged;
    public GameState CurrentState => currentState;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timerCurrentAmount = timerMaxAmount;
    }

    void Update()
    {
        UpdateGameTimer();
        InterfaceManager.Instance.UpdateTimer(timerCurrentAmount);
    }

    public void AddCoin(int amount)
    {
        coin += amount;
        InterfaceManager.Instance.UpdateCoin(coin);
    }

    public bool AttemptRemoveCoin(int amount)
    {
        if (amount > coin) return false;
        coin -= amount;
        InterfaceManager.Instance.UpdateCoin(coin);
        return true;
    }

    private void UpdateGameTimer()
    {
        if (CurrentState != GameState.KaijuControl) return;
        timerCurrentAmount -= Time.deltaTime;
        if (timerCurrentAmount <= 0)
        {
            timerCurrentAmount = 0;
            ChangeGameState(GameState.Upgrade);
        }
    }

    public void EndUpgradePhase()
    {
        timerCurrentAmount = timerMaxAmount;
        ChangeGameState(GameState.KaijuControl);
    }

    private void ChangeGameState(GameState newState)
    {
        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);
    }
}
