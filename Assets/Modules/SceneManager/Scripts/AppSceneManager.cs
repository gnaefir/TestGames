using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace App.SceneManager
{
	public class AppSceneManager : SingletonMonoBehaviour<AppSceneManager>
	{
		public class Argument
		{
			public string sceneName;
			public object param;
		}

		/// <summary>シーン情報保存しておく</summary>///
		private static object _sceneArgument { get; set; }

		/// <summary>シーン情報保存取得</summary>///
		public object GetArgument ()
		{
			return _sceneArgument;
		}

		/// <summary>シーンの履歴を保存しておく</summary>///
		private List<Argument> _arguments = new List<Argument> ();

		/// <summary>
		/// シーンをロードする関数（single）
		/// </summary>
		/// <param name="sceneName">シーンの名前</param>
		/// <param name="isAddScene">シーン履歴に追加するかどうか</param>
		/// <param name="param">シーンのパラメータ</param>
		public void LoadScene (string sceneName, bool isAddScene = false, object param = null)
		{
			// シーン情報初期化
			_sceneArgument = null;

			// シーンを通常ロード
			UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName);

			if (isAddScene)
			{
				// 二重でシーンに遷移するのを防ぐ
				if (GetLastSceneHistory () != null)
				{
					var prevSceneName = GetLastSceneHistory ().sceneName;
					if (sceneName == prevSceneName)
					{
						DeleteLastSceneHistory ();
					}
				}

				// シーン履歴に追加する
				SetParam (sceneName, param);
			}
			// シーン情報保存
			_sceneArgument = param;
		}

		/// <summary>
		/// シーンを追加する関数（Additive）
		/// </summary>
		/// /// <param name="sceneName">シーンの名前</param>
		/// <param name="isAddScene">シーン履歴に追加するかどうか</param>
		/// <param name="isFade">フェードするかどうか</param>
		/// <param name="param">シーンのパラメータ</param>
		public void AdditiveScene (string sceneName, bool isAddScene = true, bool isFade = false, object param = null)
		{
			// シーン情報初期化
			_sceneArgument = null;

			// シーンを追加ロード
			UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName, LoadSceneMode.Additive);

			if (isAddScene)
			{
				// シーン履歴に追加する
				SetParam (sceneName, param);
			}

			// シーン情報保存
			_sceneArgument = param;
		}

		/// <summary>
		/// シーンのパラメータを保存
		/// </summary>
		private void SetParam (string sceneName, object param)
		{
			Argument argument = new Argument ();
			argument.sceneName = sceneName;
			if (param != null)
			{
				argument.param = param;
			}
			_arguments.Add (argument);
		}

		/// <summary>
		/// シーンをアンロードする
		/// </summary>
		/// <param name="sceneName"></param>
		public void UnLoadScene (string sceneName)
		{
			// シーン情報初期化
			_sceneArgument = null;

			UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync (sceneName);
		}

		/// <summary>
		/// シーン履歴の最後をアンロード
		/// </summary>
		public void UnLoadLastScene ()
		{
			UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync (_arguments [_arguments.Count - 1].sceneName);
		}

		/// <summary>
		/// シーン履歴の最後を削除
		/// </summary>
		public void DeleteLastSceneHistory ()
		{
			_arguments.RemoveAt (_arguments.Count - 1);
		}

		/// <summary>
		/// シーン履歴をシーンを指定して削除
		/// </summary>
		public void DeleteSceneHistory (string sceneName)
		{
			/// ユニークじゃない場合 (最初に見つかったものを削除する)
			var index = _arguments.FindIndex (item => item.sceneName == sceneName);
			if (index >= 0)
			{
				_arguments.RemoveAt (index);
			}
		}

		/// <summary>
		/// シーン履歴の最後を取得
		/// </summary>
		public Argument GetLastSceneHistory ()
		{
			if (_arguments.Count > 0)
			{
				return _arguments [_arguments.Count - 1];
			}

			return null;
		}

		/// <summary>
		/// シーン履歴をシーンを指定して履歴取得
		/// </summary>
		/// <param name="sceneIndex">シーンのインデックス</param>
		public Argument GetSceneHistory (string sceneName)
		{
			var index = _arguments.FindIndex (item => item.sceneName == sceneName);
			if (index >= 0)
			{
				return _arguments [index];
			}

			return null;
		}

		/// <summary>
		/// シーン履歴をシーンを指定してセット(戻るボタンの仕様がわからないので作っておく)
		/// </summary>
		public void SetSceneHistory (string sceneName)
		{
			Argument argument = new Argument ();
			argument.sceneName = sceneName;
			_arguments.Add (argument);
		}

		/// <summary>
		/// シーン履歴クリア
		/// </summary>
		public void ClearSceneHistory ()
		{
			_arguments.Clear ();
		}
	}
}