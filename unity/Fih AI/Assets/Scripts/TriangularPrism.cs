using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TriangularPrism : MonoBehaviour
{
    public float width = 1f;
    public float height = 1f;
    public float depth = 1f;


    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        float halfW = width * 0.5f;
        float halfD = depth * 0.5f;

        // Points de base (avant et arrière)
        Vector3 f0 = new Vector3(-halfW, 0, -halfD);
        Vector3 f1 = new Vector3(0, height, -halfD);
        Vector3 f2 = new Vector3(halfW, 0, -halfD);

        Vector3 b0 = new Vector3(-halfW, 0, halfD);
        Vector3 b1 = new Vector3(0, height, halfD);
        Vector3 b2 = new Vector3(halfW, 0, halfD);

        // Sommets dupliqués par face pour des arêtes nettes (flat shading)
        mesh.vertices = new Vector3[]
        {
            f0, f1, f2,             // Face avant (0,1,2)
            b2, b1, b0,             // Face arrière (3,4,5)
            f0, b0, b1, f1,         // Côté gauche (6,7,8,9)
            f1, b1, b2, f2,         // Côté droit (10,11,12,13)
            f2, b2, b0, f0          // Base inférieure (14,15,16,17)
        };

        mesh.triangles = new int[]
        {
            0, 1, 2,                // Avant
            3, 4, 5,                // Arrière
            6, 7, 8, 6, 8, 9,       // Gauche
            10, 11, 12, 10, 12, 13, // Droite
            14, 15, 16, 14, 16, 17  // Bas
        };

        mesh.RecalculateNormals();
    }
}
