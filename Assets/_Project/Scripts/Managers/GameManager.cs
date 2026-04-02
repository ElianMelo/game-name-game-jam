using System;
using UnityEngine;

public enum GameState
{
    Upgrade,
    KaijuControl
}

public class GameManager : MonoBehaviour
{
    public float timerMaxAmount;
    public float timerCurrentAmount;

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
        ChangeGameState(GameState.KaijuControl);
    }

    private void ChangeGameState(GameState newState)
    {
        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);
    }
}
