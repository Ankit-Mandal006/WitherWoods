using UnityEngine;

public class Compass : MonoBehaviour
{
    public RectTransform compassNeedle;
    public Transform player;
    public Transform portal;

    void Update()
    {
        Vector3 dir = portal.position - player.position;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        angle -= player.eulerAngles.y;
        compassNeedle.localRotation = Quaternion.Euler(0, 0, -angle);
    }
}
