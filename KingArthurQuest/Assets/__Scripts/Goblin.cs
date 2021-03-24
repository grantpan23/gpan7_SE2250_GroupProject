using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    // Declaring variables
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public float hp;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        hp = 40;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        CheckDamageToHero();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
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
}
