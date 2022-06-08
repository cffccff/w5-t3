using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightScript : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D knightRigid;
    private BoxCollider2D coll;
    private float dirX = 0f;
    [SerializeField] private LayerMask jumpableGround;
    private float speed = 0f;
    private bool isAttacking = false;
    private Sprite sprite;
   private Slider slider;
    private int dmg = 10000;
    private  SpriteRenderer rend;
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;
    public int knightDamage = 3000;
    
    private enum MovementState { MoveBlend,Jump, Fall,SwordAttack,Death }
  
    private void Awake()
    {
     
        coll = GetComponent<BoxCollider2D>();
        knightRigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<Sprite>();
        slider = GetComponentInChildren<Slider>();
        slider.maxValue = 99999;
        slider.minValue = 0;
        slider.value = 99999;
        rend = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        dirX = Input.GetAxisRaw("Horizontal");
        knightRigid.velocity = new Vector2(dirX * speed*3,knightRigid.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            knightRigid.velocity = new Vector3(0, 15f, 0);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

            speed = 1.62f;
        }
        else
        {
            speed = 0;
        }

        if (dirX == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = true;
         
        }
        if (slider.value > 0)
        {
            FlipSpriteknightRigid(dirX);
        }
        
        UpdateState();

    }
    private void LateUpdate()
    {
        if (slider.value <= 0)
        {
            rend.sprite = Resources.Load<Sprite>("clown_export-dead_47");
        }
    }
   
    
    private void FlipSpriteknightRigid(float horizontalInput)
    {
        if (horizontalInput > 0.01f) 
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f) transform.localScale = new Vector3(-1, 1, 1);
    }
    private void UpdateState()
    {



              MovementState state = MovementState.MoveBlend;
            if (knightRigid.velocity.y > 0.1f)
            {
            
                state = MovementState.Jump;
            }
            else if (knightRigid.velocity.y < -0.1f)
            {
                state = MovementState.Fall;
            }

            if (Mathf.Abs(dirX) > 0f && speed < 1.51f)
            {

                speed = 1.4f;
                animator.SetFloat("Speed", speed);
            }
            else if (speed > 1.6f)
            {
                speed = 3f;
                animator.SetFloat("Speed", speed);
            }
            else
            {
                speed = 0f;
                animator.SetFloat("Speed", speed);
           }

        if (isAttacking == true)
        {
            state = MovementState.SwordAttack;
        }
        if (slider.value <= 0)
        {
            state = MovementState.Death;
            knightRigid.simulated = false;
         
        }

        animator.SetInteger("state", (int)state);
        
        isAttacking = false;
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Trap"))
        {

            //  DOTween.To(() => slider.value, x => slider.value = x, 1000, animationTime);
               slider.value = slider.value - dmg;
            Debug.Log("value :" + slider.value);
          

        }
       
    }

  

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
