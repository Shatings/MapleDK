using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HitM : MonoBehaviour
{
    public float movespeed=5;
    [SerializeField]
    TextMeshPro text;
    public Color apar;
    public float cloarSpeed=5;
    public float deatime=0.1f;
    
    public int Dange;
    
    public void DangeT(ObjBase ob)
    {
        
        Debug.Log(text);

        apar = text.color;
        text.text = ob.attackp.ToString();
        apar = new Color(0, 255, 255, 255);
        Debug.Log("색깔:"+apar);
        Debug.Log(text.text);
        
        

    }
    public void Start()
    {
        text = GetComponent<TextMeshPro>();
        Invoke("DestroyOb", deatime);
    }
    void Update()
    {
        transform.Translate(new Vector3(0, movespeed * Time.deltaTime, 0));
        apar.a = Mathf.Lerp(apar.a, 0, Time.deltaTime * cloarSpeed);
        text.color = apar;
    }
    private void DestroyOb()
    {
        Destroy(gameObject);
    }
}
