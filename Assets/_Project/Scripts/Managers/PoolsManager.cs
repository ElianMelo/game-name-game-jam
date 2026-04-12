using UnityEngine;

public class PoolsManager : MonoBehaviour
{
    public ObjectPoolingBase crossbowArrowPool;
    public ObjectPoolingBase catapultBoulderPool;

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
    
}
