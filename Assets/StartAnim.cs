using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StartAnim : MonoBehaviour
{
    public float delayTime = 3f;
    public float changeTime = 4f;
    public Transform book;
    public Transform cat;
    void Start()
    {
        //transform.localScale = Vector3.one * 1.749101f;
        //GetComponent<RectTransform>().offsetMax = new Vector2(0, 396);
        //GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -396);

        DOVirtual.DelayedCall(delayTime, () =>
        {
            DOTween.To(() => GetComponent<RectTransform>().offsetMax, a => GetComponent<RectTransform>().offsetMax = a, Vector2.zero, changeTime).SetEase(Ease.OutExpo);
            DOTween.To(() => GetComponent<RectTransform>().anchoredPosition, a => GetComponent<RectTransform>().anchoredPosition = a, Vector2.zero, changeTime).SetEase(Ease.OutExpo);
            transform.DOScale(Vector3.one, changeTime).SetEase(Ease.OutExpo).OnComplete(() =>
            {
                book.DOScale(1, 0.2f).SetEase(Ease.OutQuart);
                RectTransform rectCat = cat.GetComponent<RectTransform>();
                DOTween.To(() => rectCat.anchoredPosition, a => rectCat.anchoredPosition = a, new Vector2(-162, 37), 1.5f).SetEase(Ease.OutBack);
            });
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
