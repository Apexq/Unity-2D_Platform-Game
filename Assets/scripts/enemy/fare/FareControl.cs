using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FareControl : MonoBehaviour
{
    public float throwAngle = 45f; // Angle for the throw
    public GameObject tukuruk; // Assign your 'tükürük' prefab in the inspector
    public Transform throwPoint; // Where the tükürük will be thrown from
    public float followSpeed = 2f; // Fare'nin takip etme hızı

    private bool playerInRange = false;
    private Transform playerTransform;
    private bool isActionActive = false;

    private void Update()
    {
        if (playerInRange && playerTransform != null && !isActionActive)
        {
            StartCoroutine(ActionLoop());
        }
    }

    private IEnumerator ActionLoop()
    {
        isActionActive = true;

        while (playerInRange && playerTransform != null)
        {
            // 3 kere ateş et
            for (int i = 0; i < 3; i++)
            {
                ThrowTukuruk(playerTransform);
                yield return new WaitForSeconds(0.6f);
            }

            // 4 saniye boyunca takip et
            float followTime = 4f;
            float timer = 0f;

            while (timer < followTime && playerInRange && playerTransform != null)
            {
                FollowPlayer();
                timer += Time.deltaTime;
                yield return null;
            }
        }

        isActionActive = false;
    }

private void FollowPlayer()
{
    if (playerTransform == null) return;

    // Yön belirleme (scale ile flip yapıyoruz)
    if (playerTransform.position.x > transform.position.x)
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    else
    {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    // Hareket
    Vector2 direction = (playerTransform.position - transform.position).normalized;
    transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, followSpeed * Time.deltaTime);
}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            playerTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerTransform = null;
            StopAllCoroutines();
            isActionActive = false;

        }
    }

   void ThrowTukuruk(Transform target)
{
    if (target == null || tukuruk == null || throwPoint == null)
        return; // Hedef yoksa tükürme

    // Hedefe dön! (yüzünü çevir)
    if (target.position.x > transform.position.x)
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    else
    {
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    // Sonra tükürüğü fırlat
    GameObject Tukuruk = Instantiate(tukuruk, throwPoint.position, Quaternion.identity);
    Rigidbody2D rb = Tukuruk.GetComponent<Rigidbody2D>();
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
