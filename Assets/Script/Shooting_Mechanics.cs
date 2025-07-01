using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shooting_Mechanics : MonoBehaviour
{
    public Animator anim;
    public GameObject projectilePrefab;
    public AudioSource audioSources;
    public Transform firePoint;
    public float shootForce = 20f;
    public float ammo = 30f,maxAmmo=30f;
    public Image image;

    void Update()
    {
        image.fillAmount = ammo / 30f;
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (Collider hit in hits)
    {
        if (ammo >= maxAmmo) break;

        if (hit.CompareTag("zone1"))
            ammo += 0.1f * Time.deltaTime;

        else if (hit.CompareTag("zone2"))
            ammo += 2 * Time.deltaTime;
    }

    ammo = Mathf.Min(ammo, maxAmmo); 
        if (Input.GetButtonDown("Fire1"))
        {
            if (audioSources!= null)
            {
                audioSources.pitch = Random.Range(.5f, 1.5f);
                audioSources.Play();
            }
            if(anim!=null)
                anim.SetTrigger("Shoot");
            StartCoroutine(Shoot());

        }
    }
    IEnumerator Shoot()
    {


        yield return new WaitForSeconds(.5f);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100f); // default distance

        Vector3 direction = (targetPoint - firePoint.position).normalized;
        if (ammo >= 10)
        {
            ammo -= 10;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = direction * shootForce;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("zone1"))
        {
            if (ammo < 30)
                ammo += 1 * Time.deltaTime;
        }
        if (other.CompareTag("zone2"))
        {
            if(ammo<30)
                ammo += 3 * Time.deltaTime;
        }
    }
}
