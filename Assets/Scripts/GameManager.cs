using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	private const int GameId = 4;

	[SerializeField]
	private GameObject player;
	
	[SerializeField]
	private GameObject[] platforms;
	
	[SerializeField]
	private Text levelText;
	
	private float lerpTime = 2f;
	private int level;


	void Awake(){
		makeInstance ();
	}

	private void Start()
	{
		level = Settings.Instance.level;
		levelText.text = "Level " + level;
		CreatePlatforms();
		CreatePlayer ();
	}

	private void CreatePlatforms()
	{
		foreach (var platform in platforms)
		{
			platform.SetActive(false);
		}
		platforms[level-1].SetActive(true);
	}

	void makeInstance(){
		if (Instance == null)
			Instance = this;
	}

	void Update(){
		if (GameObject.FindGameObjectWithTag("Player"))
		{
			Vector3 targetPosition = Camera.main.transform.position;
			targetPosition.x = GameObject.FindGameObjectWithTag("Player").transform.position.x;
			// Smoothly move the camera towards the desired position
			Camera.main.transform.position =
				Vector3.Lerp(Camera.main.transform.position, targetPosition, lerpTime * Time.deltaTime);
			
		}
		if (Keyboard.current.escapeKey.isPressed)
		{
			GoHome();
		}
	}


	public void CreatePlayer(){
		float minX = -0.2f, maxX = 0.24f, minY = -0.8f, maxY = -0.4f;
		Vector3 temp = new Vector3 (Random.Range (minX, minX + 0.2f), Random.Range (minY, maxY), 0);
		temp.y += 2f;
		Instantiate (player, temp, Quaternion.identity);
		
	}
	public void GoHome()
	{
		Application.LoadLevel(0);
		DataManager.Instance.GetCurrentUser().LevelsDone[GameId] = true;
	}
}
