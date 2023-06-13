using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class TableManager : SingletonBase<TableManager>
{
	private List<AlbumData> m_ListOfAlbum;

	public List<AlbumData> ListOfAlbum
	{
		get => m_ListOfAlbum;
	}

	private List<CharacterData> m_ListOfCharacter;

	public List<CharacterData> ListOfCharacter
    {
		get => m_ListOfCharacter;
    }

	private List<ItemData> m_ListOfItem;

	public List<ItemData> ListOfItem
	{
		get => m_ListOfItem;
	}


	public override void InitManager()
    {
        base.InitManager();

		m_ListOfAlbum		= LoadJson<AlbumData>("Scripts/Album");
		m_ListOfCharacter	= LoadJson<CharacterData>("Scripts/Character");
		m_ListOfItem		= LoadJson<ItemData>("Scripts/Item");
	}


    private List<T> LoadJson<T>(string path)
    {
		var json = ResourceManager.Instance.Load<TextAsset>(path);
		if (json == null)
			return new List<T>();

		if (string.IsNullOrEmpty(json.ToString()))
			return new List<T>();

		var newList = new List<T>();
		var jsonArr = JSON.Parse(json.ToString()).AsArray;
		int len = jsonArr.Count;
		for (int i = 0; i < len; i++)
		{
			string value = jsonArr[i].AsObject.ToString();
			var data = JsonUtility.FromJson<T>(value);
			newList.Add(data);
		}
		return newList;
	}

	public void GetData()
    {

    }


}
