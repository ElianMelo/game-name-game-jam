using UnityEngine;

public class PlayerVFXController : MonoBehaviour
{
    public GameObject attackSlashVFX;
    
    public void CreateAttackSlashVFX(Vector3 position, Vector3 forward, float scale = 1f)
    {
        GameObject currentVfx = Instantiate(attackSlashVFX, position, Quaternion.identity);
        currentVfx.transform.localScale = new Vector3 (scale, scale, scale);
        currentVfx.transform.forward = forward;
        Destroy(currentVfx, 2f);
    }
}
