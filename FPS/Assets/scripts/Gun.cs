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
    public bool isAutomatic=true;
    public int clipSize = 30;
    public float reloadTime = 1f;
    public int range = 100;
    public float upRecoil = 0f;
    public float sideRecoil = 0f;
    public AudioClip shootingAudio;

    scope _scope;
    Animator animator;
    AudioSource audioSource;

    float nextTimeToShoot = 0;
    bool canShoot=true;
    bool isReloading = false;
    int bullets;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = transform.parent.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        _scope = gameObject.GetComponent<scope>();
        bullets = clipSize;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;
        if (bullets <= 0)
        {
            StartCoroutine(OutOfAmmo());
            return;
        }
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToShoot && (isAutomatic||canShoot))
        {
            canShoot = false;
            nextTimeToShoot = Time.time + 1 / (float)fireRate;
            
            shoot();
        }
        if(Input.GetMouseButtonUp(0) && !isAutomatic)
        {
            canShoot = true;
        }
    }

    void shoot()
    {
        if (shootingAudio != null)
            audioSource.PlayOneShot(shootingAudio);

        if (_scope == null || _scope.IsScoped == 0)
            animator.SetTrigger("shoot");

        transform.parent.transform.parent.GetComponent<MouseLook>().AddRecoil(upRecoil, Random.Range(-sideRecoil, sideRecoil));
        bullets -= 1;
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range, interactable))
        {
            interact target = hit.transform.GetComponent<interact>();
            target.Damage(damage);
            target.Hit(hit.point, Quaternion.LookRotation(hit.normal));
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(force * hit.normal*-1, ForceMode.Impulse);
        }
        
    }

    private void OnEnable()
    {
        nextTimeToShoot = Time.time + 0.5f;
        isReloading = false;
    }

    IEnumerator OutOfAmmo()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        bullets = clipSize;
    }
}
