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

	private float currentTime;
	public GameObject Player;


	AudioSource music;

	private int finalScore;

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
		if (ScoreManager.score >= 25)
		{
			finalScore = ScoreManager.score;
			EndGame ();
		}
		else
		{
			currentTime += Time.deltaTime;
			mainTimer.text = currentTime.ToString ("00:00.00");
		}
			
	}



	public void EndGame ()
	{
		// game is over
		Time.timeScale = 0;
		ScoreManager.score = finalScore;
		song.volume = 0.6f;
		song.pitch = 0.8f;
		int finalTime = (int)(currentTime);
		PlayerPrefs.SetInt ("Player's Time", finalTime);
	}

}
