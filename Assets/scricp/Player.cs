using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,ObjInterface
{
	public static string gType = "Player";
	public ObjBase mOb;

	private Vector3 vector;
	
	public float speed;
	private bool jump = false;
	public int jumpcount = 2;
	public bool canmove = true;
	private bool attacktime = true;
	public int attackturn;
	public float floor1y = 0f;
	public bool f1jump = false;
   
  
	public Rigidbody2D rigid;
	
	

	public Collider2D collie;
	public int dead = 0;

	public bool wait = false;
	private bool ground = false;
	private int  fly = 0;
	private float waitTime = 0;

	public F2Ground f2G;
	public float jumppower = 0.04f;
	public float endjump = -0.5f;
	
	
	
	public float maxJump;

	public Vector3 oldPos;
	

	
	public int jumpState = 0;
	public GameObject invetory;
	
	public int level;
	public int curexp;
	public int maxexp;



	//public void OnAttack(MonoBehaviour mb)
	//{
	//    if (mb.name.Equals("Emy") || mb.name.Equals("Emy(Clone)"))
	//    {
	//        Debug.Log(" " + mb.name);
	//        Emy1 es = (Emy1)mb;
	//        es.mOb.curhp -= mOb.attackp;
	//        Debug.Log(mb.name + " Hit " + mOb.attackp);
	//        if (es.mOb.curhp <= 0)
	//        {

	//            Debug.Log("Dead");

	//            GameObject.Destroy(es.gameObject);


	//            Gv.gThis.mOm.Remove(es.mOb);


	//        }

	//    }



	//}
	//public Vector3 getPos()
	//{
	//    return this.transform.position;
	//}

	// Start is called before the first frame update
	void Start()
	{
		mOb = new ObjBase();
		mOb.mMb = this;
		mOb.mType = Player.gType;
		wait = false;

		f2G = FindObjectOfType<F2Ground>();
		Gv.gThis.mOm.Add(this.mOb);
		

		
		//Hitbox 
		mOb.HitboxR = this.transform.Find("HItBoxRight").gameObject;
		mOb.HitboxR.SetActive(false);
		mOb.HitboxL = this.transform.Find("HItBoxLeft").gameObject;
		mOb.HitboxL.SetActive(false);
		mOb.righ= "Right";
		invetory = GameObject.Find("Canvas").transform.Find("Scroll View").gameObject;
		invetory.SetActive(false);


		floor1y = -0.42f;
		transform.position = new Vector3(transform.position.x,floor1y,transform.position.z);
		f1jump = true;
		jumpState = 0;


	}

	// Update is called once per frame
	void Update()
	{
		oldPos = mOb.getPos();
		endjump = this.transform.position.y;
		Om om = Gv.gThis.mOm;
		mOb.time += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.I))
        {
			if (invetory.activeSelf == true)
			{
				invetory.SetActive(false);
			}
			else
			{
				invetory.SetActive(true);
			}
			
        }
		if (Input.GetKeyDown(KeyCode.DownArrow)&&Input.GetKeyDown(KeyCode.Space)&&ground==true&&fly==0)
		{  
			lowerJump();
			fly++;
			jumpcount = 1;
			
		   

		}
	  
		//Debug.Log("" + HitboxR.transform.position);

		if (Input.GetKeyDown(KeyCode.Q))
		{   
			GameObject hitbox = mOb.GetHitBox();
			List<ObjBase> fos = mOb.Httest(hitbox);

			for (int i = 0; i < fos.Count; i++)
			{

				if (fos[i].mType == Emy1.gType || fos[i].mType == Emy2.gType)
				{
					mOb.Attack1(fos[i]);
				}

			}
			hitbox.SetActive(true);
			 

		}
		
		JumpProcess(om);

		
		
		if(mOb.getPos().y <= floor1y )
		{
			Debug.Log("mbfo=tr");
			
			jumpState = 0;
			this.transform.position = new Vector3(transform.position.x, floor1y, transform.position.z);

		}


		if (wait == true)
		{
			Wait();
		}
	   

	}
	void Wait()
	{
		waitTime += Time.deltaTime;
		canmove = false;
		Debug.Log("기다리기");
		if (waitTime > 1.0f)
		{
			Debug.Log("나가기~");
			canmove = true;
			wait = false;
			waitTime = 0;
		}
	}
	public void JumpProcess(Om om)
	{
		if (jumpState==0||jumpState == 1 || jumpState == 2)
		{
			if(jumpState == 0 && Input.GetKeyDown(KeyCode.DownArrow))
			{
				Debug.Log("다운다운다운");
				jumpState = 2;
				mFloor = null;
				transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
				oldPos = mOb.getPos();
			}
			else if (Input.GetKeyDown(KeyCode.Space))
			{
				maxJump = transform.position.y + 2f;
				Debug.Log("더블점프");
				if (jumpState == 0)
				{
					jumpState = 1;
				}
				else
				{
					jumpState = 11;
				}
				
			}
		}
        switch (jumpState)
        {
			case 0:
				break;
			
			
				
			case 1:
			case 11:
				
					transform.position = new Vector3(transform.position.x, transform.position.y + jumppower, transform.position.z);
				if (endjump >= maxJump)
				{
					jumpState++;
				}
					break;

			case 2:
			case 12:
			case 13:
				JumpDown(om);
				break;
			default:
				Debug.Log("나는 무엇인가...");
				break;

        }

	}
	public ObjBase mFloor = null;
	public void fallDown(Om om)
	{
	   
		Debug.Log("FallDown(om)");
		transform.position = new Vector3(transform.position.x, transform.position.y - jumppower, transform.position.z);
		

		
		List<ObjBase> fos = om.find2fG(mOb.getFoot().x, mOb.getFoot().y, mOb.getFoot(oldPos).y);
	   
		if (fos.Count > 0)
		{
			Vector3 pos = mOb.getPos();
		  
			//mFloor = fos[0];
			FloorChange(fos[0]);
			
			transform.position = new Vector3(pos.x, fos[0].getPos().y + fos[0].getTest().y , pos.z);
		
		   
			
	   }
		


		Debug.Log(fos.Count);
		//for (int i = 0; i < fos. Count; i++)
		//{ 
		//    Debug.Log("성이길바람");
		   

		//    if(
		//     transform.position.x - f2G.transform.position.x < f2G.w / 2 &&
		//        transform.position.x - f2G.transform.position.x > -f2G.w / 2)
		//    {

		//        transform.position = new Vector3(transform.position.x, f2G.transform.position.y + f2G.h / 2, transform.position.z);
		//        Debug.Log("성공");
		//        f2jump = true;

		//    }
		//    else
		//    {
		//        Debug.Log("망");
		//        f2jump = false;
		//    }
		//}
		

	}
	public void JumpDown(Om om)
	{
		//if (mbJumpDown == true)
		{
			//Debug.Log("falldown: " + maxJump+" y: "+transform.position.y+" firstJ: "+firstJump);
		   
			fallDown(om);
			
		  
		   
			
		 
			
		}
	   
		
	}
	public void FloorChange(ObjBase floor)
	{
		mFloor = floor;
		jumpState = 0;
		
		
		
		
		

	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{

		if (collision.gameObject.tag == "Ground")
		{

			Debug.Log("점프카운트 복구");
			jumpcount = 2;
			canmove = true;
			attacktime = true;

			ground = false;
			fly = 0;
		  
		   

		}
		if (collision.gameObject.tag == "2fGround")
		{
			Debug.Log("2층");
			canmove = false;

			jumpcount = 0;
		 
		  

		}
		if (collision.gameObject.tag == "On2fGround")
		{
			Debug.Log("점프카운트 복구");
			jumpcount = 2;
			canmove = true;
			attacktime = true;

			ground = true;
			fly = 0;
		   
			
		}
	}
  
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collie.gameObject.tag == "Line")
		{
			Debug.LogError("나감");

			rigid.drag = 1;
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Line")
		{
		   
			Debug.Log("줄");
			if (Input.GetAxisRaw("Vertical")>0)
			{
				collie.isTrigger = true;
				vector = Vector3.zero;
				
				vector = Vector3.up;
				canmove = false;
				Debug.Log("줄타기위");
				rigid.drag = 10000;
				transform.position += vector*speed*Time.deltaTime;
			}
			else if(Input.GetAxisRaw("Vertical") < 0){

				collie.isTrigger = true;
				vector = Vector3.zero;
				vector = Vector3.down;
			  
				canmove = false;
			   
				Debug.Log("줄타기밑");
				rigid.drag = 10000;
				transform.position += vector * speed * Time.deltaTime;

			}
			if ((Input.GetAxisRaw("Horizontal") >=0 || Input.GetAxisRaw("Horizontal") <= 0)&&Input.GetKeyDown(KeyCode.Space))
			{
				rigid.drag = 1;
				canmove = true;
				jumpcount = 2;
				
			}
		}

		if (collision.gameObject.tag == "LineOut")
		{

			rigid.drag = 1;
			collie.isTrigger = false;


		}


	}

	private void OnTriggerEnter2D(Collider2D collision)
	{


		
		if (collision.gameObject.tag == "GOut")
		{
			Debug.Log("나가기~");
			rigid.drag = 1;
			collie.isTrigger = true;
		}
	}




	//private void OnTriggerStay2D(Collider2D collision)
	//{
	//    if (collision.gameObject.tag == "Ground")
	//    {
	//        collie.isTrigger = false;

	//    }

	//}
	private void FixedUpdate()
	{
	   
		if (mOb.time > 0.2f)
		{
			
			mOb.HitboxR.SetActive(false);
			mOb.HitboxL.SetActive(false);
				mOb.time = 0;

		}



		 
	   
		Move();
	   




	}
	public void testJump() {
		if (!jump || jumpcount == 0)
		{
		  

			return ;

		}
		
		rigid.velocity = Vector2.zero;
		Vector2 jumpvector = new Vector2(0, jumppower);



		if (jumpcount == 1)
		{
			canmove = false;
			
			if (Input.GetAxisRaw("Horizontal") >0||Input.GetAxisRaw("Horizontal")==0)
			{
			  
			   jumpvector = new Vector2(speed, jumppower);
				
			}
			if (Input.GetAxisRaw("Horizontal")<0)
			{
			   
				jumpvector = new Vector2(-speed, jumppower);
			   
			}
		   
			
			rigid.AddForce(jumpvector, ForceMode2D.Impulse);
		   
			

		}
		if (jumpcount == 2 && mOb.righ == "Right")
		{
			Debug.Log("오");
		 ;
			attacktime = false;
			rigid.AddForce(jumpvector, ForceMode2D.Impulse);
			
		}
		if(jumpcount==2&&mOb.righ.Equals("Left"))
		{
			Debug.Log("왼");
		   
		  
			attacktime = false;
			rigid.AddForce(jumpvector, ForceMode2D.Impulse);
		}
		jump = false;
		jumpcount--;
	}
	//IEnumerator Jump()
	//{
	//    if (!jump || jumpcount == 0)
	//    {


	//        yield break;
	//    }
	//    rigid.velocity = Vector2.zero;
	//    Vector2 jumpvector = new Vector2(0, jumppower);



	//    if (jumpcount == 1)
	//    {
	//        canmove = false;
	//        Debug.Log("이단점프!");
	//        jumpvector = new Vector2(speed, jumppower);
	//        jumpcount = 0;
	//        rigid.AddForce(jumpvector, ForceMode2D.Impulse);



	//    }
	//    if (jumpcount == 2)
	//        rigid.AddForce(jumpvector, ForceMode2D.Impulse);


	//    jump = false;
	//    jumpcount--;
	//}

  
	public void Move()
	{
		vector = Vector3.zero;
		if (Input.GetAxisRaw("Horizontal") > 0)
		{
			vector = Vector3.right;
			mOb.righ = "Right";
			//transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);

		}
		if (Input.GetAxisRaw("Horizontal") < 0)
		{
			//transform.position = new Vector3(transform.position.x  -speed * Time.deltaTime, transform.position.y, transform.position.z);
			vector = Vector3.left; 
			mOb.righ = "Left";

		}
		Debug.Log(" " + mOb.righ);
		transform.position += vector * speed * Time.deltaTime;

		if (jumpState==0&&mFloor != null)
		{
			
			if ((mFloor.getPos().x + mFloor.getTest().x/2) <= transform.position.x ||(mFloor.getPos().x - mFloor.getTest().x/2) >= transform.position.x)
			{
				Debug.Log("ad");
				jumpState = 2;
			}
		}



	}



	public void lowerJump()
	{

		canmove = false;
		jumpcount = 1;
		collie.isTrigger = true;
		transform.position=new Vector3(transform.position.x, transform.position.y-0.6f, transform.position.z);
	   
			
		  
		
		
		
	}
	
}