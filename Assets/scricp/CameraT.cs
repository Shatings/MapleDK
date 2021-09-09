using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraT : MonoBehaviour
{
    public float cameraspeed;
    public Player Fplayer;
    private Vector3 vector;
   

    void Start()
    {
        Fplayer = FindObjectOfType<Player>();
        cameraspeed = Fplayer.speed;
    }

    // Update is called once per frame
    void Update()
    {
       
        Move();
        //if (Fplayer.endjump - this.transform.position.y > 0)
        //{
        //    this.transform.position = new Vector3(this.transform.position.x, transform.position.y + Fplayer.jumppower, this.transform.position.z);
        //}
        //if (Fplayer.endjump- this.transform.position.y < 0)
        //{
        //    this.transform.position = new Vector3(this.transform.position.x, transform.position.y - Fplayer.jumppower, this.transform.position.z);
        //}
        
       
    }
    void Move()
    {
       
            //vector = Vector3.zero;
            //if (Input.GetAxisRaw("Horizontal") > 0)
            //{
            //    vector = Vector3.right;
            //    //transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);

            //}
            //if (Input.GetAxisRaw("Horizontal") < 0)
            //{
            //    //transform.position = new Vector3(transform.position.x  -speed * Time.deltaTime, transform.position.y, transform.position.z);
            //    vector = Vector3.left;
            //}
            //transform.position += vector * Player.speed * Time.deltaTime;
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                if (this.transform.position.x - Fplayer.transform.position.x < 5.0f)
                {
                    transform.position = new Vector3(transform.position.x + cameraspeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + cameraspeed * Time.deltaTime, transform.position.y, transform.position.z);
                }


            }
            else
            {
                if (this.transform.position.x - Fplayer.transform.position.x > -5.0f)
                {
                    transform.position = new Vector3(transform.position.x - cameraspeed * Time.deltaTime, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x - cameraspeed * Time.deltaTime, transform.position.y, transform.position.z);
                }

            }
        }
        if (Fplayer.transform.position.x - this.transform.position.x >= 40f)
        {
            transform.position = new Vector3(transform.position.x  * cameraspeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (Fplayer.transform.position.x - this.transform.position.x <= -40f)
        {
            transform.position = new Vector3(transform.position.x  * cameraspeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Fplayer.transform.position.y - this.transform.position.y >= 21f)
        {
            transform.position = new Vector3(transform.position.x, transform.transform.position.y + 2 * cameraspeed * Time.deltaTime, transform.position.z);
        }
        else if (Fplayer.transform.position.y - this.transform.position.y <= -21f)
        {
            transform.position = new Vector3(transform.position.x, transform.transform.position.y - 2 * cameraspeed * Time.deltaTime, transform.position.z);
        }



    }

}
    

