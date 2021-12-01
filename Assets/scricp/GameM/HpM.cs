using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpM : MonoBehaviour
{
    public Image hpImage;
    public float hp;
    public Player player;
    public Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        hpImage = transform.Find("CurHp").GetComponent<Image>();
        hpText = transform.Find("HpBarT").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        checkHP();
        
    }
    void checkHP()
    {
        hp = player.mOb.curhp;
        hpImage.fillAmount = hp / player.mOb.maxhp;
        hpText.text = string.Format("Hp:{0}/{1}", hp, player.mOb.maxhp);
    }
}
