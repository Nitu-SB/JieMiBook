using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ImageShow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
        GetComponent<Image>().DOFade(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
