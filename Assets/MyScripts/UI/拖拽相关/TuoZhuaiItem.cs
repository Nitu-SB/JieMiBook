using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TuoZhuaiItem : MonoBehaviour,IDragHandler,IEndDragHandler,IBeginDragHandler
{
    public Transform topTrans;
    private Transform originTrans;
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.GetComponent<Image>().raycastTarget = false;
        transform.SetParent(topTrans);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originTrans);
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        if (go.name.Contains("Slot"))
        {
            transform.position = go.transform.position;
            go.GetComponent<TuoZhuaiSlot>().SetMyItem(this.gameObject.name);
        }
        transform.GetComponent<Image>().raycastTarget = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        originTrans = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
