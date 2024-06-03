using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riffle : MonoBehaviour
{
    [Header("Riffle")]
    public Camera cam;
    public float giveDamage = 10f;
    public float shootingRange = 100f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        RaycastHit hitInfo;
        Debug.Log("Yuh");

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);
        } else
        {
            Debug.Log("Yuh");

        }
    }
}
