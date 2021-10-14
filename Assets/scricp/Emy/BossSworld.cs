using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossSworld : MonoBehaviour
{
    float Rtime;
    float h = 5f;
    float w = 5f;
    bool ok = false;
    float e = 0f;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Rtime += Time.deltaTime;
        int ran = Random.Range(0, 1);
       
      
        if (Rtime > 1.0f)
        {
            w = Random.Range(1f, 8f);
            h = Random.Range(1f, 8f);
            switch (ran)
            {
                
                case 0:
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    e=this.transform.position.x;
                   
                    this.transform.position = new Vector3(transform.position.x - Time.deltaTime, transform.position.y, 0);
                       
                        
                        
                    
                    
                    Rtime = 0;
                    e = 0f;
                    ok = false;
                    break;

                case 1:
                    this.transform.rotation = Quaternion.Euler(0, 0, 45);
                        Rtime = 0;
                    break;


                case 2:
                    this.transform.rotation = Quaternion.Euler(0, 0, 90);
                    Rtime = 0;
                    break;
                case 3:
                    this.transform.rotation = Quaternion.Euler(0, 0, 135);
                    Rtime = 0;
                    break;

                case 4:
                    this.transform.rotation = Quaternion.Euler(0, 0, 180);
                    Rtime = 0;
                    break;

                case 5:
                    this.transform.rotation = Quaternion.Euler(0, 0, 225);
                    Rtime = 0;
                    break;

                case 6:
                    this.transform.rotation = Quaternion.Euler(0, 0, 270);
                    Rtime = 0;
                    break;
                case 7:
                    this.transform.rotation = Quaternion.Euler(0, 0, 315);
                    Rtime = 0;
                    break;

                default:
                    Debug.Log("오류");
                    break;
            }
        }
      
    }
}
