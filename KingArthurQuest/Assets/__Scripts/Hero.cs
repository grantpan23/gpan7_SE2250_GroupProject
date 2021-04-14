using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
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
    public GameObject projectile;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;

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
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }

        // For firing arrows
        else if(Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack)
        {
            StartCoroutine(SecondAttackCo());
        }

        // For walking
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        if (currentHealth.RuntimeValue>0)
        {
            playerHealthSignal.Raise();
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        
    }

    // knock back co
    private IEnumerator KnockCo(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        currentState = PlayerState.idle;
        myRigidbody.velocity = Vector2.zero;

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

    // Bow Attacking co-routine
    private IEnumerator SecondAttackCo()
    {
        // Starting attack animation
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;

        // Putting small delay, waiting one frame
        yield return null;
        MakeArrow();

        // Preventing it from going back into the attack animation
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    // Creating arrow at player position
    private void MakeArrow()
    {
        Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, ChooseArrowDirection());
    }

    // Determines the arrows correct direction
    Vector3 ChooseArrowDirection()
    {
        // Determining angle of the arrow
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0,0,temp);
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
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change*speed *Time.deltaTime);
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("chalice"))
        {
            ScoreController.scoreValue += 500;
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("coin"))
        {
            ScoreController.scoreValue += 50;
            Destroy(other.gameObject);
        }
    }
}
