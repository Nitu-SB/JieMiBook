using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LogPanel : UIBase
{
    public GameObject closeObj,openObj;
    private List<string> textList = new List<string>();
    Transform testParent;
    // Start is called before the first frame update
    void Start()
    {
        testParent = transform.Find("Bg/TextList");
        foreach (Transform child in testParent)
        {
            textList.Add(child.GetComponent<Text>().text);
            child.GetComponent<Text>().text = "";
        }
        OpenUIPanel();
    }
    public override void OpenUIPanel()
    {
        transform.GetChild(0).localScale = Vector3.zero;
        transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        transform.GetChild(0).gameObject.SetActive(true);
        AudioManager.instance.PlayEffectAudio("Pop");
        transform.GetChild(0).DOScale(1, 0.2f).SetEase(Ease.OutQuart).OnComplete(() => {
            transform.GetChild(0).GetComponent<Image>().DOFade(0.5f, 1);
        });
        StartCoroutine(ShowTextList());
    }
    public override void CloseUIPanel()
    {
        openObj.gameObject.SetActive(true);
        closeObj.gameObject.SetActive(false);

        transform.GetChild(0).gameObject.SetActive(false);

        
    }
    IEnumerator ShowTextList()
    {
        AudioManager.instance.PlayEffectAudio("Write",true);
        for (int i = 0; i < textList.Count; i++)
        {
            testParent.GetChild(i).GetComponent<Text>().DOText(textList[i], textList[i].Length / 20f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(textList[i].Length / 20f);
        }
        AudioManager.instance.StopEffectAudio();
        yield return new WaitForSeconds(4f);
        CloseUIPanel();
    }
}
