using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

	public static GameOverManager instance;
	private GameObject gameOverPanel;
	private Animator gameOverAnim;
	private Button retryBtn,MenuBtn, NextBtn;
	private Text score;

	private GameObject scoreText;
	public Text highScore;
	public Text gameOverText;

	void Awake(){
		makeInstance ();
		InitializeVariable ();
	}

	public void makeInstance(){
		if (instance == null)
			instance = this;
	}

	public void showGameOverPanel(bool win){
		if (gameOverPanel)
		{
			gameOverPanel.SetActive(true);
			ScoreManager.instance.GetHighScore();
			score.text = "Score: " + "" + ScoreManager.instance.GetScore();
			Debug.Log("score"+score.text);
			highScore.text = "High Score: " + PlayerPrefs.GetInt("highScore").ToString();
			if (win)
				gameOverText.text = "you win!";
			gameOverAnim.Play("GameOverPanel fadeIn");
		}


	}

	void InitializeVariable(){
		gameOverPanel = GameObject.Find ("GameOverPanel Holder");
		gameOverAnim = gameOverPanel.GetComponent<Animator> ();
		retryBtn = GameObject.Find ("RetryButton").GetComponent<Button>();
		MenuBtn = GameObject.Find ("MenuButton").GetComponent<Button> ();
		NextBtn = GameObject.Find ("NextButton").GetComponent<Button> ();

		retryBtn.onClick.AddListener (() => PlayAgain ());
		MenuBtn.onClick.AddListener (() => Menu ());
		NextBtn.onClick.AddListener (() => NextLevel ());

		scoreText = GameObject.Find ("ScoreText");
		score = GameObject.Find ("Text").GetComponent<Text> ();

		gameOverPanel.SetActive (false);
	}

	private void NextLevel()
	{
		Settings.Instance.SetNextLevel();
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void PlayAgain(){
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void Menu(){
		Application.LoadLevel(0);
	}
}
