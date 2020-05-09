using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class F3Sprite : MonoBehaviour
{
    public F3RectTransform f3Rect;

    public Material spriteMat;

    private MeshFilter meshFilter;
    private MeshRenderer meshRender;

    public F3SpriteAltas altas;
    public string spriteName;

    public Vector2 uv1 = new Vector2(0f, 0f);
    public Vector2 uv2 = new Vector2(0f, 0.5f);
    public Vector2 uv3 = new Vector2(1f, 0f);
    public Vector2 uv4 = new Vector2(1f, 0.5f);

    private F3SpriteData spriteData;

    Mesh mesh;

    F3VertexHelper vertexHelper = new F3VertexHelper();

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        
        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
            meshRender = gameObject.AddComponent<MeshRenderer>();
            meshRender.sharedMaterial = spriteMat;
        }

        Fill();
    }

    private void OnValidate()
    {
        if (meshFilter && Application.isPlaying)
        {
            Fill();
        }
    }

    private void Fill()
    {
        if(mesh == null)
            mesh = new Mesh();

        meshFilter.mesh = mesh;

        float glWidth = f3Rect.rect.width / 2;
        float glHeight = f3Rect.rect.height / 2;

        vertexHelper.Clear();

        spriteData = altas.GetSpriteData(spriteName);

        AddVert(new Vector3(-glWidth, -glHeight, 0), Color.white, spriteData.UV0(altas.rect));
        AddVert(new Vector3(-glWidth, glHeight, 0), Color.white, spriteData.UV1(altas.rect));
        AddVert(new Vector3(glWidth, -glHeight, 0), Color.white, spriteData.UV2(altas.rect));
        AddVert(new Vector3(glWidth, glHeight, 0), Color.white, spriteData.UV3(altas.rect));

        AddTriangle(0, 1, 2);
        AddTriangle(2, 3, 1);

        Apply(mesh, vertexHelper);
    }

    void AddVert(Vector3 pos, Color color, Vector2 uv0)
    {
        vertexHelper.position.Add(pos);
        vertexHelper.color.Add(color);
        vertexHelper.uv.Add(uv0);
    }

    void AddTriangle(int idx0, int idx1, int idx2)
    {
        vertexHelper.triangles.Add(idx0);
        vertexHelper.triangles.Add(idx1);
        vertexHelper.triangles.Add(idx2);
    }

    void Apply(Mesh mesh, F3VertexHelper helper)
    {
        mesh.vertices = helper.position.ToArray();
        mesh.triangles = helper.triangles.ToArray();
        mesh.colors = helper.color.ToArray();
        mesh.uv = helper.uv.ToArray();
    }
}

public class F3VertexHelper
{
    public List<Vector3> position;
    public List<Color> color;
    public List<Vector2> uv;
    public List<int> triangles;


    public F3VertexHelper()
    {
        position = new List<Vector3>();
        color = new List<Color>();
        uv = new List<Vector2>();
        triangles = new List<int>();
    }

    public void Clear()
    {
        position.Clear();
        color.Clear();
        uv.Clear();
        triangles.Clear();
    }
}
