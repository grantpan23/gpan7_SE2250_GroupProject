using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class Knockback : MonoBehaviour
{

    public float thrust;
    public float knockTime;
    public float damage;
    public float health;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Enemy en = (Enemy) FindObjectOfType(typeof(Enemy));
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                enemy.isKinematic = true;
                StartCoroutine(KnockCo(enemy));

                // damage enemy health
                en.health = en.health - 1;
                if (en.health <= 0)
                {
                    enemy.gameObject.SetActive(false);
                }

                //enemy.gameObject.SetActive(false);
                
            }
        }
    }

    

    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
    }
}
