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
	public Text finalTimer;
	public Text lapCount;

	private float currentTime;
	public GameObject Player;
	public GameObject EndGameObjects;

	AudioSource music;

	bool endGameOccurred;

	float finalTime;

	void Awake ()
	{
		Time.timeScale = 1;
		Player.SetActive (true);
		EndGameObjects.SetActive (false);

	}

	// setup the game
	void Start ()
	{
		EndGameObjects.SetActive (false);
		endGameOccurred = false;
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
			if (!endGameOccurred) {
				EndGame ();
				endGameOccurred = true;
			}
		}
		else
		{
			currentTime += Time.deltaTime;
			mainTimer.text = timerConverter (currentTime);
			lapCount.text = "Lap: "+PlayerMovement.teleportCount+"/3";
		}
			
	}



	public void EndGame ()
	{
		// game is over
		song.volume = 0.6f;
		song.pitch = 0.8f;
		finalTime = currentTime;
		PlayerPrefs.SetInt ("Player's Time", (int)(currentTime));
		Player.SetActive (false);
		EndGameObjects.SetActive (true);

		finalTimer.text = "Time: "+ timerConverter (finalTime);

	}


	private string timerConverter(float time)
	{
		string textTime;
		float mins = currentTime / 60;
		float secs = currentTime % 60;

		textTime = mins.ToString ("00:")+secs.ToString("00");

		return textTime;

	}

}
