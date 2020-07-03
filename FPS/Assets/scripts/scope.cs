using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scope : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject scopeOverlay;
    public GameObject weaponCam;
    public Animator animator;
    public Camera mainCam;
    public int scopeX = 2;


    int isScoped = 0;
    float originalFOV;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        originalFOV = mainCam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (isScoped == 0)
                StartCoroutine(Onscope());
            else if (isScoped == 1)
                onScopedouble();
            else
                OnUnscope();
        }
    }
    private void OnDisable()
    {
        OnUnscope();
    }
    private void OnEnable()
    {
        isScoped = 0;
        animator.SetBool("isscoped", false);
    }
    IEnumerator Onscope()
    {
        animator.SetBool("isscoped", true);
        isScoped = 1;
        yield return new WaitForSeconds(0.30f);
        scopeOverlay.SetActive(true);
        weaponCam.SetActive(false);
        mainCam.fieldOfView=originalFOV/scopeX;
    }
    void OnUnscope()
    {
        
        isScoped = 0;
       
        scopeOverlay.SetActive(false);
        weaponCam.SetActive(true);
        animator.SetBool("isscoped", false);
        mainCam.fieldOfView = originalFOV;
    }
    void onScopedouble()
    {
        mainCam.fieldOfView = originalFOV /(2* scopeX);
        isScoped = 2;
    }
}
