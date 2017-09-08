using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    int cur_score = 0;
    int highscore = 0;
    level_spawn_from_text ls;

    // Use this for initialization
    void Start () {
        ls = FindObjectOfType<level_spawn_from_text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
        {
            ls.restart_game();
        }
	}
}
