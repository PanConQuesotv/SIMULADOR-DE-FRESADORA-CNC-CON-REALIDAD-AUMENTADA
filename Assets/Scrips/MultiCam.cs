using UnityEngine;

public class MultiCam : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;

    void Start()
    {
        cam1.rect = new Rect(0, 0, 0.5f, 1); // Izquierda
        cam2.rect = new Rect(0.5f, 0, 0.5f, 1); // Derecha
    }
}
