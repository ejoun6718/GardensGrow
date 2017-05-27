﻿using UnityEngine;
using System.Collections;

public class PortalObject : RespawnLocation {
    // 0 = Main Menu
    // 1 = Overworld
    // 2 = Lava Dungeon
    // 3 = Lava Dungeon Boss
    // 4 = Wind Dungeon
    // 5 = Wind Dungeon Boss
    // 6 = Cave Dungeon
    // 7 = Cave Dungeon Boss

    public int levelToLoad;
    public bool lightSource = false;

    public GameObject loadingScreen;

	void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.CompareTag("Player"))
        {
            base.updateSpawnLocation();
            if (!lightSource)
            {
                loadingScreen.SetActive(true);
                Application.LoadLevel(levelToLoad);
            }

        }
	}
}
