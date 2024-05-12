using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MyVideo : MonoBehaviour
{
    public GameObject closeObj,nextObj;
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(34f, () => {
            canNext = true;
        });
    }
    bool canNext = false;
    // Update is called once per frame
    void Update()
    {
        if (canNext)
        {
            if (Input.GetMouseButtonDown(0))
            {
                closeObj.gameObject.SetActive(false);
                nextObj.gameObject.SetActive(true);
                AudioManager.instance.BGM.Play();
            }
        }
    }
}
