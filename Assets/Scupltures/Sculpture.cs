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

        //mesh.MarkDynamic();  // Marks mesh as dynamic for frequent updates
        //mesh.RecalculateBounds(); // Ensures the mesh updates correctly

    }


    public void Paint(Vector3 worldHitPoint, float brushRadius, Color paintColor)
    {
        if (mesh == null || vertexColors == null)
        {
            Debug.LogError("⚠️ Mesh ou vertexColors est null dans Paint() sur " + gameObject.name);
            return;
        }

        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            // Convertir les vertices du local space vers le world space
            Vector3 worldVertex = transform.TransformPoint(vertices[i]);

            // Si le sommet est dans le rayon du pinceau, on change sa couleur
            if (Vector3.Distance(worldVertex, worldHitPoint) <= brushRadius)
            {
                // Calcule le facteur de distance (proximité du pinceau)
                float distanceFactor = 1f - (Vector3.Distance(worldVertex, worldHitPoint) / brushRadius);
                distanceFactor = Mathf.Clamp01(distanceFactor); // Assure que le facteur est entre 0 et 1

                // Lerp entre la couleur actuelle et la couleur de peinture
                Color newColor = Color.Lerp(vertexColors[i], paintColor, distanceFactor);

                // Ajuste l'alpha en fonction de la distance (proximité du pinceau)
                newColor.a = Mathf.Lerp(vertexColors[i].a, 1f, distanceFactor); // L'alpha augmente à mesure qu'on se rapproche du centre

                vertexColors[i] = newColor; // Applique la nouvelle couleur
            }
        }

        mesh.colors = vertexColors; // Applique les nouvelles couleurs à la mesh

        //mesh.MarkDynamic();  // Marks mesh as dynamic for frequent updates
        //mesh.RecalculateBounds(); // Ensures the mesh updates correctly
    }

}
