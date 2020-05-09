using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class F3SpriteAltas : MonoBehaviour
{
    [SerializeField]
    public Texture2D text2d;
    [SerializeField]
    public List<F3SpriteData> list;


    public Rect rect;

#if UNITY_ANDROID
    public const TextureFormat kTextureFormat = TextureFormat.ARGB32;//android,ios的图片格式选择
#else
    public const TextureFormat kTextureFormat = TextureFormat.ARGB32;
#endif

#if UNITY_ANDROID
    public const RenderTextureFormat kRenderTextureFormat = RenderTextureFormat.ARGB32;//android,ios的图片RenderTextureFormat
#else
    public const RenderTextureFormat kRenderTextureFormat = RenderTextureFormat.ARGB32;
#endif

    public void Init(int width, int height)
    {
        text2d = new Texture2D(width, height, kTextureFormat, false, true);

        rect = new Rect(0, 0, width, height);
        list = new List<F3SpriteData>();
    }

    public void AddTexture(string name, Texture2D texture, ref int posx, ref int posy)
    {
        F3SpriteData f3SpriteData = new F3SpriteData();
        f3SpriteData.name = name;
        f3SpriteData.width = texture.width;
        f3SpriteData.height = texture.height;
        f3SpriteData.x = posx;
        f3SpriteData.y = posy;

        try
        {
            Graphics.CopyTexture(texture, 0, 0, 0, 0, texture.width, texture.height, text2d, 0, 0, f3SpriteData.x, f3SpriteData.y);
        }
        catch
        {
            Debug.Log(f3SpriteData.name + " == "+ f3SpriteData.x + " : " + f3SpriteData.y + " ::: " + f3SpriteData.width + " : " + f3SpriteData.height);
        }

        posx += texture.width;

        list.Add(f3SpriteData);
    }

    public void GetPos(Texture2D texture2D, ref int x, ref int y)
    {
        int minx = 0, miny = 0;
        int maxx = text2d.width, maxy = text2d.height;

        if(list.Count == 0)
        {
            x = 0; y = 0;
            return;
        }


        for (int i = 0; i < list.Count; i++)
        {
            if (x < minx || x < miny)
            {
                break;
            }

            if(x + texture2D.width >= maxx)
            {
                x = 0;
                F3SpriteData leftSprite = list.FindLast((tex) => { return tex.x == 0; });
                y = leftSprite.y + leftSprite.height;
            }
            else
            {
                F3SpriteData lastSprite = list[list.Count - 1];
                x = lastSprite.x + lastSprite.width;
                y = lastSprite.y;
            }

            if (y + texture2D.height >= maxy)
            {
                Debug.LogError("Max out");
                return;
            }
        }
    }

    public F3SpriteData GetSpriteData(string name)
    {
        for(int i = 0; i < list.Count;i++)
        {
            if (list[i].name == name)
            {
                return list[i];
            }
        }

        return null;
    }

}
