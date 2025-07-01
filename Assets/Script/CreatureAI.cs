using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CreatureAI : MonoBehaviour
{
    public FootSteps_AudioManager fam;
    int death = 0;
    public StunnableEnemy se;
    public PlayerDeath pd;
    public Transform point, spawnPoint;
    public float rayLength = 2f;
    public GameObject player, location, spotLight, cam;
    public NavMeshAgent agent;
    public LayerMask treeLayer;
    public Animator creep1, creep2;
    public MouseLook ml;
    private PlayerMovement mp;
    private bool canWatch = false;
    public int c = 0, range;
    public float avoidanceStrength = 1.5f;
    public NextLevel nl;
    
    void Start()
    {
        //location = Instantiate(location, this.transform.position, this.transform.rotation);
        location=GameObject.FindWithTag("Location");
        player = GameObject.FindWithTag("Player");
        cam = GameObject.FindWithTag("MainCamera");
        ml = cam.GetComponent<MouseLook>();
        se = GetComponent<StunnableEnemy>();
        mp = player.GetComponent<PlayerMovement>();
        agent = this.GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        InvokeRepeating(nameof(UpdateLocation), 0f, 3f);
        creep1.SetBool("walk", true);
        creep2.SetBool("walk", true);
    }

    void StartNavMesh()
    {
        agent.isStopped = false;
    }

    void UpdateLocation()
    {
        if (!mp.isHieding)
            location.transform.position = player.transform.position;
    }

    void Update()
    {
        
        if (se==null||!se.isStunned)
        {
            
            death = 0;
            creep1.SetBool("Death", false);
            creep2.SetBool("Death", false);
            if (spotLight.activeSelf)
            {
                range = 20;
                mp.isHieding = false;
            }
            else
            {
                range = 10;
            }

            float d = Vector3.Distance(location.transform.position, transform.position);
            if (!mp.isHieding)
                c = 0;

            if (!canWatch && mp.isHieding && c == 0)
            {
                float radius = Random.Range(4f, 10f);
                Vector2 randomPos = Random.insideUnitCircle.normalized * radius;
                location.transform.position = transform.position + new Vector3(randomPos.x, 0, randomPos.y);
                c++;
            }

            if (d > 2f)
            {
                agent.SetDestination(location.transform.position);
            }
            else
            {
                c = 0;
                agent.speed = 3f;
                agent.isStopped = true;
                creep1.SetTrigger("sniff");
                creep2.SetTrigger("sniff");
                creep1.SetBool("silent_walk", true);
                creep1.SetBool("walk", false);
                creep2.SetBool("silent_walk", true);
                creep2.SetBool("walk", false);
                Invoke(nameof(StartNavMesh), 5f);
                Vector2 randomPos = Random.insideUnitCircle * 4f;
                location.transform.position += new Vector3(randomPos.x, 0, randomPos.y);
            }

            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= 2f)
            {
                player.GetComponent<Animator>().SetTrigger("CreatureJumpScare");
                cam.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                spotLight.SetActive(false);
                player.GetComponent<Animator>().SetBool("holdLight", false);
                player.GetComponent<PlayerMovement>().enabled = false;
                ml.enabled = false;
                this.gameObject.SetActive(false);
                Invoke(nameof(Release), 2f);
                if(nl!=null)
                    Invoke("Trigger", 2f);
            }
            if (fam != null)
            {
                if (distance <= 70f)
                {
                    fam.enabled = true;
                    //this.gameObject.GetComponent<FootSteps_AudioManager>().enabled = true;
                }
                else
                {
                    fam.enabled = false;
                    //this.gameObject.GetComponent<FootSteps_AudioManager>().enabled = false;
                }
            }
            if (distance > 2f && distance < range && !mp.isHieding)
                {
                    location.transform.position = player.transform.position;
                    agent.speed = 6.3f;
                    creep1.SetBool("silent_walk", false);
                    creep1.SetBool("walk", true);
                    creep2.SetBool("silent_walk", false);
                    creep2.SetBool("walk", true);
                    if (!mp.isHieding)
                        canWatch = true;
                }

            if (distance > range)
                canWatch = false;

            if (canWatch)
            {
                location.transform.position = player.transform.position;
                mp.isHieding = false;
            }

            
        }
        else if (se != null && se.isStunned)
        {
            creep1.SetBool("Death", true);
            creep2.SetBool("Death", true);
            death = 1;
        }
    }
    void Trigger()
    {
        nl.ChangeScene("LastLevel");
    }
    void Release()
    {
        player.GetComponent<Animator>().SetTrigger("ideal");

        if (agent != null)
            agent.enabled = false;

        if (point != null)
            transform.position = point.position;

        if (agent != null)
            agent.enabled = true;

        gameObject.SetActive(true);

        if(pd!=null)
        pd.count++;
        player.GetComponent<PlayerMovement>().enabled = true;
        ml.enabled = true;

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;

        if(spawnPoint!=null)
        player.transform.position = spawnPoint.transform.position;
        StartCoroutine(EnableControllerDelayed());
    }

    IEnumerator EnableControllerDelayed()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = true;
    }
}
