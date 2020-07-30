using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machet : MonoBehaviour
{
    public float waitTime = 0.5f;
    public int damage = 5;

    Animator animator;
    float counter;
    bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        counter = Mathf.Max(0, counter - Time.deltaTime);
        if(Input.GetMouseButtonDown(0)&&counter<=0)
        {
            StartCoroutine(attack());
        }

    }

    IEnumerator attack()
    {
        animator.SetTrigger("attack");
        counter = waitTime;
        isAttacking = true;
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;

    }


    private void OnTriggerEnter(Collider other)
    {
        if(isAttacking)
        {
            interact health = other.GetComponent<interact>();
            if(health!=null)
            {
                health.Damage(damage);
            }
        }
    }
}
