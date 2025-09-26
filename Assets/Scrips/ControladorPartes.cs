using UnityEngine;
using UnityEngine.EventSystems;

public class ControlPartes : MonoBehaviour
{
    [Header("Referencias a las partes")]
    public Transform ejeZ;
    public Transform mesaGiratoria;
    public Transform disco;

    [Header("Configuración de movimiento")]
    public float velocidadMovimiento = 1f;   // velocidad ejeZ
    public float velocidadRotacion = 90f;    // grados por segundo

    // Flags para saber qué botón está presionado
    private bool moverArriba, moverAbajo;
    private bool rotarMesaDerecha, rotarMesaIzquierda;
    private bool rotarDiscoDerecha, rotarDiscoIzquierda;

    void Update()
    {
        // === EjeZ (subir/bajar en Z local más lento) ===
        if (moverArriba) ejeZ.Translate(Vector3.forward * 0.02f * velocidadMovimiento * Time.deltaTime, Space.Self);
        if (moverAbajo) ejeZ.Translate(Vector3.back * 0.02f * velocidadMovimiento * Time.deltaTime, Space.Self);

        // === MesaGiratoria (rotar sobre Z local más lento) ===
        if (rotarMesaDerecha) mesaGiratoria.Rotate(Vector3.forward * 0.5f * velocidadRotacion * Time.deltaTime, Space.Self);
        if (rotarMesaIzquierda) mesaGiratoria.Rotate(Vector3.back * 0.5f * velocidadRotacion * Time.deltaTime, Space.Self);

        // === Disco (rotar ahora sobre Y local en vez de X) ===
        if (rotarDiscoDerecha) disco.Rotate(Vector3.up * 0.5f * velocidadRotacion * Time.deltaTime, Space.Self);
        if (rotarDiscoIzquierda) disco.Rotate(Vector3.down * 0.5f * velocidadRotacion * Time.deltaTime, Space.Self);
    }




    // === Eventos para los botones ===
    public void EjeArriba_Presionar() { moverArriba = true; }
    public void EjeArriba_Soltar() { moverArriba = false; }

    public void EjeAbajo_Presionar() { moverAbajo = true; }
    public void EjeAbajo_Soltar() { moverAbajo = false; }

    public void MesaDerecha_Presionar() { rotarMesaDerecha = true; }
    public void MesaDerecha_Soltar() { rotarMesaDerecha = false; }

    public void MesaIzquierda_Presionar() { rotarMesaIzquierda = true; }
    public void MesaIzquierda_Soltar() { rotarMesaIzquierda = false; }

    public void DiscoDerecha_Presionar() { rotarDiscoDerecha = true; }
    public void DiscoDerecha_Soltar() { rotarDiscoDerecha = false; }

    public void DiscoIzquierda_Presionar() { rotarDiscoIzquierda = true; }
    public void DiscoIzquierda_Soltar() { rotarDiscoIzquierda = false; }
}
