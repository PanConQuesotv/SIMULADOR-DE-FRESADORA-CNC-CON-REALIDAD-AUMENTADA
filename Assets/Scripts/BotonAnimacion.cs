using UnityEngine;

public class BotonAnimacion : MonoBehaviour
{
    public Animator animador;
    public string nombreTrigger = "Reproducir";

    public void ReproducirAnimacion()
    {
        if (animador != null)
        {
            animador.SetTrigger(nombreTrigger);
        }
    }
}
