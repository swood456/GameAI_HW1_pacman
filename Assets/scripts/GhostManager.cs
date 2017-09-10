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
    
	public void clear_ghosts()
    {
        ghosts = new ghost_default_ai[] { };
    }

	// Update is called once per frame
	void Update () {
		if(ghosts.Length != num_ghosts)
        {
            ghosts = FindObjectsOfType<ghost_default_ai>();
        }
        
	}

    public bool is_dest_valid(Vector2 test_dest)
    {
        if (ghosts.Length != num_ghosts)
        {
            ghosts = FindObjectsOfType<ghost_default_ai>();
        }
        foreach (ghost_default_ai ghost_ai in ghosts)
        {
            if (ghost_ai.get_dest() == test_dest)
                return false;
            if (ghost_ai.get_pos() == test_dest)
                return false;
        }
        return true;
    }
}
