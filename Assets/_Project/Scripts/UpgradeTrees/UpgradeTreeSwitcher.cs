using UnityEngine;
using UnityEngine.UI;

public class UpgradeTreeSwitcher : MonoBehaviour
{
    public Button kaijuuTreeButton;
    public Button troopsTreeButton;
    public Button playGameButton;
    public GameObject visuals;

    public UpgradeTreeController kaijuuTreeControl;
    public UpgradeTreeController troopsTreeControl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playGameButton.onClick.AddListener(OnPlayGameButtonPressed);
        kaijuuTreeButton.onClick.AddListener(SwitchKaijuuTree);
        troopsTreeButton.onClick.AddListener(SwitchTroopsTree);
        GameManager.OnGameStateChanged = OnGameStateChanged;
    }
    private void OnDestroy()
    {
        playGameButton.onClick.RemoveAllListeners();
        kaijuuTreeButton.onClick.RemoveAllListeners();
        troopsTreeButton.onClick.RemoveAllListeners();
    }
        
    private void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.KaijuControl) HideVisuals();
        if (gameState == GameState.Upgrade) ShowVisuals();
    }

    private void ShowVisuals()
    {
        visuals.SetActive(true);
    }

    private void HideVisuals()
    {
        visuals.SetActive(false);
    }

    private void OnPlayGameButtonPressed()
    {
        GameManager.Instance.EndUpgradePhase();
    }

    private void SwitchKaijuuTree()
    {
        kaijuuTreeControl.ShowVisuals();
        troopsTreeControl.HideVisuals();
    }

    private void SwitchTroopsTree()
    {
        troopsTreeControl.ShowVisuals();
        kaijuuTreeControl.HideVisuals();
    }
}
