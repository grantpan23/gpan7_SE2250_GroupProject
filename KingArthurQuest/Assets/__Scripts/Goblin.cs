using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    // Declaring variables
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public float hp;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        hp = 40;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
        CheckDamageToHero();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
    }

    void CheckDamageToHero()
    {
        if(Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            hp -= 1;
            print(hp);
            if (hp <= 0)
            {
                target.gameObject.SetActive(false);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}