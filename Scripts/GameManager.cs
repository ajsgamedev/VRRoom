using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

	// make game manager public static so can access this from other scripts
	public static GameManager gm;

	public AudioSource song;
	public AudioSource ringSound;

	public float startTime = 0.0f;
	public Text mainTimer;
	public Text lapCount;

	private float currentTime;
	public GameObject Player;
	public GameObject EndGameObjects;

	AudioSource music;

	private int finalScore;
	float mins;
	float secs;

	void Awake ()
	{
		Time.timeScale = 1;
		Player.SetActive (true);
	}

	// setup the game
	void Start ()
	{
		
		currentTime = 0.0f;
		// get a reference to the GameManager component for use by other scripts
		if (gm == null)
			gm = this.gameObject.GetComponent<GameManager> ();

	}


	// this is the main game event loop
	void Update ()
	{
		if (PlayerMovement.teleportCount >= 3)
		{
			finalScore = ScoreManager.score;
			EndGame ();
		}
		else
		{
			currentTime += Time.deltaTime;
			mins = currentTime / 60;
			secs = currentTime % 60;
			mainTimer.text = mins.ToString ("00:")+secs.ToString("00");
			lapCount.text = "Lap: "+PlayerMovement.teleportCount+"/3";
		}
			
	}



	public void EndGame ()
	{
		// game is over
		//Time.timeScale = 0;
		ScoreManager.score = finalScore;
		song.volume = 0.6f;
		song.pitch = 0.8f;
		int finalTime = (int)(currentTime);
		PlayerPrefs.SetInt ("Player's Time", finalTime);
		Player.SetActive (false);
		EndGameObjects.SetActive (true);

	}

}
