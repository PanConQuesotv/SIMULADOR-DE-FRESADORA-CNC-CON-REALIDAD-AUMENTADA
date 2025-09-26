using UnityEngine;

public class RotatingPiece : MonoBehaviour
{
    public Transform[] objetosGiratorios; // Array de objetos para girar
    private float rotationSpeed = 0f;
    private float maxSpeed = 100f;
    private float acceleration = 150f;
    private float deceleration = 80f;

    public bool girando = false;

    void Update()
    {
        // Teclado
        if (Input.GetKey(KeyCode.Space))
            girando = true;
        if (Input.GetKeyUp(KeyCode.Space))
            girando = false;

        // Control de velocidad
        if (girando)
            rotationSpeed = Mathf.MoveTowards(rotationSpeed, maxSpeed, acceleration * Time.deltaTime);
        else
            rotationSpeed = Mathf.MoveTowards(rotationSpeed, 0f, deceleration * Time.deltaTime);

        // Aplicar rotación a todos
        foreach (Transform obj in objetosGiratorios)
        {
            if (obj != null)
                obj.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    // Para botones
    public void IniciarGiro() => girando = true;
    public void DetenerGiro() => girando = false;
}
