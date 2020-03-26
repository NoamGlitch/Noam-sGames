using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //resources

    //Start() variables
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    //enumerator state called by the state number. FSM
    private enum State {idle, running, jumping, falling}
    private State state = State.idle;

    //small important things
    [SerializeField] private LayerMask ground;
    private int jumpCount;

    //change through the unity editor
    public int jumpNumInAir = 3;
    public float nowMoveSpeed = 12f;
    public float normalMoveSpeed = 12f;
    public float jumpDistance = 30f;
    public float fastMoveSpeed = 2f;

    //end-resources


    //when the object is active in the scene
    private void Start()
    {
        //find the RigidBody
        rb = GetComponent<Rigidbody2D>();
        //find the Animator
        anim = GetComponent<Animator>();
        //find the Collider
        coll = GetComponent<Collider2D>();
    }

    //every frame in the game
    private void Update()
    {
        //refrencing the Methodes
        InputManager();
        VelocityState();
        //sets animation based on the enumerator state that is up there
        anim.SetInteger("state", (int)state);
        
    }

    private void InputManager()
    {
        //horizontal moving axis
        float hDirection = Input.GetAxis("Horizontal");

        //animation speed
        anim.speed = 1;

        //shift clicking for speeding the character
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //making the moving speed faster
            nowMoveSpeed = normalMoveSpeed + fastMoveSpeed;
            //making the animation faster
            anim.speed = 2;
        }
        //if the shift is not clicked
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            //normalizing the moving speed
            nowMoveSpeed = normalMoveSpeed;
            //normalizing the animation speed
            anim.speed = 1;
        }

        //right movement
        if (hDirection > 0)
        {
            //RigidBody move forcing
            rb.velocity = new Vector2(nowMoveSpeed, rb.velocity.y);
            //make the character look to the right place
            transform.localScale = new Vector2(1, 1);
        }
        //left movement
        else if (hDirection < 0)
        {
            //RigidBody move forcing
            rb.velocity = new Vector2(-nowMoveSpeed, rb.velocity.y);
            //make the character look to the right place
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            //if not clicking on any key the program needs to do nothing
        }

        //jumping, and make a jump number limitation
        //if the jumping key pressed, and the character is not touching the ground
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            //RigidBody jump forcing
            rb.velocity = new Vector2(rb.velocity.x, jumpDistance);
            //tell the animation to play the jump animation
            state = State.jumping;
            //add 1 to the jumping number on air...
            jumpCount++;
        }
        //if the jumping key pressed, and the jump count is less then the accepted jumps in one jump
        else if (Input.GetButtonDown("Jump") && jumpCount < jumpNumInAir)
        {
            //RigidBody jump forcing
            rb.velocity = new Vector2(rb.velocity.x, jumpDistance);
            //tell the animation to play the jump animation
            state = State.jumping;
            //add 1 to the jumping number on air...
            jumpCount++;
        }
        //if the character is just touching the ground
        else if (coll.IsTouchingLayers(ground))
        {
            //jump number will be equal to 0
            jumpCount = 0;
        }
    }
    

    //That Function is transisioning between the states.
    private void VelocityState()
    {
        //if the character jumping
        if(state == State.jumping)
        {
            //then
            //if the jump forcing is less the 0.1
            if(rb.velocity.y < 0.1f)
            {
                //tell the animator to change to a fall animation
                state = State.falling;  
            }
        }
        //if the character falling
        else if(state == State.falling)
        {
            //then
            //if the character touches the ground
            if(coll.IsTouchingLayers(ground))
            {
                //tell the animator to change to an idle animation
                state = State.idle;
            }
        }
       
        //if the 
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;

        }else
        {

            state = State.idle;
        }
    }
}
