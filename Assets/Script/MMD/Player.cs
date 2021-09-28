using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float m_Vertical;
    float m_Horizontal;


    public float forceSize = 100.0f;
    private Animator animator;
    private Rigidbody rigidbody;

    private int veticalId = Animator.StringToHash("Vertical");
    private int honrizontalId = Animator.StringToHash("Horizontal");
    private int sprintId = Animator.StringToHash("Sprint");
    private int backId = Animator.StringToHash("Back");
    private int JumpId = Animator.StringToHash("Jump");
    private int InAirId = Animator.StringToHash("InAir");
    private float speed = 0.0f;
    private bool isSprint = false;
    private Vector2 inputPre = new Vector2();
    private bool isBack = false;
    private bool isJump = false;
    private bool isInAir = false;
    //用于人物位移的
    public float m_speedInFunction = 5.0f;
    
    private float honrizontal = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    
        SetAnimatorParams();
        Move();
    
    }

    public void Jump()
    {
        if (isJump && !isInAir)
        {
        
            isInAir = true;
            animator.SetBool(InAirId,isInAir);
            animator.CrossFadeInFixedTime("Jump", 0.0f);
            rigidbody.velocity +=Vector3.up*forceSize;
            isJump = false;
        }
    }

    private void FixedUpdate()
    {
        Jump();
    }

    public void HandleInput()
    {
        // gets input from mobile           
        inputPre.y = Input.GetAxis("Vertical"); /*检测垂直方向键*/
        inputPre.x = Input.GetAxis("Horizontal"); /*检测水平方向键*/

        isSprint = Input.GetKey(KeyCode.LeftShift);
        isJump = Input.GetKey(KeyCode.Space);
        
        speed =  inputPre.y;
        honrizontal = inputPre.x;
        

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Ground") && isInAir)
        {
            isInAir = false;
            animator.CrossFadeInFixedTime("LandSoft", 0.0f);
        }
     }

    public void SetAnimatorParams()
    {
        animator.SetFloat(veticalId,speed*3.8f);
        animator.SetFloat(honrizontalId,honrizontal*2.43f);
        animator.SetBool(sprintId,isSprint);
        if (isJump)
        {
            animator.SetTrigger(JumpId); 
        }

    }

    public void Move()
    {
        float  tempSpeed = (float) (isSprint?m_speedInFunction * 1.5 : m_speedInFunction);
       // transform.Translate(Vector3.forward * inputPre.y * tempSpeed * Time.deltaTime);//W S 上 下
        //transform.Translate(Vector3.right * inputPre.x * tempSpeed * Time.deltaTime);//A D 左右
    }
}
