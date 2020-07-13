using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public GameObject grenadePrefab;
    public float force = 40f;
    public float cooldown = 0.2f;

    GameObject grenade;
    Grenade _grenadeScript;
    Rigidbody rb;

    float timeElapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        // switching to gun after grenade thrown
        if (grenade == null || grenade.transform.parent == null)
        {
            transform.parent.GetComponent<weaponChange>().SelectedWeapon = 0;
            transform.parent.GetComponent<weaponChange>().selectWeapon();
        }

        // input to activate grenade
        if (timeElapsed > cooldown && Input.GetMouseButtonDown(0))
        {
            _grenadeScript.TimeToExplode = _grenadeScript.fuseTime;
            _grenadeScript.IsLive = true;
            grenade.GetComponent<Animator>().SetBool("isLive", true);
        }

        // input to throw grenade
        if (grenade != null && Input.GetMouseButtonUp(0) && _grenadeScript.IsLive)
        {
            grenade.transform.parent = null;
            Destroy(grenade.GetComponent<Animator>());
            rb = grenade.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(force * Camera.main.transform.forward, ForceMode.Impulse);
        }
    }

    private void OnEnable()
    {
        grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Destroy(grenade.GetComponent<Rigidbody>());
        grenade.transform.parent = gameObject.transform;
        _grenadeScript = grenade.GetComponent<Grenade>();
        timeElapsed = 0f;
    }

    private void OnDisable()
    {
        // cancelling the grenade if weapon is switched
        if (grenade != null && grenade.transform.parent != null)
        {
            Destroy(grenade);
        }
    }
}
