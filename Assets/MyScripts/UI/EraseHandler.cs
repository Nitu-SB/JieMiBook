using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(RawImage))]
public class EraseHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public GameObject closeObj,nectObj;
    public GameObject img;


    //��ˢ�뾶
    [SerializeField] int brushRadius = 50;

    //���������������������ڸ�ֵ����Ϊ������ɣ��Զ�����ʣ�ಿ��
    [SerializeField] float finishPercent = 0.9f;

    //������ƫ����,�����ϸ�������>=��ֵʱ��ʼ�µĲ�����
    [SerializeField] float drawOffset = 10f;

    //�Ƿ��Բ������
    public bool isFinish;

    //Ҫ������ͼƬ
    RawImage eraseImage;
    Texture2D eraseTexture;
    //ͼƬ����
    int textureWidth;
    int textureHeight;

    //ͼƬ��С
    float textureLength;
    //��������ͼƬ��С
    float eraseLength;

    Camera mainCamera;

    void Awake()
    {
        eraseImage = GetComponent<RawImage>();
        mainCamera = Camera.main;
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        isFinish = false;
        eraseLength = 0;

        //ԭ����ͼƬ
        //Texture2D originalTexture = (Texture2D)eraseImage.mainTexture;
        Texture2D originalTexture = (Texture2D)eraseImage.texture;

        //��������ͼƬ��չʾ��������
        eraseTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.ARGB32, false);
        textureWidth = eraseTexture.width;
        textureHeight = eraseTexture.height;
        eraseTexture.SetPixels(originalTexture.GetPixels());
        eraseTexture.Apply();

        Debug.Log(textureWidth + " - " + textureHeight);

        eraseImage.texture = eraseTexture;

        textureLength = eraseTexture.GetPixels().Length;
    }

    #region Pointer Event

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isFinish)
            return;
        //Debug.Log("����");
        tempLastPoint = eventData.position;
        ErasePoint(eventData.position);
        AudioManager.instance.PlayEffectAudio("End",true);
    }

    Vector2 tempEventPoint;
    Vector2 tempLastPoint;
    public void OnDrag(PointerEventData eventData)
    {
        if (isFinish)
            return;
        //Debug.Log("Drag");
        tempEventPoint = eventData.position;

        //�����ϸ������� >= ��ֵʱ��ʼ�µĲ�����
        if ((tempEventPoint - tempLastPoint).sqrMagnitude < drawOffset * drawOffset)
            return;

        //������
        ErasePoint(tempEventPoint);
        //��¼��
        tempLastPoint = tempEventPoint;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isFinish)
            return;
        //Debug.Log("Up");
        ErasePoint(eventData.position);
        AudioManager.instance.StopEffectAudio();
    }

    #endregion

    Vector3 tempWorldPoint;
    Vector3 tempLocalPoint;
    Vector2Int pixelPos;
    void ErasePoint(Vector2 screenPos)
    {
        //Debug.Log("ErasePoint");
        //���λ������ת�������������Ч�����Ÿĳ�ƽ�������һ��
        tempWorldPoint = mainCamera.ScreenToWorldPoint(/*screenPos*/Input.mousePosition);
        tempLocalPoint = transform.InverseTransformPoint(Input.mousePosition);

        //���ͼƬ���ص�����
        pixelPos.x = (int)tempLocalPoint.x + textureWidth / 2;
        pixelPos.y = (int)tempLocalPoint.y + textureHeight / 2;
       // Debug.Log(pixelPos);
        //���λ���Ƿ���ͼƬ��Χ��
        if (pixelPos.x < 0 || pixelPos.x >= textureWidth || pixelPos.y < 0 || pixelPos.y >= textureHeight)
            return;

        //������ˢ����Χ�����ص�
        for (int i = -brushRadius; i <= brushRadius; i++)
        {
            //����/�ұ߽�
            if (pixelPos.x + i < 0 || pixelPos.x + i >= textureWidth)
                continue;

            for (int j = -brushRadius; j <= brushRadius; j++)
            {
                //����/�±߽�
                if (pixelPos.y + j < 0 || pixelPos.y + j >= textureHeight)
                    continue;

                //�Ƿ���Բ�η�Χ��
                if (Mathf.Pow(i, 2) + Mathf.Pow(j, 2) > Mathf.Pow(brushRadius, 2))
                    continue;

                //���ص�ɫֵ
                Color color = eraseTexture.GetPixel(pixelPos.x + i, pixelPos.y + j);

                //�ж�͸����,�Ƿ��Ѳ���
                if (Mathf.Approximately(color.a, 0))
                    continue;

                //�޸����ص�͸����
                color.a = 0;
                eraseTexture.SetPixel(pixelPos.x + i, pixelPos.y + j, color);

                //��������ͳ��
                eraseLength++;
            }
        }
        eraseTexture.Apply();
        //Debug.Log("asdasdasdasdasdas");
        //�жϲ�������
        RefreshErasePercent();
    }

    float tempPercent;
    void RefreshErasePercent()
    {
        if (isFinish)
            return;

        tempPercent = eraseLength / textureLength;

        tempPercent = (float)Math.Round(tempPercent, 2);

        Debug.Log("�����ٷֱ� : " + tempPercent);

        if (tempPercent >= finishPercent)
        {
            isFinish = true;

            eraseImage.enabled = false;

            //���������¼�
            DOVirtual.DelayedCall(0.5f, () => {
                img.GetComponent<Animator>().enabled = true;
                AudioManager.instance.PlayEffectAudio("Walk", true);
                DOVirtual.DelayedCall(8, () => {
                    AudioManager.instance.Effect.Stop();
                    AudioManager.instance.BGM.Stop();
                    nectObj.gameObject.SetActive(true);
                    closeObj.gameObject.SetActive(false);
                    
                });
                
            });
        }
    }

}