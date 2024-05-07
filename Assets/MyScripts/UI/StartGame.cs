using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class StartGame : MonoBehaviour
{
    public int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => {
            StartCoroutine(loadlevel(sceneIndex));
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator loadlevel(int sceneIndex)
    //设置协程类型方法loadlevel
    {
        //Background.SetActive(true);
        GameObject.Find("Common/Image").GetComponent<Image>().DOFade(1, 1f);
        yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //加载本场景数值 +1的场景SampleScene
        operation.allowSceneActivation = false;
        //先不加载下一场景
        while (!operation.isDone)
        { 
            if (operation.progress >= 0.9f)
            {
                
                operation.allowSceneActivation = true;
                DOVirtual.DelayedCall(1, () => {
                    GameObject.Find("Common/Image").GetComponent<Image>().DOFade(0, 1f);
                });
               

            }
            yield return null;
        }
        
        yield return null;
    }
}
