using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
	public string lev;
	public Brick brick;
	private PaddleMovement paddleMovement = null;
	public int timer = 600;
	private bool auto = false;

	void Start () {
		
	}

	void Update () {
		BrickDestroyed();
		
		if (lev == "Level_Auto") {
			if (auto == true) {
				if (paddleMovement == null) {
					paddleMovement = GameObject.FindObjectOfType<PaddleMovement>();
					paddleMovement.autoPlay = true;
				}
			}
			checkInput();
		} else if (lev == "Level_01" || lev == "Level_02") {
			Cursor.visible = false;
		} else if (!Cursor.visible) {
			Cursor.visible = true;
		}



		if (lev == "Start") {
			timer -= 1;
			if (timer == 0) {
				auto = true;
				LoadLevel("Level_Auto");
			}
		}

	}

	void checkInput () {
		if (Input.anyKeyDown) {
			Debug.Log(timer);
			auto = false;
			LoadLevel("Start");
		}
	}


	public void LoadLevel(string sceneName) {
		if (sceneName == "Lose") {
			SceneManager.LoadScene("Lose");
		} else {
			Brick.brickCount = 0;				
			SceneManager.LoadScene(sceneName);
		}
	}

	public void QuitLevel(string name) {
		Application.Quit();
	}

	public void LoadNextLevel() {
		if (lev == "Level_Auto") {
			Brick.brickCount = 0;					
			SceneManager.LoadScene("Level_Auto");
		} else {
			Brick.brickCount = 0;					
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	public void BrickDestroyed() {
		if (Brick.brickCount <= 0)  {
			if (lev == "Level_01" || lev == "Level_02" || lev == "Level_Auto") {
				LoadNextLevel();
			}
		}
	}
}
