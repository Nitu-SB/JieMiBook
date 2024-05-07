using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YaoShuiPanel : MonoBehaviour
{
    public GameObject nextObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReSetPanel()
    {
        Transform yaoshuiList = transform.Find("YaoShui/YaoShuiList");
        foreach(Transform child in yaoshuiList)
        {
            child.GetComponent<YaoShuiItem>().ReSetPos();
        }
    }
    public void NextPage()
    {
        nextObj.gameObject.SetActive(true);
    }
}
