using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    [System.Serializable]
    public class CustomMaterial
    {
        public string nombre;
        public Color color;
        public float metallic;
        public float glossiness;
    }

    public Renderer materialRenderer;
    public CustomMaterial[] materiales;

    private int currentIndex = 0;

    void Start()
    {
        if (materialRenderer == null)
        {
            Debug.LogError("No se asignó un Renderer al script MaterialChange.");
            return;
        }

        // Si el renderer no tiene un material válido o no tiene shader, creamos uno nuevo
        if (materialRenderer.sharedMaterial == null || materialRenderer.sharedMaterial.shader == null)
        {
            Debug.LogWarning("Material no encontrado o inválido. Creando material temporal...");
            materialRenderer.material = new Material(Shader.Find("Standard"));
        }

        AplicarMaterialActual();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) CambiarMaterialSiguiente();
        if (Input.GetKeyDown(KeyCode.N)) CambiarMaterialAnterior();
    }

    public void CambiarMaterialSiguiente()
    {
        currentIndex = (currentIndex + 1) % materiales.Length;
        AplicarMaterialActual();
    }

    public void CambiarMaterialAnterior()
    {
        currentIndex = (currentIndex - 1 + materiales.Length) % materiales.Length;
        AplicarMaterialActual();
    }

    private void AplicarMaterialActual()
    {
        if (materialRenderer != null && materiales.Length > 0)
        {
            var mat = materiales[currentIndex];

            // Aseguramos que el material tenga el shader Standard
            if (materialRenderer.material.shader.name != "Standard")
            {
                materialRenderer.material.shader = Shader.Find("Standard");
            }

            materialRenderer.material.color = mat.color;
            materialRenderer.material.SetFloat("_Metallic", mat.metallic);
            materialRenderer.material.SetFloat("_Glossiness", mat.glossiness);

            Debug.Log($"Material aplicado: {mat.nombre}");
        }
    }


}