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
    //����Э�����ͷ���loadlevel
    {
        //Background.SetActive(true);
        GameObject.Find("Common/Image").GetComponent<Image>().DOFade(1, 1f);
        yield return new WaitForSeconds(1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //���ر�������ֵ +1�ĳ���SampleScene
        operation.allowSceneActivation = false;
        //�Ȳ�������һ����
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
