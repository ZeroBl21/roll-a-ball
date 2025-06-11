using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class DiamondMesh : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Diamond";

        // Vértices del octaedro
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 1, 0),    // 0 - punta superior
            new Vector3(-0.5f, 0, 0),// 1 - izquierda
            new Vector3(0, 0, 0.5f), // 2 - frente
            new Vector3(0.5f, 0, 0), // 3 - derecha
            new Vector3(0, 0, -0.5f),// 4 - atrás
            new Vector3(0, -1, 0),   // 5 - punta inferior
        };

        // Triángulos (caras)
        int[] triangles = new int[]
        {
            // Superior
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 1,

            // Inferior
            5, 2, 1,
            5, 3, 2,
            5, 4, 3,
            5, 1, 4
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // Asignar a componentes
        GetComponent<MeshFilter>().mesh = mesh;
        // var mat = new Material(Shader.Find("Standard"));
        // mat.color = Color.cyan; // Puedes cambiarlo
        // mat.SetFloat("_Glossiness", 1f); // Más brillante
        // GetComponent<MeshRenderer>().material = mat;
    }
}
