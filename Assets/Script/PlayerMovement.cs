using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject text;
    GameObject shine;
    public WaveContoller wc;
    public GameObject spotLight;
    public FlashLight fl;
    public CharacterController controller;
    public float speed=12f,gravity=-9.81f;
    public Transform groundCheck;
    public float groundDistance=0.4f;
    public LayerMask groundMask;
    public bool canHide,isHieding=false,t=false;
    PlayerInventory pi;
    Animator anim;

    Vector3 velocity;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        pi=this.GetComponent<PlayerInventory>();
        anim =this.GetComponent<Animator>();
        controller=this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);
        if(isGrounded&&velocity.y<0)
            velocity.y=0f;

        float x=Input.GetAxis("Horizontal");
        float z=Input.GetAxis("Vertical");

        Vector3 move=transform.right*x+transform.forward*z;
        controller.Move(move*speed*Time.deltaTime);
        velocity.y+=gravity*Time.deltaTime;
        controller.Move(velocity*Time.deltaTime);

        if (Input.GetButtonDown("Interact") && canHide)
        {
            anim.SetBool("canHide", true);
            anim.SetBool("holdLight", false);
            spotLight.SetActive(false);
            fl.off = true;
            fl.on = false;
            isHieding = true;
            text.SetActive(false);
        }
        if(!canHide)
        {
            anim.SetBool("canHide",false);
            isHieding=false;
        }

    }
    void LateUpdate()
    {
        if(pi!=null)
        if (pi.hasCrystal || pi.hasCrystal1 || pi.hasSkull)
            text.SetActive(t);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bush"))
        {
            canHide = true;
            shine = other.transform.Find("Ind")?.gameObject;
            shine.SetActive(false);
            text.SetActive(false);
            t = true;
        }
        if (other.CompareTag("Finish"))
            anim.SetTrigger("SpriteJumpScare");
        if (other.CompareTag("door"))
            wc.count++;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Bush"))
        {
            canHide = false;
            shine.SetActive(true);
            t = false;
            text.SetActive(false);
        }
    }
}
