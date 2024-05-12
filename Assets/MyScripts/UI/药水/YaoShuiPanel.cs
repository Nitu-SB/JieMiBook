using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YaoShuiPanel : MonoBehaviour
{
    public GameObject nextObj;
    bool canNext = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canNext)
        {
            if (Input.GetMouseButtonDown(0))
            {
                nextObj.gameObject.SetActive(true);
            }
        }
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
        canNext = true;
    }
}
