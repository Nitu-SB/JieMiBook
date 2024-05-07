using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TextShow : MonoBehaviour
{
    public float showSpeed = 30f;
    string myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>().text;
        GetComponent<Text>().text = "";
        AudioManager.instance.PlayEffectAudio("Write", true);
        GetComponent<Text>().DOText(myText, myText.Length / showSpeed).SetEase(Ease.Linear).OnComplete(()=> {
            AudioManager.instance.StopEffectAudio();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
