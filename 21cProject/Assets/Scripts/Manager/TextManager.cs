using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class TextManager : SingletonBase<TextManager>
{
    Dictionary<int, string> m_DicOfText = new Dictionary<int, string>();

    public override void InitManager()
    {
        base.InitManager();

		var list = LoadJson<StringData>("Scripts/String");
		list.ForEach(data =>
		{
			if (m_DicOfText.ContainsKey(data.INDEX) == false)
            {
				m_DicOfText.Add(data.INDEX, data.KOREAN);

			}
		});
	}

	/// <summary>
	/// 텍스트 가져오기.
	/// </summary>
	public string GetText(int stringID)
    {
		return m_DicOfText[stringID];
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
}
