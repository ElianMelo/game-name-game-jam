using UnityEngine;

public enum VFXList
{
    Slash,
    Burst,
    Area,
    Projectile
}

public class PlayerVFXController : MonoBehaviour
{
    public GameObject attackSlashVFX;
    public GameObject burstVFX;
    public GameObject areaVFX;
    public GameObject projectileVFX;

    private GameObject GetPrefabBaseOnVFXList(VFXList vfxList)
    {
        switch (vfxList)
        {
            case VFXList.Slash: return attackSlashVFX;
            case VFXList.Burst: return burstVFX;
            case VFXList.Area: return areaVFX;
            case VFXList.Projectile: return projectileVFX;
        }
        return null;
    }

    public void CreateAttackVFX(Vector3 position, Vector3 forward, float scale, float damage, VFXList vfxList)
    {
        GameObject currentVfx = Instantiate(GetPrefabBaseOnVFXList(vfxList), position, Quaternion.identity);
        currentVfx.GetComponent<PlayerVFXDamageSource>().SetupDamage(damage);
        currentVfx.transform.localScale = new Vector3(scale, scale, scale);
        currentVfx.transform.forward = forward;
        Destroy(currentVfx, 1f);
    }
}
