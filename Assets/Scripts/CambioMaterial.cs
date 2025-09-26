using UnityEngine;

public class ModelSwitcher : MonoBehaviour
{
    public GameObject materialModel; // Asigna el cilindro "MATERIAL" en el Inspector
    public GameObject ajedrezModel;  // Asigna el modelo "Ajedrez" en el Inspector
    private bool isMaterialActive = true;

    void Start()
    {
        // Asegurar que solo un modelo esté activo al inicio
        materialModel.SetActive(isMaterialActive);
        ajedrezModel.SetActive(!isMaterialActive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Detectar la tecla "G"
        {
            ToggleModel();
        }
    }

    private void ToggleModel()
    {
        isMaterialActive = !isMaterialActive;
        materialModel.SetActive(isMaterialActive);
        ajedrezModel.SetActive(!isMaterialActive);
    }
}
