using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Apply velocity and direction to arrow
    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed; // Setting velocity to be the same in all direction
        transform.rotation = Quaternion.Euler(direction);   // Changing rotation of the arrow depending on its direction
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
