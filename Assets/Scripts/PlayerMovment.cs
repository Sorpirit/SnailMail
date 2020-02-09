using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovment : MonoBehaviour
{

    [SerializeField] private float maxSpeed;
    [SerializeField] private float acc;
    [SerializeField,Range(0f,1f)] private float stopDamp;
    [SerializeField,Range(0f,1f)] private float chengDirDamp;
    [SerializeField,Range(0,.09f)] private float accuracy;
    [SerializeField] private Camera cam;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform grabAnchor;


    private enum PlayTest{
        velocityMode,
        positionMode,
    }
    private Rigidbody2D myRb;

    private float horInout;
    private float vertInput;
    private Vector2 mousePos;
    [SerializeField] private PlayTest mode;

    private void Awake() 
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {

        horInout = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        mousePos = Input.mousePosition;
        Animate();

    }

    private void FixedUpdate() 
    {
        if(Input.GetKeyDown(KeyCode.M)){
            mode = mode == PlayTest.velocityMode ? PlayTest.positionMode : PlayTest.velocityMode;
        }


        //RotatePlayer();

        if(mode == PlayTest.velocityMode)
        
            MovePlayerVelocity();   
        else
            MovePlayerPosition();

    
    }

    private void MovePlayerVelocity()
    {

        Vector2 direction = new Vector2(horInout,vertInput).normalized;
        myRb.velocity += direction * acc * Time.deltaTime;
        myRb.velocity = Vector2.ClampMagnitude((Vector2) myRb.velocity,maxSpeed);

        /*
        if(Mathf.Abs(horInout) < .1f && Mathf.Abs(vertInput) < .1f)
        {
            myRb.velocity += myRb.velocity.normalized * -1 * stopDamp * Time.deltaTime;

            if(myRb.velocity.magnitude < .1f){
                myRb.velocity = Vector2.zero;
            }
        }
        */
        
        if(Mathf.Abs(horInout) <.1f)
        {
            myRb.velocity = new Vector2(myRb.velocity.x * (1f - stopDamp) ,myRb.velocity.y);

            if(Mathf.Abs(myRb.velocity.x) < accuracy)
            {
                myRb.velocity = new Vector2(0,myRb.velocity.y);
            }

        }
        else if(Mathf.Sign(horInout) != Mathf.Sign(myRb.velocity.x))
        {

            myRb.velocity = new Vector2(myRb.velocity.x * (1f - stopDamp) ,myRb.velocity.y);

            if(Mathf.Abs(myRb.velocity.x) < accuracy)
            {
                myRb.velocity = new Vector2(0,myRb.velocity.y);
            }

        }

        if(Mathf.Abs(vertInput) <.1f)
        {
            myRb.velocity = new Vector2(myRb.velocity.x,myRb.velocity.y * (1f - stopDamp));

            if(Mathf.Abs(myRb.velocity.y) < accuracy)
            {
                myRb.velocity = new Vector2(myRb.velocity.x,0);
            }

        }
        else if(Mathf.Sign(vertInput) != Mathf.Sign(myRb.velocity.y))
        {

            myRb.velocity = new Vector2(myRb.velocity.x,myRb.velocity.y * (1f - stopDamp));

            if(Mathf.Abs(myRb.velocity.y) < accuracy)
            {
                myRb.velocity = new Vector2(myRb.velocity.x,0);
            }

        }


        
           

    }
    private void MovePlayerPosition(){
        Vector2 direction = new Vector2(horInout,vertInput).normalized;
        
        myRb.MovePosition(myRb.position + direction * maxSpeed * Time.deltaTime);
    }


    private void RotatePlayer()
    {
        
        Vector2 viewDir = (cam.ScreenToWorldPoint(mousePos) - transform.position).normalized;
        transform.up = viewDir;

    }

    private void Animate(){
        Vector2 dir = new Vector2(horInout,vertInput);
        if(dir != Vector2.zero){
            
            if(transform.localScale.x * dir.x < 0)
                transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y);
            else if(dir.x == 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y);
            
            if(dir.x == 0)
                grabAnchor.transform.eulerAngles = new Vector3(0,0,90 * dir.y);
            else
                grabAnchor.transform.eulerAngles = new Vector3(0,0,90 * dir.y * dir.x);

            animator.SetFloat("Horisontal",dir.x);
            animator.SetFloat("Vertical",dir.y);
        }
       
        animator.SetFloat("Speed",dir.magnitude);
    }

}
