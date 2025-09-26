using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class GCodeInterpreter : MonoBehaviour
{
    public InputField inputField;

    public Transform cama;  // Rotación en Z
    public Transform disco; // Rotación en Y y movimiento en Z
    public Transform brazo; // Movimiento en X y Y

    private bool isProcessing = false;

    // 🔹 Guardamos la posición y rotación inicial
    private Vector3 initialBrazoPos;
    private Vector3 initialDiscoPos;
    private Quaternion initialCamaRot;
    private Quaternion initialDiscoRot;

    void Start()
    {
        inputField.gameObject.SetActive(false);
        inputField.onEndEdit.AddListener(OnEnterPressed);

        // Guardamos los valores iniciales
        initialBrazoPos = brazo.localPosition;
        initialDiscoPos = disco.localPosition;
        initialCamaRot = cama.rotation;
        initialDiscoRot = disco.localRotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            inputField.gameObject.SetActive(!inputField.gameObject.activeSelf);
            if (inputField.gameObject.activeSelf)
            {
                inputField.text = "";
                inputField.Select();
                inputField.ActivateInputField();
            }
        }

        // 🔹 Si se presiona "O", volver al estado inicial
        if (Input.GetKeyDown(KeyCode.O) && !isProcessing)
        {
            StartCoroutine(ResetToInitialState());
        }
    }

    void OnEnterPressed(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isProcessing)
        {
            StartCoroutine(ExecuteCommands(text));
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }

    IEnumerator ExecuteCommands(string gcode)
    {
        isProcessing = true;
        string[] commands = gcode.Split('\n');

        foreach (string command in commands)
        {
            if (command.StartsWith("G01"))
            {
                ParseG01(command);
                yield return new WaitForSeconds(1f);
            }
        }

        isProcessing = false;
    }

    void ParseG01(string command)
    {
        float? targetX = null, targetY = null, targetZ = null;
        float? targetRotZ = null, targetRotY = null;

        Match matchX = Regex.Match(command, @"X([-+]?\d*\.?\d+)");
        Match matchY = Regex.Match(command, @"Y([-+]?\d*\.?\d+)");
        Match matchZ = Regex.Match(command, @"Z([-+]?\d*\.?\d+)");
        Match matchRotZ = Regex.Match(command, @"RZ([-+]?\d*\.?\d+)");
        Match matchRotY = Regex.Match(command, @"RY([-+]?\d*\.?\d+)");

        if (matchX.Success) targetX = float.Parse(matchX.Groups[1].Value);
        if (matchY.Success) targetY = float.Parse(matchY.Groups[1].Value);
        if (matchZ.Success) targetZ = float.Parse(matchZ.Groups[1].Value);
        if (matchRotZ.Success) targetRotZ = float.Parse(matchRotZ.Groups[1].Value);
        if (matchRotY.Success) targetRotY = float.Parse(matchRotY.Groups[1].Value);

        StartCoroutine(MoveToPosition(targetX, targetY, targetZ, targetRotZ, targetRotY));
    }

    IEnumerator MoveToPosition(float? x, float? y, float? z, float? rotZ, float? rotY)
    {
        Vector3 startPosBrazo = brazo.localPosition;
        Vector3 targetPosBrazo = new Vector3(
            x.HasValue ? Mathf.Clamp(x.Value, -1.3f, 1.3f) : brazo.localPosition.x,
            y.HasValue ? Mathf.Clamp(y.Value, 2.097f, 3.143f) : brazo.localPosition.y,
            brazo.localPosition.z
        );

        Vector3 startPosDisco = disco.localPosition;
        Vector3 targetPosDisco = new Vector3(
            disco.localPosition.x,
            disco.localPosition.y,
            z.HasValue ? Mathf.Clamp(z.Value, -0.4f, 0.4f) : disco.localPosition.z
        );

        Quaternion startRotCama = cama.rotation;
        Quaternion targetRotCama = Quaternion.Euler(
            cama.rotation.eulerAngles.x,
            cama.rotation.eulerAngles.y,
            rotZ.HasValue ? Mathf.Clamp(rotZ.Value, -90f, 90f) : cama.rotation.eulerAngles.z
        );

        Quaternion startRotDisco = disco.localRotation;
        Quaternion targetRotDisco = Quaternion.Euler(
            disco.localRotation.eulerAngles.x,
            rotY.HasValue ? rotY.Value % 360f : disco.localRotation.eulerAngles.y,
            disco.localRotation.eulerAngles.z
        );

        float duration = 1.0f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            brazo.localPosition = Vector3.Lerp(startPosBrazo, targetPosBrazo, elapsedTime / duration);
            disco.localPosition = Vector3.Lerp(startPosDisco, targetPosDisco, elapsedTime / duration);
            cama.rotation = Quaternion.Lerp(startRotCama, targetRotCama, elapsedTime / duration);
            disco.localRotation = Quaternion.Lerp(startRotDisco, targetRotDisco, elapsedTime / duration);

            disco.rotation = cama.rotation * Quaternion.Euler(0, disco.localRotation.eulerAngles.y, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        brazo.localPosition = targetPosBrazo;
        disco.localPosition = targetPosDisco;
        cama.rotation = targetRotCama;
        disco.localRotation = targetRotDisco;

        disco.rotation = cama.rotation * Quaternion.Euler(0, disco.localRotation.eulerAngles.y, 0);
    }

    // 🔹 Nueva función para regresar al estado inicial suavemente
    IEnumerator ResetToInitialState()
    {
        isProcessing = true;
        float duration = 1.5f; // Más tiempo para un movimiento más natural
        float elapsedTime = 0;

        Vector3 startBrazoPos = brazo.localPosition;
        Vector3 startDiscoPos = disco.localPosition;
        Quaternion startCamaRot = cama.rotation;
        Quaternion startDiscoRot = disco.localRotation;

        while (elapsedTime < duration)
        {
            brazo.localPosition = Vector3.Lerp(startBrazoPos, initialBrazoPos, elapsedTime / duration);
            disco.localPosition = Vector3.Lerp(startDiscoPos, initialDiscoPos, elapsedTime / duration);
            cama.rotation = Quaternion.Lerp(startCamaRot, initialCamaRot, elapsedTime / duration);
            disco.localRotation = Quaternion.Lerp(startDiscoRot, initialDiscoRot, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        brazo.localPosition = initialBrazoPos;
        disco.localPosition = initialDiscoPos;
        cama.rotation = initialCamaRot;
        disco.localRotation = initialDiscoRot;

        isProcessing = false;
    }
}