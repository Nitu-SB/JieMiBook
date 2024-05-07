using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public abstract class UIBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void  OpenUIPanel()
    {
        transform.GetChild(0).transform.localScale = Vector3.zero;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuart);
    }

    public virtual void CloseUIPanel()
    {
        transform.GetChild(0).transform.localScale = Vector3.one;
        transform.GetChild(0).transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuart).OnComplete(()=> {
            transform.GetChild(0).gameObject.SetActive(false);
        });
    }
}
