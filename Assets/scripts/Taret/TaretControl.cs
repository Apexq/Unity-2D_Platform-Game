using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaretControl : MonoBehaviour
{
    public GameObject taretTopuPrefab; // Reference to the TaretTopu prefab
    public Transform firePoint; // The point from where the TaretTopu will be fired
    public float fireRate = 1f; // Fire rate in seconds
    private float nextFireTime = 0f; // Time to track the next fire

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Fire()
    {
        GameObject taretTopu = Instantiate(taretTopuPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = taretTopu.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(firePoint.up * 500f); // Apply force to the TaretTopu in the upward direction
        }
    }
}
