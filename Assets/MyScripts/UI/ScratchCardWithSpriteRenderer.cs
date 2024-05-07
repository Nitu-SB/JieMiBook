using UnityEngine;
using UnityEngine.UI;

public class ScratchCardWithSpriteRenderer : MonoBehaviour
{
    // 公开背景和遮罩的Sprite Renderer组件
    public SpriteRenderer backgroundRenderer;
    public SpriteRenderer maskRenderer;

    // 遮罩原图的副本，用于保存未刮开状态
    private Texture2D originalMaskTexture;

    // 刮擦笔刷大小
    public float brushSize = 50f;

    void Start()
    {
        // 获取遮罩原图纹理并保存副本
        Texture2D maskTexture = maskRenderer.sprite.texture as Texture2D;
        originalMaskTexture = new Texture2D(maskTexture.width, maskTexture.height);
        originalMaskTexture.SetPixels(maskTexture.GetPixels());
        originalMaskTexture.Apply();
    }

    void Update()
    {
        // 当鼠标左键按下时
        if (Input.GetMouseButtonDown(0))
        {
            // 获取当前鼠标屏幕坐标
            Vector3 mousePosition = Input.mousePosition;

            // 转换为世界坐标，并投影到遮罩renderer所在的平面
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("笔触")))
            {
                Vector2 localPoint = hit.textureCoord;
                ApplyScratch(localPoint);
            }
        }
    }

    void ApplyScratch(Vector2 touchPosition)
    {
        // 获取当前遮罩的Texture2D
        Texture2D currentMaskTexture = new Texture2D(maskRenderer.sprite.texture.width, maskRenderer.sprite.texture.height);
        currentMaskTexture.SetPixels(maskRenderer.sprite.texture.GetPixels());

        // 计算刮擦矩形区域
        int xMin = Mathf.FloorToInt(touchPosition.x * currentMaskTexture.width - brushSize / 2);
        int xMax = Mathf.CeilToInt(touchPosition.x * currentMaskTexture.width + brushSize / 2);
        int yMin = Mathf.FloorToInt(touchPosition.y * currentMaskTexture.height - brushSize / 2);
        int yMax = Mathf.CeilToInt(touchPosition.y * currentMaskTexture.height + brushSize / 2);

        // 限制刮擦区域在纹理内
        xMin = Mathf.Clamp(xMin, 0, currentMaskTexture.width);
        xMax = Mathf.Clamp(xMax, 0, currentMaskTexture.width);
        yMin = Mathf.Clamp(yMin, 0, currentMaskTexture.height);
        yMax = Mathf.Clamp(yMax, 0, currentMaskTexture.height);

        // 对刮擦区域设置透明色
        for (int x = xMin; x < xMax; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                currentMaskTexture.SetPixel(x, y, Color.clear);
            }
        }

        // 应用刮擦结果
        currentMaskTexture.Apply();
        maskRenderer.sprite = Sprite.Create(currentMaskTexture, new Rect(0, 0, currentMaskTexture.width, currentMaskTexture.height), Vector2.one * 0.5f);
    }
}
