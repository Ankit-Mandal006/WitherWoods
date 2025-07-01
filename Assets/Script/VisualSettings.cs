using UnityEngine;

[ExecuteAlways]
public class VisualSettings : MonoBehaviour
{
    [Range(0, 250)]
    public int grassDrawDistance = 100; // How far grass is visible

    void Start()
    {
        ApplySettings();
    }

    void OnValidate()
    {
        ApplySettings();
    }

    void ApplySettings()
    {
        Terrain[] terrains = FindObjectsOfType<Terrain>();

        foreach (Terrain terrain in terrains)
        {
            terrain.detailObjectDistance = grassDrawDistance;
            terrain.Flush();
        }
    }
}
