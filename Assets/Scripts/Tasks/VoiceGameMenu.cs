using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VoiceGameMenu : MonoBehaviour {
	
	private const int GameId = 4;
	[SerializeField] private Toggle[] LevelButtons;

	private void Start()
	{
		Settings.Instance.level = 1;
	}

	private void Update()
	{
		if (Keyboard.current.escapeKey.isPressed)
		{
			GoHome();
		}
		LevelButtons[Settings.Instance.level-1].Select();
	}

	public void PlayGame()
	{
		Application.LoadLevel("Gameplay");
	}

	public void SetLevel(int value)
	{
		Settings.Instance.level = value;
	}


	public void Help(){
		Application.LoadLevel ("howTo");
	}
	
	public void GoHome()
	{
		Application.LoadLevel(0);
		DataManager.Instance.GetCurrentUser().LevelsDone[GameId] = true;
	}
	
}
