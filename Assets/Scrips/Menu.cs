using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void CargarEscena1()
    {
        SceneManager.LoadScene("FresadoraCnc");
    }

    public void CargarEscena2()
    {
        SceneManager.LoadScene("Prueba");
    }

    public void AbrirSettings()
    {
        // Puedes hacer visible un panel de ajustes o cargar otra escena
        Debug.Log("Abrir ajustes (aquí puedes mostrar un panel)");
    }

    public void SalirAplicacion()
    {
#if UNITY_ANDROID
        Application.Quit();
#else
        Debug.Log("Salir solo funciona en dispositivo físico.");
#endif
    }
}

