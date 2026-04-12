using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    public ObjectPoolingBase crossbowArrowPool;
    public ObjectPoolingBase catapultBoulderPool;
    public ObjectPoolingBase enemyVFXPool;
    public ObjectPoolingBase enemyStationaryVFXPool;

    public static PoolsManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetCrossbowArrow(float duration)
    {
        return crossbowArrowPool.GetPooledObject(duration);
    }

    public GameObject GetCatapultBoulder(float duration)
    {
        return catapultBoulderPool.GetPooledObject(duration);
    }

    public GameObject GetEnemyVFX(float duration)
    {
        return enemyVFXPool.GetPooledObject(duration);
    }

    public GameObject GetEnemyStationaryVFX(float duration)
    {
        return enemyStationaryVFXPool.GetPooledObject(duration);
    }

}
