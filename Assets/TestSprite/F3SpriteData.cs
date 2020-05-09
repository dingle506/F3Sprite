using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class F3SpriteData
{
    public string name;
    public int x, y;            //u, v
    public int width, height;

    public int bordLeft, bordRight, bordTop, bordBottom;
    public int paddingLeft, paddingRight, paddingTop, paddingBottom;

    public bool hasBorder { get { return (bordLeft | bordRight | bordTop | bordBottom) != 0; } }
    public bool hasPadding { get { return (paddingLeft | paddingRight | paddingTop | paddingBottom) != 0; } }

    public void SetRect(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public void SetPadding(int left, int right, int top, int bottom)
    {
        this.paddingLeft = left;
        this.paddingRight = right;
        this.paddingTop = top;
        this.paddingBottom = bottom;
    }

    public void SetBorad(int left, int right, int top, int bottom)
    {
        this.bordLeft = left;
        this.bordRight = right;
        this.bordTop = top;
        this.bordBottom = bottom;
    }

    public Vector2 UV0(Rect rect)
    {
        return new Vector2(x / rect.width, y / rect.height);
    }

    public Vector2 UV1(Rect rect)
    {
        return new Vector2((x + width) / rect.width, y / rect.height);
    }

    public Vector2 UV2(Rect rect)
    {
        return new Vector2(x / rect.width, (y + height) / rect.height);
    }

    public Vector2 UV3(Rect rect)
    {
        return new Vector2((x + width) / rect.width, (y + height) / rect.height);
    }
}