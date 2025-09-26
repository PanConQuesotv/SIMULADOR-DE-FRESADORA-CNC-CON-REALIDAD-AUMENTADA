using UnityEngine;

public class FresadoDeformacion : MonoBehaviour
{
    public MeshFilter meshFilter;
    public Transform broca;
    public float radioCorte = 0.1f;
    public float profundidadCorte = 0.02f;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] verticesOriginales;
    private bool meshModificado = false;

    void Start()
    {
        mesh = Instantiate(meshFilter.mesh);
        meshFilter.mesh = mesh;
        vertices = mesh.vertices;
        verticesOriginales = mesh.vertices.Clone() as Vector3[];
    }

    void Update()
    {
        if (meshModificado)
        {
            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            meshModificado = false;
        }
    }

    void FixedUpdate()
    {
        AplicarFresado();
    }

    void AplicarFresado()
    {
        Vector3 posicionBrocaLocal = meshFilter.transform.InverseTransformPoint(broca.position);

        for (int i = 0; i < vertices.Length; i++)
        {
            float distanciaXZ = Vector2.Distance(
                new Vector2(verticesOriginales[i].x, verticesOriginales[i].z),
                new Vector2(posicionBrocaLocal.x, posicionBrocaLocal.z));

            if (distanciaXZ < radioCorte && verticesOriginales[i].y > posicionBrocaLocal.y)
            {
                float factorCorte = Mathf.Clamp01(1 - (distanciaXZ / radioCorte));
                vertices[i].y = Mathf.Max(verticesOriginales[i].y - profundidadCorte * factorCorte, posicionBrocaLocal.y);
                meshModificado = true;
            }
        }
    }
}
