using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Guo : MonoBehaviour
{
    public string answer;
    public string myYaoShui;
    public YaoShuiPanel yaoShuiPanel;
    public GameObject wrongTip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Check()
    {
        if(answer == myYaoShui)
        {
            Debug.Log("正确的药水顺序");
            DOVirtual.DelayedCall(1, () => {
                yaoShuiPanel.NextPage();
            });
        }
        else
        {

            Debug.Log("错误的药水顺序");
            
            wrongTip.gameObject.SetActive(true);
            DOVirtual.DelayedCall(2, () => {
                wrongTip.gameObject.SetActive(false);
                ReSetMyYaoShui();
                yaoShuiPanel.ReSetPanel();
            });
        }
        
    }
    public void AddYaoShui(string yaoshuiID)
    {
        myYaoShui += yaoshuiID;
        if (myYaoShui.Length >= 5)
        {
            Check();
        }
    }
    public void ReSetMyYaoShui()
    {
        myYaoShui = "";
    }
}
