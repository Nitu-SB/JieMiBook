using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class YaoShuiItem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    Vector3 oriPos;
    public Transform topTrans;
    private Transform originTrans;
    // Start is called before the first frame update
    void Start()
    {
        oriPos = transform.position;
        originTrans = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReSetPos()
    {
        
        transform.position = oriPos;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.gameObject.SetActive(true);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.GetComponent<Image>().raycastTarget = false;
        transform.SetParent(topTrans);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
         transform.SetParent(originTrans);
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        if (go.name.Contains("Guo"))
        {
            AudioManager.instance.PlayEffectAudio("Potion");
            transform.gameObject.SetActive(false);
            go.GetComponent<Guo>().AddYaoShui(this.gameObject.name.Substring(0, 1));
        }
        transform.GetComponent<Image>().raycastTarget = true;
    }

    
}
