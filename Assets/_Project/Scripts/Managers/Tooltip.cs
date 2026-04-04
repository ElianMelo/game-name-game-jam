using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tooltip : MonoBehaviour
{
    public RectTransform rectTransform;
    public TMP_Text text;

    public string data;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string newData)
    {
        CalculateShowedData(newData);
        CalculatePosition();
    }

    private void CalculateShowedData(string data)
    {
        text.text = data;
    }

    private void Update()
    {
        CalculatePosition();
    }

    private void CalculatePosition()
    {
        var position = Pointer.current.position.ReadValue();
        var normalizedPosition = new Vector2(position.x / Screen.width, position.y / Screen.height);
        var pivot = CalculatePivot(normalizedPosition);
        rectTransform.pivot = pivot;
        transform.position = position;
    }

    private Vector2 CalculatePivot(Vector2 normalizedPosition)
    {
        var pivotTopLeft = new Vector2(-0.05f, 1.05f);
        var pivotTopRight = new Vector2(1.05f, 1.05f);
        var pivotBottomLeft = new Vector2(-0.05f, -0.05f);
        var pivotBottomRight = new Vector2(1.05f, -0.05f);

        if (normalizedPosition.x < 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopLeft;
        }
        else if (normalizedPosition.x > 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopRight;
        }
        else if (normalizedPosition.x <= 0.5f && normalizedPosition.y < 0.5f)
        {
            return pivotBottomLeft;
        }
        else
        {
            return pivotBottomRight;
        }
    }
}
