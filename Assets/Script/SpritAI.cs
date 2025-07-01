using UnityEngine;
using UnityEngine.AI;

public class SpritAI : MonoBehaviour
{
    public Transform player;
    public WaveContoller wc;
    private NavMeshAgent agent;
    public Animator anim;
    private bool canMove = true;
    public MouseLook ml;
    public GameObject FlashLight;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (!agent) Debug.LogError("NavMeshAgent missing on " + gameObject.name);
        if (!anim) Debug.LogError("Animator missing on " + gameObject.name);
    }

    void Update()
    {
        if (!IsPlayerLookingAtMe()||!FlashLight.activeSelf) // Move only if player is NOT looking
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

        if (canMove)
{
    agent.isStopped = false;
    agent.SetDestination(player.position);
    anim.speed = 1;
    
    // Face the player before moving
    Vector3 lookDirection = (player.position - transform.position).normalized;
    lookDirection.y = 0;
    transform.rotation = Quaternion.LookRotation(lookDirection);
}
else
{
    agent.isStopped = true;
    agent.ResetPath();  // ❌ Clear destination
    agent.velocity = Vector3.zero; // 🔴 Stop movement immediately
    anim.speed = 0; // Stop animation
}
float distance=Vector3.Distance(player.transform.position,this.transform.position);
        if (distance < 2f)
        {
            wc.count = 4;
        }
    }

    bool IsPlayerLookingAtMe()
    {
        Vector3 directionToAI = (transform.position - player.position).normalized;
        Vector3 playerForward = player.forward.normalized;

        float dot = Vector3.Dot(playerForward, directionToAI);

        return dot > 0.5f && Mathf.Abs(directionToAI.y - playerForward.y) < 0.5f;
    }
}