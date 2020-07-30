using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponChange : MonoBehaviour
{
    int selectedWeapon = 0;
    int previousWeapon;

    public int SelectedWeapon
    {
        set { selectedWeapon = value; }
    }

    void Start()
    {
        selectedWeapon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        previousWeapon = selectedWeapon;
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectedWeapon = 4;
        }
        if (previousWeapon != selectedWeapon)
        {
            selectWeapon();
        }
    }

    public void selectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }


}
