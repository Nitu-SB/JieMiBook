using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuoZhuaiSlot : MonoBehaviour
{
    public string myItem;
    public TuoZhuaiPanel tuoZhuaiPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMyItem(string itemName)
    {
        myItem = itemName;
        tuoZhuaiPanel.Check();
    }
}
