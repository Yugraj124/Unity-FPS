using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem muzzleFlash;
    [Range(0.1f,20)]
    public float fireRate = 15;
    public int damage = 5;
    public Transform cam;
    public float force = 15f;
    public LayerMask interactable;
    public bool isBurst=true;
    public int range = 100;

    float nextTimeToShoot = 0;
    bool canShoot=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButton(0)&&Time.time>=nextTimeToShoot&&(isBurst||canShoot))
        {
            canShoot = false;
            nextTimeToShoot = Time.time + 1 / (float)fireRate;
            shoot();
            
        }
        if(Input.GetMouseButtonUp(0)&&!isBurst)
        {
            canShoot = true;
        }
    }
    void shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range, interactable))
        {
            interact target=hit.transform.GetComponent<interact>();
            target.Damage(damage);
            target.Hit(hit.point, Quaternion.LookRotation(hit.normal));
            Rigidbody rb = hit.rigidbody;
            rb.AddForce(force * hit.normal*-1,ForceMode.Impulse);
        }

    }
}
