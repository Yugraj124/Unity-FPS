using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public GameObject grenadePrefab;
    public float force = 40f;

    GameObject grenade;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
             grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
            rb = grenade.GetComponent<Rigidbody>();
            rb.useGravity = false;
            grenade.transform.parent = gameObject.transform;
        }
        if(Input.GetMouseButtonUp(0))
        {
            grenade.transform.parent = null;
            rb.useGravity = true;
            rb.AddForce(force * Camera.main.transform.forward, ForceMode.VelocityChange);
        }
    }
}
