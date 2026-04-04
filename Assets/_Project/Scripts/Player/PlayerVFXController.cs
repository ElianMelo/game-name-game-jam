using UnityEngine;

public class PlayerVFXController : MonoBehaviour
{
    public GameObject attackSlashVFX;
    public GameObject burstVFX;
    public GameObject areaVFX;
    public GameObject projectileVFX;

    public void CreateAttackSlashVFX(Vector3 position, Vector3 forward, float scale = 1f)
    {
        GameObject currentVfx = Instantiate(attackSlashVFX, position, Quaternion.identity);
        currentVfx.transform.localScale = new Vector3 (scale, scale, scale);
        currentVfx.transform.forward = forward;
        Destroy(currentVfx, 2f);
    }

    public void CreateBurstSkillVFX(Vector3 position, Vector3 forward, float scale = 1f)
    {
        GameObject currentVfx = Instantiate(burstVFX, position, Quaternion.identity);
        currentVfx.transform.localScale = new Vector3(scale, scale, scale);
        currentVfx.transform.forward = forward;
        Destroy(currentVfx, 2f);
    }

    public void CreateAreaSkillVFX(Vector3 position, Vector3 forward, float scale = 1f)
    {
        GameObject currentVfx = Instantiate(areaVFX, position, Quaternion.identity);
        currentVfx.transform.localScale = new Vector3(scale, scale, scale);
        currentVfx.transform.forward = forward;
        Destroy(currentVfx, 2f);
    }

    public void CreateProjectileSkillVFX(Vector3 position, Vector3 forward, float scale = 1f)
    {
        GameObject currentVfx = Instantiate(projectileVFX, position, Quaternion.identity);
        currentVfx.transform.localScale = new Vector3(scale, scale, scale);
        currentVfx.transform.forward = forward;
        Destroy(currentVfx, 2f);
    }
}
