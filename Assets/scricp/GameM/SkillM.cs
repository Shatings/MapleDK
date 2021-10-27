using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillM : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text text;
    public float time;
    bool timecheck=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FiledSkill();
    }
    void FiledSkill()
    {
        if (FindObjectOfType<Summons>())
        {
            if (!timecheck)
            {
                time = FindObjectOfType<Summons>().deatime;
                timecheck = true;
            }
            text.gameObject.SetActive(true);
            image.gameObject.SetActive(true);
            Debug.Log(image.fillAmount);
            image.fillAmount -= (1 / FindObjectOfType<Summons>().deatime)*Time.deltaTime;
            time-= 1 * Time.deltaTime;
            text.text = " " + (Mathf.Round(time));
        }
        else
        {
            image.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            image.fillAmount = 1;
            timecheck = false;
        }
    }
}
