using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MingXingPianPanel : UIBase
{
    private Button closeBtn;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn = transform.Find("Bg/CloseBtn").GetComponent<Button>();
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => {
            CloseUIPanel();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
