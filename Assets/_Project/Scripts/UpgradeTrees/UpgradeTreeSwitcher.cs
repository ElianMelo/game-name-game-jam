using System;
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

    public GameObject unlockBurstTree;
    public GameObject unlockAreaTree;
    public GameObject unlockProjectileTree;

    public static Action<KaijuuAttibuteGroupType> UnlockKaijuuSkillTreePath { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playGameButton.onClick.AddListener(OnPlayGameButtonPressed);
        kaijuuTreeButton.onClick.AddListener(SwitchKaijuuTree);
        troopsTreeButton.onClick.AddListener(SwitchTroopsTree);
        GameManager.OnGameStateChanged = OnGameStateChanged;
        UnlockKaijuuSkillTreePath += OnUnlockKaijuuSkillTreePath;
    }
    private void OnDestroy()
    {
        playGameButton.onClick.RemoveAllListeners();
        kaijuuTreeButton.onClick.RemoveAllListeners();
        troopsTreeButton.onClick.RemoveAllListeners();
        UnlockKaijuuSkillTreePath -= OnUnlockKaijuuSkillTreePath;
    }

    private void OnUnlockKaijuuSkillTreePath(KaijuuAttibuteGroupType groupType)
    {
        switch (groupType)
        {
            case KaijuuAttibuteGroupType.SkillAOE: UnlockAreaTree(); return;
            case KaijuuAttibuteGroupType.SkillBurst: UnlockBurstTree(); return;
            case KaijuuAttibuteGroupType.SkillProjectiles: UnlockProjectileTree(); return;
        }
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

    private void UnlockBurstTree()
    {
        unlockBurstTree.SetActive(true);
    }
    private void UnlockAreaTree()
    {
        unlockAreaTree.SetActive(true);
    }
    private void UnlockProjectileTree()
    {
        unlockProjectileTree.SetActive(true);
    }
}
