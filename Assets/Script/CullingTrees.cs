using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingTrees : MonoBehaviour
{
    public GameObject[] trees;
    public Transform playerPos; // Fixed missing semicolon

    void Start()
    {
        InvokeRepeating(nameof(UpdateCulling), 0f, 1f);
    }

    void UpdateCulling()
    {
        foreach (GameObject tree in trees)
        {
            float distance = Vector3.Distance(playerPos.position, tree.transform.position);
            tree.SetActive(distance <= 15f); // Deactivate trees beyond 15f
        }
    }
}
