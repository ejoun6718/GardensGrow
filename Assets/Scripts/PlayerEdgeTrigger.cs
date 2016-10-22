﻿using UnityEngine;
using System.Collections;

public class PlayerEdgeTrigger : MonoBehaviour {
	public bool isTriggered;

	void OnTriggerEnter2D(Collider2D other) {
		isTriggered = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		isTriggered = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		isTriggered = false;
	}
}