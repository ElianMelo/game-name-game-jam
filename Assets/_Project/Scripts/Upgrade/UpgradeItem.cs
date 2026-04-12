using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
    public bool isKaijuKnowdlge;
    public float amountValue;
    public int upgradeCost;

    [Header("Blend Shape")]
    public bool hasBlendShape;
    public BlendShapeType blendShapeType;
    public float blendShapeAmount;

    public UnityEvent OnMouseEnter;
    public UnityEvent OnMouseExit;
    public UnityEvent OnUpgradeProgress;
    public UnityEvent OnUpgradeUnlock;

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
        OnMouseEnter?.Invoke();
        TooltipSystemManager.Show($"{ConvertUpgradeTypeToText(upgradeType)} \n Amount: {amountValue} \n Cost: {upgradeCost}");
        if (isUnlocked) return;
        background.color = hoverColor;
        background.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit?.Invoke();
        TooltipSystemManager.Hide();
        if (isUnlocked) return;
        background.gameObject.SetActive(false);
    }

    private string ConvertUpgradeTypeToText(UpgradeType upgradeType)
    {   
        switch (upgradeType)
        {
            case UpgradeType.Damage:
            case UpgradeType.Range:
            case UpgradeType.Speed:
            case UpgradeType.Cooldown:
            case UpgradeType.Health:
            case UpgradeType.Unlock:
                return upgradeType.ToString();
            case UpgradeType.AttackSpeed:
                return "Attack Speed";
            case UpgradeType.SpawnAmount:
                return "Spawn Units";
            case UpgradeType.SpawnSpeed:
                return "Spawn Speed";
        }
        return "";
    }

    private void AttempBuyUpgrade()
    {
        if (isUnlocked) return;
        bool brought = isKaijuKnowdlge ? GameManager.Instance.AttemptRemoveKaijuuKnowledge(upgradeCost) : GameManager.Instance.AttemptRemoveTroopDamage(upgradeCost);
        if (!brought) return;
        currentPhase += 1;
        phaseText.text = $"{currentPhase} / {phases}";
        ApplyUpgradeEffect();
        OnUpgradeProgress?.Invoke();
        if (currentPhase == phases)
            UnlockUpgrade();
    }

    private void ApplyUpgradeEffect()
    {
        if (hasBlendShape)
        {
            KaijuUpgradeManager.Instance.Controller.PlayerBlendShapesController.AddToBlendShape(blendShapeType, blendShapeAmount);
        }
        switch (upgradeClass)
        {
            case UpgradeClass.Kaijuu: KaijuUpgradeManager.Instance.BuyUpgrade(upgradeType, kaijuuAttributeGroup, amountValue); return;
            case UpgradeClass.Troop: TroopUpgradeManager.Instance.BuyUpgrade(upgradeType, troopName, amountValue); return;
        }
    }

    private void UnlockUpgrade()
    {
        isUnlocked = true;
        OnUpgradeUnlock?.Invoke();
        background.color = unlockedColor;
    }
}
