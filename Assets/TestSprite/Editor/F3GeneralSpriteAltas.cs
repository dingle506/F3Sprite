using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.SceneManagement;

public class F3GeneralSpriteAltas
{
    [MenuItem("Tool/Make Altas")]
    public static void MakeAltas()
    {
        string path = "/TestSprite/Res/Image";
        List<Texture2D> textures = GetAllTexture(path);

        int width = 0;
        int height = 0;
        GetAllTexture(ref textures, ref width, ref height);

        Debug.Log(width + " : " + height);

        GameObject obj = new GameObject("altas");
        F3SpriteAltas altas = obj.AddComponent<F3SpriteAltas>(); //ScriptableObject.CreateInstance<F3SpriteAltas>();
        altas.Init(width, height);

        int x = 0, y = 0;
        for(int i = 0; i < textures.Count; i++)
        {
            altas.GetPos(textures[i], ref x, ref y);
            altas.AddTexture(textures[i].name, textures[i], ref x, ref y);
        }

        //AssetDatabase.CreateAsset(obj, "Assets/TestSprite/Res/Image/f3altals");

        byte[] textBytes = altas.text2d.EncodeToPNG();
        
        System.IO.File.WriteAllBytes("Assets/TestSprite/Res/Image/f3altals.png", textBytes);
        obj = PrefabUtility.SaveAsPrefabAssetAndConnect(obj, "Assets/TestSprite/Res/Image/f3altals.prefab",  InteractionMode.AutomatedAction);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        Debug.Log(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/TestSprite/Res/Image/f3altals.png"));

        obj.GetComponent<F3SpriteAltas>().text2d = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/TestSprite/Res/Image/f3altals.png");
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        //PrefabUtility.SavePrefabAsset(obj);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void GetAllTexture(ref List<Texture2D> textures, ref int width, ref int height)
    {
        textures.Sort((text1, text2) =>
        {
            if (text1.width * text2.height > text2.width * text2.height)
            {
                return 1;
            }

            return 0;
        });

        float totalArea = 0;
        int[] atlasSizes = new int[10] { 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048 };

        for (int i = 0; i < textures.Count; i++)
        {
            Texture2D tex = textures[i];
            float texWidth = tex.width;
            float texHeight = tex.height;
            float area = texWidth * texHeight;

            totalArea += area;
        }
        
        Debug.Log("totalArea: " + totalArea);

        for (int i = 0; i < atlasSizes.Length; i++)
        {
            width = atlasSizes[i];
            for (int j = 0; j < atlasSizes.Length; j++)
            {
                height = 0;
                height = atlasSizes[i];
                if (totalArea < width * height)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }

            if(totalArea < width * height)
            {
                if (width * 2 < height)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
    }

    static List<Texture2D> GetAllTexture(string path)
    {
        List<Texture2D> list = new List<Texture2D>();

        string[] filesName = Directory.GetFiles(Path.Combine(Application.dataPath + path));
        for(int i = 0; i < filesName.Length; i++)
        {
            if(filesName[i].EndsWith(".png"))
            {
                Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(FileUtil.GetProjectRelativePath(filesName[i]));
                list.Add(tex);
            }
        }

        return list;
    }
}
