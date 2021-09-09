using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestPlayer : MonoBehaviour
{
    private Vector3 vector;
    
    public float speed=2.5f;
    public float jumppower = 0.03f;
    public float endjump=0;
    public bool jumping = false;
    public bool jumpdown = false;
    public float firstJump;
    public float lastJump;
    public skill attackami;
    private GameObject attack;
    float time;
    private bool attackTurn;

    public float playerx;
    public float playery;
    
    // Start is called before the first frame update
    void Start()
    {
        firstJump = -0.4f;
        lastJump =0.5f;
        attackami = FindObjectOfType<skill>();
        attack = this.transform.Find("QSkill").gameObject;
        attack.SetActive(false);
        attackTurn = false;
        
        
        //Hitbox 

    }

    // Update is called once per frame
    void Update()
    {
        playerx = this.transform.position.x;
        playery = this.transform.position.y;
        time += Time.deltaTime;
        if (vector == Vector3.right)
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
       if(vector==Vector3.left)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        endjump = this.transform.position.y;
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Debug.Log(endjump);
            if (endjump < lastJump && jumpdown == false)
            {


                jumping = true;


            }
            
            
        }
        Jumping();
        JumpDown();
        if (Input.GetKey(KeyCode.Q)&&attackTurn==false)
        {
            attackTurn = true;
            attack.SetActive(true);
            
            
        }
    }
    private void FixedUpdate()
    {
        if (time > 1.1f&&attackTurn==true)
        {


          
            attackami.skillani.SetBool("Skill2", true);
        }
        if (time > 2.5f)
        {
            attack.SetActive(false);
            attackami.skillani.SetBool("Skill2", false);
            attackTurn = false;
            time = 0;
            
            
        }
    }
    public void Jumping()
    {
        if (jumping == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + jumppower, transform.position.z);

        }
        if (endjump >= lastJump)
        {
            jumping = false;
            jumpdown = true;
        }
    }
    public void JumpDown()
    {
        if (jumpdown == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - jumppower, transform.position.z);
        }
        if (endjump <= firstJump)
        {
            jumpdown = false;
        }
    }
    public void Move()
    {
        vector = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            vector = Vector3.right;
            //transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);

        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //transform.position = new Vector3(transform.position.x  -speed * Time.deltaTime, transform.position.y, transform.position.z);
            vector = Vector3.left;
        }
        transform.position += vector * speed * Time.deltaTime;
       
    }
    void JumpMove()
    {
        if (Input.GetAxisRaw("Horizontal") > 0&&vector==Vector3.right)
        {
           
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);

        }
        if (Input.GetAxisRaw("Horizontal") < 0 && vector == Vector3.left)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            
        }
    }
    
}
