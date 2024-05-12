using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class BookUnlock : MonoBehaviour
{
    public GameObject leftPage, rightPage;
    public Text text0, text1, text2, text3;

    public GameObject nextObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool canNext = false;
    private void OnEnable()
    {
        leftPage.transform.localPosition = Vector3.zero;
        rightPage.transform.localPosition = Vector3.zero;
        leftPage.transform.DOLocalMoveX(-347, 1);
        rightPage.transform.DOLocalMoveX(345, 1).OnComplete(()=> {
            AudioManager.instance.PlayEffectAudio("Write",true);
            text0.DOText("To见习魔女莉莉小姐", 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                text1.DOText("<color=#FFFFFF00>空</color>您最近是否时常怀疑自己？您是否压力太大难以入睡？您是否对工作感到厌倦？\n<color=#FFFFFF00>空</color>恭喜你被选中罗纳旅行计划的幸运儿！现诚邀您逃离城市的喧嚣开启一段梦幻旅程与我们一起度过一段奇妙的时光吧！", 4).SetEase(Ease.Linear).OnComplete(() =>
                {
                    text2.DOText("期待着与你相见~\n<color=#FFFFFF00>空</color>丘比敬上——", 1.5f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        text3.DOText("2000.3.5", 0.5f).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            AudioManager.instance.StopEffectAudio();
                            canNext = true;
                            //DOVirtual.DelayedCall(2, () =>
                            //{
                            //    nextObj.gameObject.SetActive(true);
                            //});
                        });
                    });
                });
            });
        });
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
}
