using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,ObjInterface
{
	public static string gType = "Player";
	public ObjBase mOb;

	private Vector3 vector;

	public float floor1y = 0f;
	public bool f1jump = false;
   
	public int dead = 0;

	public bool wait = false;

	public F2Ground f2G;
	public float jumppower = 0.04f;
	public float endjump = -0.5f;
	
	
	
	public float maxJump;

	public Vector3 oldPos;

	public bool waitMove;
	public bool attacking = false;

	
	public int jumpState = 0;
	public GameObject invetory;
	
	public int level;
	public int curexp=1;
	public int maxexp=100;
	public float Damagetime;
	public bool inv=false;
	public float debufftime;
	[SerializeField]
	public ObjBase mFloor = null;
	public bool checkDb = false;
	





	void Start()
	{
		maxexp = 100;
		mOb = new ObjBase();
		mOb.mMb = this;
		mOb.mType = Player.gType;
		mOb.sprite = GetComponent<SpriteRenderer>();
		wait = false;

		f2G = FindObjectOfType<F2Ground>();
		Gv.gThis.mOm.Add(this.mOb);
		mOb.ani = GetComponent<Animator>();
		mOb.clip = Resources.Load("Sound/P_Attack") as AudioClip;
		Debug.Log(mOb.clip);

		
		//Hitbox 
		mOb.Hitbox = this.transform.Find("HItBoxRight").gameObject;
		mOb.Hitbox.SetActive(false);
		//mOb.HitboxL = this.transform.Find("HItBoxLeft").gameObject;
		//mOb.HitboxL.SetActive(false);
		mOb.righ= "Left";
		invetory = GameObject.Find("Canvas").transform.Find("Scroll View").gameObject;
		invetory.SetActive(false);


		mOb.floor1y = -0.2f;
		transform.position = new Vector3(transform.position.x, mOb.floor1y, transform.position.z);
		f1jump = true;
		jumpState = 0;
		mOb.transform = this.transform;
		


	}

	private void Checkexp()
    {
		Debug.Log("max"+maxexp);
		if (curexp >= maxexp)
		{
			
			level += curexp / maxexp;
			curexp = curexp % maxexp;
			mOb.PlusLevel(this.gameObject.GetComponent<Player>());
		}
	}
	private void Skill1()
	{
		if (!FindObjectOfType<Summons>())
		{
			GameObject gameob = Resources.Load("Prefab/Skill1") as GameObject;
			Instantiate(gameob);
		}
	}
	private void Attack()
    {
		SoundMgr.instance.soundPlay(0,"P_attack");
		GameObject hitbox = mOb.GetHitBox();
		List<ObjBase> fos = mOb.Httest(hitbox);
		attacking = true;

		for (int i = 0; i < fos.Count; i++)
		{
			Debug.Log("공격중임");
			if (fos[i].mType == Emy1.gType || fos[i].mType == Emy2.gType|| fos[i].mType == Emy3.gType||fos[i].mType==Emy4.gType)
			{
				mOb.Attack1(fos[i],this.gameObject);
			}

		}
	}
	private void HitEnd()
    {
		mOb.HitEnd(mOb);
		
    }
	private void Die()
    {
		mOb.Die(this.gameObject);
    }
	private void AttackEnd()
	{
		attacking = false;
		mOb.AttackEnd(mOb);

	}
	
	// Update is called once per frame
	void Update()
	{
		
		oldPos = mOb.getPos();
		endjump = this.transform.position.y;
		Om om = Gv.gThis.mOm;
		mOb.time += Time.deltaTime;
		try
		{
			FindObjectOfType<GameMgr>().CheckHp();
		}
        catch(System.Exception e)
        {
			Debug.Log("에러" + e);
			attacking = true;
        }
		Checkexp();
        if (inv)
        {
			Damagetime += Time.deltaTime;
        }

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
		
	  
		//Debug.Log("" + HitboxR.transform.position);

		
        if (Input.GetKeyDown(KeyCode.R))
        {
			Skill1();
        }

		if (!mOb.ani.GetBool("Die"))
		{
			JumpProcess(om);
		}

		if (mFloor!=null&&!inv)
        {
			mOb.ani.SetBool("Hit", true);
			mOb.curhp -= 100;
			inv = true;
        }
		
		
		if(mOb.getPos().y <= mOb.floor1y)
		{
			mFloor = null;
			mOb.ani.SetBool("Jump", false);
			jumpState = 0;
			this.transform.position = new Vector3(transform.position.x, mOb.floor1y, transform.position.z);
			waitMove = false;

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
				transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
				oldPos = mOb.getPos();
				mOb.ani.SetBool("Jump", true);
			}
			else if (Input.GetKeyDown(KeyCode.Space))
			{
				maxJump = transform.position.y + 1.5f;
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
				mOb.ani.SetBool("Jump", true);
				transform.position = new Vector3(transform.position.x, transform.position.y + jumppower*Time.deltaTime, transform.position.z);
				if (endjump >= maxJump)
				{
					jumpState++;
					
				}
				break;
			case 11:

				if (mOb.righ == "Right")
				{
					transform.position = new Vector3(transform.position.x+(jumppower*2 * Time.deltaTime), transform.position.y + (jumppower * Time.deltaTime),transform.position.z);
					mOb.ani.SetBool("Right", true);
					
				}
                else
                {
					transform.position = new Vector3(transform.position.x - (jumppower*2 * Time.deltaTime), transform.position.y+ (jumppower* Time.deltaTime), transform.position.z);
					mOb.ani.SetBool("Right", false);
					
				}
				mOb.ani.SetBool("Jump", true);
				waitMove = true;
				if (endjump >= maxJump)
				{
					jumpState++;
				}
					break;

			case 2:
			case 12:
			case 13:
				mOb.ani.SetBool("Jump", true);
                if (jumpState == 12)
                {
					if (mOb.righ == "Right")
					{
						transform.position = new Vector3(transform.position.x + (jumppower*Time.deltaTime * 2), transform.position.y , transform.position.z);
					}
                    else
                    {
						transform.position = new Vector3(transform.position.x - (jumppower*Time.deltaTime * 2), transform.position.y , transform.position.z);
					}
				}
				
				JumpDown(om);
				break;
			default:
				Debug.Log("나는 무엇인가...");
				break;

        }

	}
	
	public void fallDown(Om om)
	{
	   
		Debug.Log("FallDown(om)");
		transform.position = new Vector3(transform.position.x, transform.position.y - (jumppower*3*Time.deltaTime), transform.position.z);
		

		
		List<ObjBase> fos = om.find2fG(mOb.getFoot().x, mOb.getFoot().y, mOb.getFoot(oldPos).y);
	   
		if (fos.Count > 0)
		{
			Vector3 pos = mOb.getPos();
		  
			//mFloor = fos[0];
			FloorChange(fos[0]);
			
			transform.position = new Vector3(pos.x, fos[0].getPos().y + fos[0].getRadius().y , pos.z);
			waitMove = false;
		
		   
			
	   }
        else
        {
			mFloor=null;
        }
		Debug.Log(fos.Count);
		
		

	}
	public void JumpDown(Om om)
	{
		
		
			
		fallDown(om);
		
	   
		
	}
	public void FloorChange(ObjBase floor)
	{
		mOb.ani.SetBool("Jump", false);
		mFloor = floor;
		jumpState = 0;
		

	}
	IEnumerator UnBeatTime()
	{

		for (int i = 0; i < 10; ++i)
		{
			if (i % 2 == 0)
				mOb.sprite.color = new Color32(255, 255, 255, 90);
			else
				mOb.sprite.color = new Color32(255, 255, 255, 180);

			yield return new WaitForSeconds(0.05f);
		}

		//Alpha Effect End 
		mOb.sprite.color = new Color32(255, 255, 255, 255);


		
		yield return null;




	}

	
	private void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.LeftControl))
		{
			GameObject hitbox = mOb.GetHitBox();
			mOb.ani.SetBool("Attack", true);
		}
		if (mOb.ani.GetFloat("Debuff") != 1)
        {
			debufftime += Time.deltaTime;
		}
        if (debufftime > 5f)
        {
			mOb.speed *= 2;
			mOb.ani.SetFloat("Debuff", 1);
			debufftime = 0;
		}
		
			Move();


		
		if (mOb.ani.GetBool("Hit"))
        {
			StartCoroutine(UnBeatTime());
        }
		
		if (mOb.time > 0.2f)
		{
			
			mOb.Hitbox.SetActive(false);
			
			mOb.time = 0;

		}
        if (Damagetime > 0.3)
        {
			StopAllCoroutines();
			mOb.sprite.color = new Color32(255, 255, 255, 255);
			inv = false;
			Damagetime = 0;
			
			
        }
		

	}
	
	public void Move()
	{
		
        if (waitMove == true||attacking)
        {
			return;
        }
		vector = Vector3.zero;
		if (Input.GetAxisRaw("Horizontal") != 0)
		{
			vector = (Input.GetAxisRaw("Horizontal") > 0) ? Vector3.right : Vector3.left;
			mOb.righ = (Input.GetAxisRaw("Horizontal") > 0) ? "Right" : "Left";
			this.transform.localScale=(Input.GetAxisRaw("Horizontal") > 0)?new Vector3(-1,this.transform.localScale.y,this.transform.localScale.z):new Vector3(1, this.transform.localScale.y, this.transform.localScale.z);
			transform.position += vector * mOb.speed * Time.deltaTime;
			mOb.ani.SetBool("Walking", true);
		}
        else
        {
			mOb.ani.SetBool("Walking", false);
		}

		if (jumpState==0&&mFloor != null)
		{
			
			if ((mFloor.getPos().x + mFloor.getRadius().x/2) <= transform.position.x ||(mFloor.getPos().x - mFloor.getRadius().x/2) >= transform.position.x)
			{
				Debug.Log("ad");
				jumpState = 2;
			}
		}



	}



	
	
}