using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color unlockedColor;
    public Color lockedColor;
    public Color hoverColor;
    
    public int phases;
    public TMP_Text phaseText;

    public Image background;
    public Button selfButton;

    [Header("Upgrade Info")]
    public UpgradeClass upgradeClass;
    public UpgradeType upgradeType;
    public KaijuuAttibuteGroupType kaijuuAttributeGroup;
    public TroopName troopName;
    public float amountValue;
    public int upgradeCost;

    private bool isUnlocked;
    private int currentPhase = 0;

    void Start()
    {
        selfButton.onClick.AddListener(AttempBuyUpgrade);
        background.gameObject.SetActive(false);
        phaseText.text = $"{currentPhase} / {phases}";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystemManager.Show($"{upgradeType.ToString()} \n Amount: {amountValue} \n Cost: {upgradeCost}");
        if (isUnlocked) return;
        background.color = hoverColor;
        background.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystemManager.Hide();
        if (isUnlocked) return;
        background.gameObject.SetActive(false);
    }

    private void AttempBuyUpgrade()
    {
        if (isUnlocked) return;
        bool brought = GameManager.Instance.AttemptRemoveCoin(upgradeCost);
        if (!brought) return;
        currentPhase += 1;
        phaseText.text = $"{currentPhase} / {phases}";
        ApplyUpgradeEffect();
        if (currentPhase == phases)
            UnlockUpgrade();
    }

    private void ApplyUpgradeEffect()
    {
        switch (upgradeClass)
        {
            case UpgradeClass.Kaijuu: KaijuUpgradeManager.Instance.BuyUpgrade(upgradeType, kaijuuAttributeGroup, amountValue); return;
            case UpgradeClass.Troop: TroopUpgradeManager.Instance.BuyUpgrade(upgradeType, troopName, amountValue); return;
        }
    }

    private void UnlockUpgrade()
    {
        isUnlocked = true;
        background.color = unlockedColor;
    }
}
