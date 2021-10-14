using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Invetory : MonoBehaviour
{
    public List<Item> items;
    //public List<item> items;
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Solt[] slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = slotParent.GetComponentsInChildren<Solt>();
    }
    public void Freshlot()
    {

        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
            slots[i].itemId = items[i].id;
            Debug.Log(" " + slots[i].itemId);


        }

        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }
    public void AddItem(Item _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item);
            Freshlot();

        }
        else
        {
            Debug.Log("가득 찼습니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
