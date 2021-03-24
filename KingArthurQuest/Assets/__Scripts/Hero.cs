using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class Hero : MonoBehaviour
{
    static public Hero S;
    public PlayerState currentState;
    public float speedMultiplier;
    private float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change; 
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody =  GetComponent<Rigidbody2D>();
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!"); //if Hero already exists show error in Console
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey (KeyCode.LeftShift)){
            speed = 4*speedMultiplier;
        }
        else{
            speed = 2*speedMultiplier;
        }
    
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        // For attacking
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }

        // For walking
        else if(currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
    }

    // Attacking co-routine
    private IEnumerator AttackCo()
    {
        // Starting attack animation
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;

        // Putting small delay, waiting one frame
        yield return null;

        // Preventing it from going back into the attack animation
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove(){
        if(change != Vector3.zero){
            MoveCharacter();
            animator.SetFloat("moveX",change.x);
            animator.SetFloat("moveY",change.y);
            animator.SetBool("moving", true);
        }
        else{
            animator.SetBool("moving",false);
        }
    }

    void MoveCharacter(){

        myRigidbody.MovePosition(transform.position + change*speed *Time.deltaTime);
    }

   
}
