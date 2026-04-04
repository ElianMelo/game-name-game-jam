using TMPro;
using UnityEngine;

public class FloatingNumberObject : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _number;
    public TMP_Text Number => _number;
}
