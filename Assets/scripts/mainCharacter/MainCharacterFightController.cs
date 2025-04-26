using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [Header("damage settings")]
    public float damage = 5;
    public float damageWaitingTime = 0.2f;
    private float damageTimeCounter = 0;
    public GameObject Bullet;
    public float bulletVectorMagnitude = 1;
    private float bulletTimeCounter = 0;
    public float bulletTime = 0.2f;
    public float offset = 0;

    private Animator PlayerAnimator;

    private bool isAttacking = false;
    private GameObject enemy;

    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        PunchAttackControl();
        AttackTimer();
        Shotattack();
    }

    private void PunchAttackControl()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
            PlayerAnimator.SetBool("isAttacking",true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isAttacking = false;
            PlayerAnimator.SetBool("isAttacking", false);
        }
    }

    private void Shotattack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            PlayerAnimator.SetTrigger("isShot");

            GameObject Clone = Instantiate(Bullet, this.gameObject.transform.position + new Vector3(0, offset, 0), this.gameObject.transform.rotation);
            Clone.SetActive(true);
            Clone.GetComponent<Rigidbody2D>().gravityScale = 0;

            if (this.gameObject.transform.rotation.y != 0)
            {
                Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bulletVectorMagnitude, ForceMode2D.Impulse);
            }
            else
            {
                Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.right * bulletVectorMagnitude, ForceMode2D.Impulse);
            }
          
        }
    }
    private void AttackTimer() 
    {
        if (isAttacking && enemy != null)
        {
            damageTimeCounter += Time.deltaTime;

            if (damageTimeCounter > damageWaitingTime)
            {
                damageTimeCounter = 0;
                // Önce Undamagable mı diye kontrol et
                Undamagable undamagable = enemy.GetComponent<Undamagable>();
                if (undamagable != null)
                {
                    // Eğer Undamagable ise, hasar vermek yerine Warning göster
                    FindObjectOfType<AnimalPlantHitWarning>().ShowWarning();
                }
                else
                {
                    // Normal düşman → hasar ver
                    enemy.GetComponent<healthControl>().takeDamege(damage);
                }
            }
        }
    }

    private void shotTimer()
    {
        bulletTimeCounter += Time.deltaTime;

        if (bulletTime < bulletTimeCounter)
        {
            Shotattack();
            bulletTimeCounter = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
           enemy = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            enemy = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ball"))
        {
            if (Bullet.name.Split("(")[0] != collision.gameObject.name.Split("(")[0])
            {
                Destroy(Bullet);
                Bullet = collision.gameObject;
                collision.gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.tag.Equals("HealthPosion"))
        {
            this.gameObject.GetComponent<healthControl>().takeHealth(collision.gameObject.GetComponent<lootsDefualtBehavior>().getHealth());
            Destroy(collision.gameObject);
        }
    }

}
