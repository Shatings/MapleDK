using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpM : MonoBehaviour
{
    public Image expImage;
    public float exp;
    public Player player;
    public Text expText;
    public Text levelT;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        expImage = transform.Find("CurExp").GetComponent<Image>();
        expText = GameObject.Find("ExpBarT").GetComponent<Text>();
        levelT = GameObject.Find("Level").GetComponent<Text>();
        

    }

    // Update is called once per frame
    void Update()
    {
        checkSc();
    }
    void checkSc()
    {
        score.text = "점수:" + Object.FindObjectOfType<GameMgr>().ScoreT().ToString();
        levelT.text = "레벨:" + player.level.ToString();
        exp = player.curexp;
        expImage.fillAmount = exp / player.maxexp;
        expText.text = string.Format("Exp:{0}/{1}", exp, player.maxexp);
    }
}
