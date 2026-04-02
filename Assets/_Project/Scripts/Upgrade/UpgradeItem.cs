using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color unlockedColor;
    public Color lockedColor;
    public Color hoverColor;
    public int upgradeCost;
    public int phases;
    public TMP_Text phaseText;

    public Image background;
    public Button selfButton;

    private bool isUnlocked;
    private int currentPhase = 0;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUnlocked) return;
        background.color = hoverColor;
        background.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isUnlocked) return;
        background.gameObject.SetActive(false);
    }

    void Start()
    {
        selfButton.onClick.AddListener(AttempBuyUpgrade);
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
        // todo: upgrade effect
    }

    private void UnlockUpgrade()
    {
        isUnlocked = true;
        background.color = unlockedColor;
    }
}
