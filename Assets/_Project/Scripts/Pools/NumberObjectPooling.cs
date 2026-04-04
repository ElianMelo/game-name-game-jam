using UnityEngine;

public class NumberObjectPooling : ObjectPoolingBase
{
    public static NumberObjectPooling SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }

    new public FloatingNumberObject GetPooledObject(float duration)
    {
        GameObject currentObject = base.GetPooledObject(duration);
        return currentObject.GetComponent<FloatingNumberObject>();
    }
}
