using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interact : MonoBehaviour
{
    public GameObject hitEffect;
    public int Health = 50;
    public bool canExplode = true;
    public GameObject explosion;
    // Start is called before the first frame update
    public void Damage(int amount)
    {
        if (canExplode)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Destroy(gameObject);
                if(explosion!=null)
                {
                    GameObject explode = Instantiate(explosion, transform.position, transform.rotation);
                    Destroy(explode, 2.5f);

                }
            }
        }
    }
    public void Hit(Vector3 point, Quaternion pos)
    {
        Instantiate(hitEffect,point, pos, parent: gameObject.transform);
    }
}
