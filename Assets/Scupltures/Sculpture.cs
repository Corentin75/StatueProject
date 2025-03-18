using UnityEngine;

public class Sculpture : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh mesh;
    private Color[] vertexColors;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("⚠️ MeshFilter est introuvable sur l'objet : " + gameObject.name);
            return;
        }

        mesh = meshFilter.mesh;
        if (mesh == null)
        {
            Debug.LogError("⚠️ Mesh est null sur l'objet : " + gameObject.name);
            return;
        }

        // Initialisation des couleurs des sommets avec alpha à 0 pour la transparence
        vertexColors = new Color[mesh.vertexCount];

        for (int i = 0; i < vertexColors.Length; i++)
        {
            vertexColors[i] = new Color(1f, 1f, 1f, 0f); // Statue invisible (alpha 0)
        }

        mesh.colors = vertexColors; // Appliquer les couleurs initiales
    }


    public void Paint(Vector3 worldHitPoint, float brushRadius, Color paintColor)
    {
        if (mesh == null || vertexColors == null)
        {
            Debug.LogError("⚠️ Mesh ou vertexColors est null dans Paint() sur " + gameObject.name);
            return;
        }
        Debug.Log("Hit");
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            // Convertir les vertices du local space vers le world space
            Vector3 worldVertex = transform.TransformPoint(vertices[i]);

            // Si le sommet est dans le rayon du pinceau, on change sa couleur
            if (Vector3.Distance(worldVertex, worldHitPoint) <= brushRadius)
            {
                float distanceFactor = 1f - (Vector3.Distance(worldVertex, worldHitPoint) / brushRadius);
                vertexColors[i] = Color.Lerp(vertexColors[i], paintColor, distanceFactor);
            }
        }

        mesh.colors = vertexColors; // Appliquer les nouvelles couleurs
    }
}
