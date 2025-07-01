using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public GameObject ObjRef;
    public Transform forward,player;
    bool canGrab=false;

    void Start()
    {
        
    }
    void Update()
    {
        if(canGrab)
        {
            Vector3 direction=(forward.position-player.position).normalized;
            ObjRef.transform.position=player.position+(direction*1.1f);
            //ObjRef.transform.LookAt(player.position);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
                canGrab=true;
                Debug.Log("Working");
        }
    }
}
