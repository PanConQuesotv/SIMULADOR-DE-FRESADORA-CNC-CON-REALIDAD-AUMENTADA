using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Renderer materialRenderer; // Asigna el cilindro en el Inspector
    private int currentColorIndex = 0;

    private Color[] colors = new Color[]
    {
        new Color(0.75f, 0.75f, 0.75f, 1f), // Gris plateado (brillante)
        Color.black,                       // Negro opaco
        new Color(1.0f, 0.84f, 0.0f, 1f)    // Oro brillante
    };

    void Update()
    {
        // Teclado
        if (Input.GetKeyDown(KeyCode.M))
        {
            CambiarMaterial();
        }
    }

    // Llamado también desde botón UI
    public void CambiarMaterial()
    {
        if (materialRenderer != null)
        {
            currentColorIndex = (currentColorIndex + 1) % colors.Length;
            materialRenderer.material.color = colors[currentColorIndex];

            // Ajustar brillo/metallic
            if (currentColorIndex == 0 || currentColorIndex == 2)
            {
                materialRenderer.material.SetFloat("_Metallic", 1f);
                materialRenderer.material.SetFloat("_Glossiness", 1f);
            }
            else
            {
                materialRenderer.material.SetFloat("_Metallic", 0f);
                materialRenderer.material.SetFloat("_Glossiness", 0f);
            }
        }
    }
}
