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
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Enemy en = (Enemy) FindObjectOfType(typeof(Enemy));
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<Hero>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<Hero>().currentState = PlayerState.stagger;
                        other.GetComponent<Hero>().Knock(knockTime, damage);
                    }
                }
                
                //hit.isKinematic = false;
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
               // hit.isKinematic = true;

                // damage enemy health
                //en.health = en.health - 1;
                //if (en.health <= 0)
                // {
                //    enemy.gameObject.SetActive(false);
                //}

                // enemy.gameObject.SetActive(false);
                
            }
        }
    }

    


}
