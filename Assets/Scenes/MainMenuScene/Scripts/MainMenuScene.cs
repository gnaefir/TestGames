using UnityEngine;
using System;
using App.SceneManager;

public class MainMenuScene : MonoBehaviour
{
	class SceneParam
	{
		/// <summary>コールバック</summary>///
		public Action cb;
	}

	/// <summary>シーンのパラメータ</summary>///
	private SceneParam _sceneParam = null;

	/// <summary>
	/// フェードシーンをロード
	/// </summary>
	public static void ChangeScene (Action cb = null)
	{
		var param = new SceneParam ();
		param.cb = cb;

		AppSceneManager.Instance.LoadScene ("MainMenuScene", false);
	}

	// Use this for initialization
	void Start ()
	{
		var argument = AppSceneManager.Instance.GetArgument ();
		_sceneParam = argument as SceneParam;
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
