using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class CameraPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform targetTrans;
    Vector3 targetScale;
    public Image write;
    public GameObject closeObj;
    public GameObject nectObj;
    public GameObject wrongTip;
    void Start()
    {
        targetScale = targetTrans.localScale;
    }
    bool canNext = false;
    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetScale+= Vector3.one * scroll*0.3f;
        float x = targetScale.x;
        x = Mathf.Clamp(x, 0.5523f, 1.22f);
        targetScale = Vector3.one * x;
        targetTrans.transform.localScale = Vector3.Lerp(targetTrans.transform.localScale, targetScale, 4*Time.deltaTime);

        if (canNext)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.gameObject.SetActive(false);
                closeObj.gameObject.SetActive(false);
                nectObj.gameObject.SetActive(true);
            }
        }
    }

    public void Check()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        bool isHigh = false;
        foreach (RaycastResult result in results)
        {
            if(result.gameObject.name == "High")
            {
                isHigh = true;
                AudioManager.instance.PlayEffectAudio("Photo");
                write.color = new Color(1, 1, 1, 0);
                write.DOFade(1, 0.05f).SetEase(Ease.OutQuart).OnComplete(()=> {
                    write.DOFade(0, 0.3f).SetEase(Ease.OutQuart).OnComplete(() => {
                        DOVirtual.DelayedCall(0.5f, () =>
                        {
                            canNext = true;
                            //this.gameObject.SetActive(false);
                            //closeObj.gameObject.SetActive(false);
                            //nectObj.gameObject.SetActive(true);
                            
                        });
                     });
                });
            }
        }
        if (!isHigh)
        {
            wrongTip.gameObject.SetActive(true);
            DOVirtual.DelayedCall(1, () => { wrongTip.gameObject.SetActive(false); });
        }
    }
}
