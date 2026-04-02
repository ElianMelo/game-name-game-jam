using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
        transform.LookAt(_camera.transform.position);
        transform.Rotate(0, 180f, 0f);
    }

    //void Update()
    //{
    //    transform.LookAt(_camera.transform.position);
    //}
}
