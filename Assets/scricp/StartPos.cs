using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPos : MonoBehaviour
{
    public float successRate;
    public float fallRate;
    public float destroyRate;
    public int suc;
    public int fall;
    public int count=0;
    public int destory = 0;
    public int starcount = 0;
    public int[] starpos = new int[26];
    public int maxstar = 0;
    // Start is called before the first frame update
    void Start()
    {
        successRate = 95.0f;
        fallRate = 5.0f;
        destroyRate = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if (starcount < 300000000)
        //{
        //    Yongin();
        //}
    }
    public void Yongin(Item _item)
    {
        int rate = Random.Range(1, 101);
        switch (_item.startpos) {
            case 12:
                _item.destroyRate = 0.6f;
                break;
            case 13:
                _item.destroyRate = 1.3f;
                break;
            case 14:
                _item.destroyRate = 1.4f;
                break;
            case 15:
            case 16:
            case 17:
                _item.destroyRate = 2.1f;
                break;
            case 18:
            case 19:
                _item.destroyRate = 2.8f;
                break;
            case 20:
            case 21:
                _item.destroyRate = 7.0f;
                break;
            case 22:
                _item.successRate = 3.0f;
                _item.destroyRate = 19.40f;
                break;
            case 23:
                _item.successRate = 2.0f;
                _item.destroyRate = 23.05f;
                break;
            case 24:
                _item.successRate = 1.0f;
                _item.destroyRate = 24.07f;
                break;
            case 25:
                maxstar++;
                break;
            default:
                destroyRate = 0;
                break;

        }
        
      
        
        if (rate < _item.successRate)
        {
            Debug.Log("성공");
            Debug.Log(" "+rate);
            suc++;
            _item.startpos++;
            if (_item.startpos != 3&& _item.startpos < 15)
            {
                Debug.Log("확률방지");
                _item.successRate -= 5;
            }
           
            
            
        }
        else if(rate>destroyRate&&rate>successRate)
        {
            Debug.Log("실패");
            Debug.Log(" "+rate);
            fall++;

            
                if (_item.startpos < 15&& _item.startpos > 10 && _item.startpos % 5 != 0)
                {
                    Debug.Log("하락방지");
                     _item.startpos--;
                     _item.successRate += 5;
                }
            
        }
        else
        {
            Debug.Log("파괴");
            destory++;
            _item.startpos = 12;
            _item.successRate = 45;
        }
        Debug.Log("성공회수:" + suc + "실패회수:" + fall+"파괴회수: "+destory);
        Debug.Log("스타포스:"+_item.startpos);
        Debug.Log("성공확률:" + _item.successRate);
        starpos[count]++;
        starcount++;
    }


}
