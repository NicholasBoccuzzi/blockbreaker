﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour {

	public bool autoPlay = false;
	private Ball ball;
	private bool started = false;
	float xValue;
	float yValue;
	public LevelManager lev;
	// Use this for initialization
	void Start () {

		ball = GameObject.FindObjectOfType<Ball>();
		GetComponent<AudioSource> ().playOnAwake = false;
		if (lev.lev == "Level_Auto") {
			autoPlay = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (autoPlay) {
			started = true;
		} else if (ball && started == false) {
			started = ball.started;		
		}

		playerControlled();
		AutoPlay();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (started) {
			GetComponent<AudioSource> ().Play();
		}
	}

	void AutoPlay() {
		if (autoPlay) {
			Vector3 paddlePos = new Vector3 ( 8, this.transform.position.y, 0f);
			Vector3 ballPos = ball.transform.position;
			xValue = ballPos.x;
			paddlePos.x = xValue;				
			this.transform.position = paddlePos;
			ball.AutoPlayLaunch();
		}
	}

	void playerControlled () {
		if (!autoPlay) {
			Vector3 paddlePos = new Vector3 ( 8, this.transform.position.y, 0f);
			if (lev.lev == "Level_02") {
				xValue = (Input.mousePosition.x/Screen.width * -16) + 16;
				paddlePos.x = xValue;	
				this.transform.position = paddlePos;
			} else {
				xValue = Input.mousePosition.x/Screen.width * 16;
				paddlePos.x = xValue;				
				this.transform.position = paddlePos;
			}
		}
	}
}
