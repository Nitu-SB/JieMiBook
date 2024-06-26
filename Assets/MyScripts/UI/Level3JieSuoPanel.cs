using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Level3JieSuoPanel : UIBase
{
    public bool isLock = true;
    public string answerNum;
    private Button closeBtn;
    private string myAnswer;
    public GameObject wrongTip;
    List<InputField> inputFields = new List<InputField>();

    public Transform targetIMGList;

    public GameObject nextObj;

    bool canNext = false;
    // Start is called before the first frame update
    void Start()
    {
        closeBtn = transform.Find("Bg/CloseBtn").GetComponent<Button>();
        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => {
            CloseUIPanel();
        });

        Transform imageTrans = transform.Find("Bg/Image");
        inputFields.Clear();
        foreach (Transform child in imageTrans)
        {
            inputFields.Add(child.GetComponent<InputField>());
            child.GetComponent<InputField>().onValueChanged.AddListener(Check);
        }
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
    public override void OpenUIPanel()
    {
        base.OpenUIPanel();
        for (int i = 0; i < inputFields.Count; i++)
        {
            inputFields[i].text = "";
        }
    }
    void Check(string str)
    {
        myAnswer = "";
        for (int i = 0; i < inputFields.Count; i++)
        {
            myAnswer += inputFields[i].text;
        }

        if (myAnswer == answerNum)
        {
            //CloseUIPanel();
            //transform.parent.Find("BaseBookBtn/XingBtn").gameObject.SetActive(true);
            //transform.parent.Find("Base/Lock").gameObject.SetActive(false);
            //transform.parent.Find("Base/UnLock").gameObject.SetActive(true);
            foreach(Transform child in targetIMGList)
            {
                child.GetComponent<Image>().DOFade(0, 2f);
            }

            DOVirtual.DelayedCall(2f, () => {
                canNext = true;
            });
        }
        else
        {
            if(myAnswer.Length == 4)
            {
                wrongTip.gameObject.SetActive(true);
                DOVirtual.DelayedCall(1, () => { wrongTip.gameObject.SetActive(false); });
            }
          
        }
    }
}
