using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TuoZhuaiPanel : MonoBehaviour
{
    public Transform slotListTrans;
    public Transform rightImageTrans;

    public GameObject nextObj;

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
        bool isCorrect = true;
        foreach(Transform child in slotListTrans)
        {
            if(child.GetComponent<TuoZhuaiSlot>().myItem == "")
            {
                isCorrect = false;

                return;
            }
            if(child.name.Substring(child.name.Length-1,1)!= child.GetComponent<TuoZhuaiSlot>().myItem.Substring(child.GetComponent<TuoZhuaiSlot>().myItem.Length - 1, 1))
            {
                isCorrect = false;
                //break;
            }
        }

        if (isCorrect)
        {
            //继续
            Debug.Log("拖拽提正确");
            ShowImage();
        }
        else
        {
            wrongTip.gameObject.SetActive(true);
            DOVirtual.DelayedCall(1, () => { wrongTip.gameObject.SetActive(false); });
        }
    }
    public void ShowImage()
    {
        Image img0, img0_c, img1, img1_c, img2, img2_c;
        img0 = rightImageTrans.GetChild(0).GetComponent<Image>();
        img0_c = rightImageTrans.GetChild(0).GetChild(0).GetComponent<Image>();
        img1 = rightImageTrans.GetChild(2).GetComponent<Image>();
        img1_c = rightImageTrans.GetChild(2).GetChild(0).GetComponent<Image>();
        img2 = rightImageTrans.GetChild(1).GetComponent<Image>();
        img2_c = rightImageTrans.GetChild(1).GetChild(0).GetComponent<Image>();

        img0.DOFade(0, 1.5f);
        img0_c.DOFade(1, 1.5f).OnComplete(() => {
            img1.DOFade(0, 1.5f);
            img1_c.DOFade(1, 1.5f).OnComplete(() => {
                img2.DOFade(0, 1.5f);
                img2_c.DOFade(1, 1.5f);

                DOVirtual.DelayedCall(2, () => {
                    nextObj.gameObject.SetActive(true);
                });
            });
        });
    }
}
