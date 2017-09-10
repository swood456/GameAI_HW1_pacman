using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour {

    int num_ghosts = 0;
    ghost_default_ai[] ghosts = { };

    public void set_num_ghosts(int num_g)
    {
        num_ghosts = num_g;
    }
    
	
	// Update is called once per frame
	void Update () {
		if(ghosts.Length != num_ghosts)
        {
            ghosts = FindObjectsOfType<ghost_default_ai>();
        }

        // debugging
        for(int i = 0; i < num_ghosts; ++i)
            for(int j = i+1; j < num_ghosts; ++j)
            {
                if (ghosts[i].get_dest() == ghosts[j].get_dest())
                    print("BAD! 2 ghosts share same dest");
            }
        /*
        if (ghosts[0].get_dest() == ghosts[1].get_dest())
            print("BAD! 2 ghosts share same dest");
            */
	}

    public bool is_dest_valid(Vector2 test_dest)
    {
        foreach(ghost_default_ai ghost_ai in ghosts)
        {
            if (ghost_ai.get_dest() == test_dest)
                return false;
            if (ghost_ai.get_pos() == test_dest)
                return false;
        }
        return true;
    }
}
