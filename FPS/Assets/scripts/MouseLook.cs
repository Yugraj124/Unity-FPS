using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSenstivity = 100f;
    public Transform player;

    float xRotation = 0;

    float upRecoil = 0f;
    float sideRecoil = 0f;
    float sideRecoilAmount = 0f;
    float upRecoilAmount = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = sideRecoil + Input.GetAxis("Mouse X") * mouseSenstivity * Time.deltaTime;
        float mouseY = upRecoil / 2f + Input.GetAxis("Mouse Y") * mouseSenstivity * Time.deltaTime;

        upRecoil -= upRecoilAmount;
        sideRecoil -= sideRecoilAmount;
        if (upRecoil < 0)
        {
            upRecoil = 0f;
            upRecoilAmount = 0f;
        }
        if (sideRecoil < 0) 
        { 
            sideRecoil = 0f;
            sideRecoilAmount = 0f;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);

    }

    public void AddRecoil(float up, float side)
    {
        upRecoil = up;
        sideRecoil = side;
        upRecoilAmount = up * Time.deltaTime * 20f;
        sideRecoilAmount = side * Time.deltaTime * 20f;
    }
}
