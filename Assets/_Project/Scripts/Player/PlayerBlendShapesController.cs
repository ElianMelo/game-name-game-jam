using UnityEngine;


public enum BlendShapeType { 
    Claw,
    Jaw,
    Belly,
    Horn,
    Tail
}

public class PlayerBlendShapesController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    private float ClawBlend = 0; 
    private float JawBlend = 0;
    private float BellyBlend = 0;
    private float HornBlend = 0;
    private float TailBlend = 0;
    public void AddToBlendShape(BlendShapeType type, float amount)
    {
        float currentAmount = 0;
        switch (type)
        {
            case BlendShapeType.Claw: currentAmount = ClawBlend += amount; break;
            case BlendShapeType.Jaw: currentAmount = JawBlend += amount; break;
            case BlendShapeType.Belly: currentAmount = BellyBlend += amount; break;
            case BlendShapeType.Horn: currentAmount = HornBlend += amount; break;
            case BlendShapeType.Tail: currentAmount = TailBlend += amount; break;
        }
        skinnedMeshRenderer.SetBlendShapeWeight((int)type, currentAmount);
    }
}
