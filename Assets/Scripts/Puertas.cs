using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject puertaIzquierda;
    private bool isVisible = true;

    void Update()
    {
        // Teclado (letra P)
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePuerta();
        }
    }

    // Llamado desde botón o mando
    public void TogglePuerta()
    {
        isVisible = !isVisible;
        SetVisibility(isVisible);
    }

    private void SetVisibility(bool visible)
    {
        if (puertaIzquierda != null)
        {
            Renderer renderer = puertaIzquierda.GetComponent<Renderer>();
            if (renderer != null)
                renderer.enabled = visible;

            Collider collider = puertaIzquierda.GetComponent<Collider>();
            if (collider != null)
                collider.enabled = visible;
        }
    }
}
