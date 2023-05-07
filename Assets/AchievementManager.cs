using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {


	public static AchievementManager instance;


	void Awake(){
		if (instance == null)
			instance = this;
	}

	public void Login(){
		Social.localUser.Authenticate ((bool success) => {
		});
	}

	public void ShowAchievements(){
		if (Social.localUser.authenticated) {
			Social.ShowAchievementsUI ();
		} else {
			Login ();
		}
	}

	public void CheckAchievements(){
		if (ScoreManager.instance.score > 5)
			Social.ReportProgress (Achievments.achievement_beginner, 100f, (bool success) => {
			});
		if (ScoreManager.instance.score > 10)
			Social.ReportProgress (Achievments.achievement_intermediate, 100f, (bool success) => {
			});
		if (ScoreManager.instance.score > 30)
			Social.ReportProgress (Achievments.achievement_expert, 100f, (bool success) => {
			});
		if (ScoreManager.instance.score > 50)
			Social.ReportProgress (Achievments.achievement_pro, 100f, (bool success) => {
			});
		if (ScoreManager.instance.score > 100)
			Social.ReportProgress (Achievments.achievement_jobless, 100f, (bool success) => {
			});
	}
}
