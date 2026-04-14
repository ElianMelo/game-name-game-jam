using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTreeSwitcher : MonoBehaviour
{
    public Button kaijuuTreeButton;
    public Button troopsTreeButton;
    public GameObject kaijuuTreeSelected;
    public GameObject troopsTreeSelected;
    public Button playGameButton;
    public GameObject visuals;

    public UpgradeTreeController kaijuuTreeControl;
    public UpgradeTreeController troopsTreeControl;

    [Header("Kaijuu Unlock")]
    public GameObject unlockBurstTree;
    public GameObject unlockAreaTree;
    public GameObject unlockProjectileTree;
    [Header("Troop Unlock")]
    public GameObject unlockRiderTree;
    public GameObject unlockCrossbowTree;
    public GameObject unlockCatapultTree;

    public static Action<KaijuuAttibuteGroupType> UnlockKaijuuSkillTreePath { get; set; }
    public static Action<TroopName> UnlockTroopTreePath { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        kaijuuTreeSelected.SetActive(true);
        playGameButton.onClick.AddListener(OnPlayGameButtonPressed);
        kaijuuTreeButton.onClick.AddListener(SwitchKaijuuTree);
        troopsTreeButton.onClick.AddListener(SwitchTroopsTree);
        GameManager.OnGameStateChanged = OnGameStateChanged;
        UnlockKaijuuSkillTreePath += OnUnlockKaijuuSkillTreePath;
        UnlockTroopTreePath += OnUnlockTroopTreePath;
    }
    private void OnDestroy()
    {
        playGameButton.onClick.RemoveAllListeners();
        kaijuuTreeButton.onClick.RemoveAllListeners();
        troopsTreeButton.onClick.RemoveAllListeners();
        UnlockKaijuuSkillTreePath -= OnUnlockKaijuuSkillTreePath;
        UnlockTroopTreePath -= OnUnlockTroopTreePath;
    }

    private void OnUnlockKaijuuSkillTreePath(KaijuuAttibuteGroupType groupType)
    {
        switch (groupType)
        {
            case KaijuuAttibuteGroupType.SkillAOE: UnlockAreaTree(); return;
            case KaijuuAttibuteGroupType.SkillBurst: UnlockBurstTree(); return;
            case KaijuuAttibuteGroupType.SkillRoll: UnlockRollTree(); return;
        }
    }

    private void OnUnlockTroopTreePath(TroopName troopName)
    {
        switch (troopName)  
        {
            case TroopName.Rider: UnlockRiderTree(); return;
            case TroopName.Crossbow: UnlockCrossbowTree(); return;
            case TroopName.Catapult: UnlockCatapultTree(); return;
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
        SoundManager.Instance.UIStartBattle();
        GameManager.Instance.EndUpgradePhase();
    }

    private void SwitchKaijuuTree()
    {
        kaijuuTreeControl.ShowVisuals();
        troopsTreeControl.HideVisuals();
        kaijuuTreeSelected.SetActive(true);
        troopsTreeSelected.SetActive(false);
        SoundManager.Instance.UISwitchTab();
    }

    private void SwitchTroopsTree()
    {
        troopsTreeControl.ShowVisuals();
        kaijuuTreeControl.HideVisuals();
        troopsTreeSelected.SetActive(true);
        kaijuuTreeSelected.SetActive(false);
        SoundManager.Instance.UISwitchTab();
    }

    private void UnlockBurstTree()
    {
        unlockBurstTree.SetActive(true);
    }
    private void UnlockAreaTree()
    {
        unlockAreaTree.SetActive(true);
    }
    private void UnlockRollTree()
    {
        unlockProjectileTree.SetActive(true);
    }
    private void UnlockRiderTree()
    {
        unlockRiderTree.SetActive(true);
    }
    private void UnlockCrossbowTree()
    {
        unlockCrossbowTree.SetActive(true);
    }
    private void UnlockCatapultTree()
    {
        unlockCatapultTree.SetActive(true);
    }
}
