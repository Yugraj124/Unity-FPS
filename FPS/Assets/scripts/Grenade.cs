using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Range(0.1f,3.0f)]
    public float fuseTime = 3f;
    public GameObject explosionPrefab;
    public float effectRadius = 5f;
    public float explosionForce = 500f;

    float timeToExplode;
    bool isLive = false;

    #region Properties

    public float TimeToExplode
    {
        set { timeToExplode = value; }
    }

    public bool IsLive
    {
        get { return isLive; }
        set { isLive = value; }
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        timeToExplode -= Time.deltaTime;
        if(isLive && timeToExplode <= 0)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Collider[] colliders = Physics.OverlapSphere(transform.position, effectRadius);
            foreach (Collider Object in colliders)
            {
                Rigidbody rb = Object.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, effectRadius);
                }
            }

            Destroy(gameObject);
            
            Destroy(explosion, 2f);
        }
    }
}
