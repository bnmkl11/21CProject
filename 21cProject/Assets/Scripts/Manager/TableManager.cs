using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class TableManager : SingletonBase<TableManager>
{
    public override void InitManager()
    {
        base.InitManager();

		var list = LoadJson<UIData>("Scripts/Test");
		list.ForEach(data =>
		{
			Debug.Log(data.NAME);
		});
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
