using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class biliminsaniControl : MonoBehaviour
{
    public GameObject tupPrefab; // Assign your 'tup' prefab in the inspector
    public Transform throwPoint; // Where the bottle will be thrown from
    public float throwForce = 7f;
    public float throwAngle = 45f; // Angle for the throw
    public float throwInterval = 2f; // Interval between throws in seconds
    public bool isEntered = false;
    private Collider2D other; // Store the collider reference
    private bool canThrow = true; // Flag to control throwing

    void Update()
    {
        if (isEntered)
        {
            if (canThrow)
            {
                StartCoroutine(gecikmeliTupAtma());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEntered = true;
            this.other = other; // Store the collider reference
        }


    }
    private void OnTriggerExit2D(Collider2D other)
    {
         if (other.CompareTag("Player"))
    {
        isEntered = false;
    }
    }
    private IEnumerator gecikmeliTupAtma()
    {
        canThrow = false;
        ThrowTup(other.transform); // Use the stored collider reference
        yield return new WaitForSeconds(3f); // Wait for the specified interval
        canThrow = true;

    }


    void ThrowTup(Transform target)
    {
        if (tupPrefab != null && throwPoint != null)
        {
            GameObject tup = Instantiate(tupPrefab, throwPoint.position, Quaternion.identity);
            Rigidbody2D rb = tup.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 start = throwPoint.position;
                Vector2 end = target.position;
                float distance = end.x - start.x;
                float gravity = Mathf.Abs(Physics2D.gravity.y);
                float angle = throwAngle * Mathf.Deg2Rad;

                // Calculate initial velocity
                float velocity = Mathf.Sqrt(Mathf.Abs(distance) * gravity / Mathf.Sin(2 * angle));
                float vx = velocity * Mathf.Cos(angle) * Mathf.Sign(distance);
                float vy = velocity * Mathf.Sin(angle);

                rb.velocity = new Vector2(vx, vy);
            }
        }
    }
}