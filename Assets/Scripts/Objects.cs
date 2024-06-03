using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public float objectHealth = 100f;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void objectHitDamage(float amount)
    {
        objectHealth -= amount;

        if (objectHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
