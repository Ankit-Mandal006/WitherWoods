using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetTerrainObstacles : MonoBehaviour
{
    void Start()
    {
        Terrain[] terrains = Terrain.activeTerrains; // Get all terrains in the scene
        GameObject parent = new GameObject("Tree_Obstacles");

        foreach (Terrain terrain in terrains)
        {
            TreeInstance[] trees = terrain.terrainData.treeInstances;
            Vector3 terrainSize = terrain.terrainData.size;
            Debug.Log("Processing Terrain: " + terrain.name + " | Size: " + terrainSize);

            for (int i = 0; i < trees.Length; i++)
            {
                TreeInstance tree = trees[i];
                Vector3 worldPosition = Vector3.Scale(tree.position, terrainSize) + terrain.transform.position;
                Quaternion tempRot = Quaternion.AngleAxis(tree.rotation * Mathf.Rad2Deg, Vector3.up);

                GameObject obs = new GameObject("Obstacle_" + terrain.name + "_" + i);
                obs.transform.SetParent(parent.transform);
                obs.transform.position = worldPosition;
                obs.transform.rotation = tempRot;

                NavMeshObstacle obsElement = obs.AddComponent<NavMeshObstacle>();
                obsElement.carving = true;
                obsElement.carveOnlyStationary = true;

                // Get tree prototype to check for colliders
                GameObject treePrefab = terrain.terrainData.treePrototypes[tree.prototypeIndex].prefab;
                if (treePrefab == null)
                {
                    Debug.LogError("ERROR: Tree prefab is missing for " + terrain.name);
                    continue;
                }

                Collider coll = treePrefab.GetComponent<Collider>();
                if (coll is CapsuleCollider capsule)
                {
                    obsElement.shape = NavMeshObstacleShape.Capsule;
                    obsElement.center = capsule.center;
                    obsElement.radius = 0.1f;
                    obsElement.height = capsule.height;
                }
                else if (coll is BoxCollider box)
                {
                    obsElement.shape = NavMeshObstacleShape.Box;
                    obsElement.center = box.center;
                     obsElement.size = new Vector3(0.01f, 2f, 0.01f);
                }
                else
                {
                    Debug.LogError("ERROR: No valid collider (Capsule/Box) found on tree prefab: " + treePrefab.name);
                    continue;
                }
            }
        }

        Debug.Log("All NavMeshObstacles were successfully added to all terrains!");
    }
}
