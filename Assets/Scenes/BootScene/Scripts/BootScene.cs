using UnityEngine;
using App.SceneManager;

public class BootScene : MonoBehaviour
{

	/// <summary>シーンマネージャオブジェクト</summary>///
	[SerializeField]
	private GameObject _startUp;

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad (_startUp);
		AppSceneManager.Instance.LoadScene ("TitleScene", false);
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
