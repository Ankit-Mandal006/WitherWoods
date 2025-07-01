using UnityEngine;

public class Thundering : MonoBehaviour
{
    public AudioSource[] audioSources;
    public Transform player;
    public Transform portal;
    public GameObject spotlight,particles;
    Animator anim,anim_particles;
    public bool thunder=false,canThunder=true;
    public float T = 30f;
    void Start()
    {
        anim=spotlight.GetComponent<Animator>();
        anim_particles=particles.GetComponent<Animator>();
    }

    void PositionSpotlight()
    {
        if (player == null || portal == null || spotlight == null) return;

        // Calculate direction from player to portal
        Vector3 direction = (portal.position - player.position).normalized;

        // Position the spotlight **18 units from the player in that direction**
        spotlight.transform.position = player.position + (direction * 18f);
        particles.transform.position = player.position + (direction * 18f);

        // Make the spotlight **face the player**
        spotlight.transform.LookAt(player.position);
        particles.transform.LookAt(player.position);

        // Ensure the spotlight is active
        spotlight.SetActive(true);
    }

    void Update()
    {
        if (portal != null)
        {
            if (thunder && canThunder) // Press 'L' to reposition the spotlight
            {
                T = 30f;
                thunder = false;
                PositionSpotlight();
                anim.SetTrigger("Thunder");
                int index = Random.Range(0, audioSources.Length);
                AudioSource source = audioSources[index];
                source.Play();
                anim_particles.SetTrigger("excited");
            }
            float d = Vector3.Distance(portal.position, player.position);
            if (portal != null && d > 20)
                T -= Time.deltaTime;
            else
                T = 30f;
            if (T <= 0f)
                thunder = true;
        }
    }
}
