using UnityEngine;
using UnityEngine.UI;

public class ScratchCardWithSpriteRenderer : MonoBehaviour
{
    // �������������ֵ�Sprite Renderer���
    public SpriteRenderer backgroundRenderer;
    public SpriteRenderer maskRenderer;

    // ����ԭͼ�ĸ��������ڱ���δ�ο�״̬
    private Texture2D originalMaskTexture;

    // �β���ˢ��С
    public float brushSize = 50f;

    void Start()
    {
        // ��ȡ����ԭͼ�������渱��
        Texture2D maskTexture = maskRenderer.sprite.texture as Texture2D;
        originalMaskTexture = new Texture2D(maskTexture.width, maskTexture.height);
        originalMaskTexture.SetPixels(maskTexture.GetPixels());
        originalMaskTexture.Apply();
    }

    void Update()
    {
        // ������������ʱ
        if (Input.GetMouseButtonDown(0))
        {
            // ��ȡ��ǰ�����Ļ����
            Vector3 mousePosition = Input.mousePosition;

            // ת��Ϊ�������꣬��ͶӰ������renderer���ڵ�ƽ��
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("�ʴ�")))
            {
                Vector2 localPoint = hit.textureCoord;
                ApplyScratch(localPoint);
            }
        }
    }

    void ApplyScratch(Vector2 touchPosition)
    {
        // ��ȡ��ǰ���ֵ�Texture2D
        Texture2D currentMaskTexture = new Texture2D(maskRenderer.sprite.texture.width, maskRenderer.sprite.texture.height);
        currentMaskTexture.SetPixels(maskRenderer.sprite.texture.GetPixels());

        // ����β���������
        int xMin = Mathf.FloorToInt(touchPosition.x * currentMaskTexture.width - brushSize / 2);
        int xMax = Mathf.CeilToInt(touchPosition.x * currentMaskTexture.width + brushSize / 2);
        int yMin = Mathf.FloorToInt(touchPosition.y * currentMaskTexture.height - brushSize / 2);
        int yMax = Mathf.CeilToInt(touchPosition.y * currentMaskTexture.height + brushSize / 2);

        // ���ƹβ�������������
        xMin = Mathf.Clamp(xMin, 0, currentMaskTexture.width);
        xMax = Mathf.Clamp(xMax, 0, currentMaskTexture.width);
        yMin = Mathf.Clamp(yMin, 0, currentMaskTexture.height);
        yMax = Mathf.Clamp(yMax, 0, currentMaskTexture.height);

        // �Թβ���������͸��ɫ
        for (int x = xMin; x < xMax; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                currentMaskTexture.SetPixel(x, y, Color.clear);
            }
        }

        // Ӧ�ùβ����
        currentMaskTexture.Apply();
        maskRenderer.sprite = Sprite.Create(currentMaskTexture, new Rect(0, 0, currentMaskTexture.width, currentMaskTexture.height), Vector2.one * 0.5f);
    }
}
