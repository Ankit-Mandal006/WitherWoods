using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class MultiTerrainDetailCleaner : MonoBehaviour
{
    public Terrain[] terrains; // Drag multiple terrain objects here
    public float heightThreshold = 0.8f; // World height threshold

    [ContextMenu("Clean All Terrains Below Threshold")]
    void CleanAllTerrains()
    {
        if (terrains == null || terrains.Length == 0)
        {
            Debug.LogError("⛔ No terrains assigned.");
            return;
        }

        foreach (Terrain terrain in terrains)
        {
            if (terrain == null) continue;

            TerrainData data = terrain.terrainData;
            Vector3 terrainPos = terrain.transform.position;
            int detailWidth = data.detailWidth;
            int detailHeight = data.detailHeight;

            for (int layer = 0; layer < data.detailPrototypes.Length; layer++)
            {
                int[,] details = data.GetDetailLayer(0, 0, detailWidth, detailHeight, layer);

                for (int y = 0; y < detailHeight; y++)
                {
                    for (int x = 0; x < detailWidth; x++)
                    {
                        float normX = (float)x / (detailWidth - 1);
                        float normY = (float)y / (detailHeight - 1);

                        float height = data.GetInterpolatedHeight(normX, normY) + terrainPos.y;

                        if (height < heightThreshold)
                            details[y, x] = 0;
                    }
                }

                data.SetDetailLayer(0, 0, layer, details);
            }

            Debug.Log($"✅ Cleaned: {terrain.name}");
        }

        Debug.Log("🎯 All terrain grass below threshold removed!");
    }
}
