using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingletonBase<ResourceManager>
{
    // 텍스처 딕셔너리.
    private Dictionary<string, Texture> m_DicTexture = new Dictionary<string, Texture>();

    // 스프라이트 딕셔너리.
    private Dictionary<string, Sprite> m_DicSprite = new Dictionary<string, Sprite>();

    public override void InitManager()
    {
        base.InitManager();
        LoadAllSprite("Image/Pictures");
        LoadAllSprite("Image/Albums");
    }

    public T Load<T>(string path) where T : Object
    {
        T tobj = Resources.Load<T>(path);
        if (tobj == null)
        {
            Debug.LogError("Fail Load Resource " + path);
            return null;
        }
        return tobj;
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        T[] obj = Resources.LoadAll<T>(path);

        if (obj == null)
        {
            Debug.LogError("Fail Load Resource " + path);
            return null;
        }
        return obj;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Fail Instantiate Resource");
            return null;
        }

        GameObject Prefabs = Load<GameObject>(path);

        if (Prefabs == null)
            return null;

        return Instantiate(Prefabs, parent);
    }

    public GameObject Instantiate(GameObject newResouces, Transform parent = null)
    {
        if (newResouces == null)
        {
            Debug.LogError("Fail Instantiate Resource");
            return null;
        }

        return Object.Instantiate(newResouces, parent);
    }

    // 해당 경로의 텍스처 불러옴.
    void LoadAllTexture(string path)
    {
        Texture[] texture = LoadAll<Texture>(path);

        for (int i = 0; i < texture.Length; i++)
        {
            int texID = -1;
            if (!m_DicTexture.ContainsKey(texture[i].name))
            {
                m_DicTexture.Add(texture[i].name, texture[i]);
            }
        }
    }

    // 텍스처 반환.
    public Texture GetTexture(string textureID)
    {
        Texture tex;

        if (m_DicTexture.TryGetValue(textureID, out tex))
        {
            return tex;
        }

        return null;
    }

    // 모든 스프라이트 불러옴,
    public void LoadAllSprite(string path)
    {
        Sprite[] sprite = LoadAll<Sprite>(path);

        for (int i = 0; i < sprite.Length; i++)
        {
            if (!m_DicSprite.ContainsKey(sprite[i].name))
            {
                m_DicSprite.Add(sprite[i].name, sprite[i]);
            }
        }
    }

    // 스프라이트 반환.
    public Sprite GetSprite(string path, string spriteID)
    {
        Sprite tex;
        if (m_DicSprite.TryGetValue(spriteID, out tex))
        {
            return tex;
        }

        Debug.LogError(spriteID);
        return null;

    }

    // 스프라이트 반환.
    public Sprite GetSprite(string spriteID)
    {
        Sprite tex;
        if (m_DicSprite.TryGetValue(spriteID, out tex))
        {
            return tex;
        }

        Debug.LogError(spriteID);
        return null;

    }


}