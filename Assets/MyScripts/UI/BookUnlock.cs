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
            text0.DOText("To��ϰħŮ����С��", 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                text1.DOText("<color=#FFFFFF00>��</color>������Ƿ�ʱ�������Լ������Ƿ�ѹ��̫��������˯�����Ƿ�Թ����е���룿\n<color=#FFFFFF00>��</color>��ϲ�㱻ѡ���������мƻ������˶����ֳ�����������е���������һ���λ��ó�������һ��ȹ�һ�������ʱ��ɣ�", 4).SetEase(Ease.Linear).OnComplete(() =>
                {
                    text2.DOText("�ڴ����������~\n<color=#FFFFFF00>��</color>��Ⱦ��ϡ���", 1.5f).SetEase(Ease.Linear).OnComplete(() =>
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
