using UnityEngine;

public class TitleScene : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{

	}

	/// <summary>
	/// スタートボタンを押したときの処理
	/// </summary>
	public void OnStartButton ()
	{
		MainMenuScene.ChangeScene ();
	}
}
