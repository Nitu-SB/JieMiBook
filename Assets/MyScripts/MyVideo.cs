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
        DOVirtual.DelayedCall(18.5f, () => {
            closeObj.gameObject.SetActive(false);
            nextObj.gameObject.SetActive(true);
            AudioManager.instance.BGM.Play();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
