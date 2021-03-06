﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost_default_ai : MonoBehaviour {

	public float speed = 0.04f;
	Vector2 dest = Vector2.zero;
    Vector2 cur_pos;
	Vector2 move_dir = -Vector2.right;
    
	int message = 0;
    GhostManager gm;

    public Vector2 get_dest() { return dest; }
    public Vector2 get_pos() { return cur_pos; }

    private void Start()
    {
        dest = transform.position;
        cur_pos = transform.position;
        gm = FindObjectOfType<GhostManager>();
    }

    private bool valid(Vector2 dir)
	{
		Vector2 pos = transform.position;
		// we only want to look at ghost and walls, ignore pacman and pellets
		// all walls are on layer 8, ghost is on layer 11

		int wall_layer_mask = 1 << 8 | 1 << 11;
        // draw a line from the position of the square next to us to our square.
        // if the first thing that we hit is us, there is nothing blocking that space.
        RaycastHit2D hit = Physics2D.Linecast(pos + dir * 0.13f, pos, wall_layer_mask);

        // return true if the thing we hit is NOT wall
        //return  gm.is_dest_valid((Vector2)transform.position + (dir * 0.13f)) && hit.collider == GetComponent<Collider2D>();
        return gm.is_dest_valid((Vector2)transform.position + (dir * 0.13f)) && hit.collider.gameObject.name == name;//hit.collider == GetComponent<Collider2D>();
        //return gm.is_dest_valid((Vector2)transform.position + (dir * 0.13f));
    }

    // Update is called once per frame
    void FixedUpdate () {

        // move towards our destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
        

        // when we reach our destination, pick a new one
        if ((Vector2)transform.position == dest)
        {
            cur_pos = dest;
            Vector2[] choices = new Vector2[] { -Vector2.right, Vector2.right, Vector2.up, -Vector2.up };
            int[] degrees = new int[] { 180, 0, 90, 270 };
            // try a few times to get a random direction
            for(int i = 0; i < 10; ++i)
            {
                int index = Random.Range(0, 4);
                Vector2 next_dir = choices[index];
                if(valid(next_dir))
                {
                    dest = (Vector2)transform.position + (next_dir * 0.13f);
                    transform.rotation = Quaternion.AngleAxis(degrees[index], Vector3.forward);
                    //print(" new dest is: " + dest);
                    return;
                }
            }

            // after a few tries, just try all possible directions and if that works, stay put
            for(int i = 0; i < 4; ++i)
            {
                if(valid(choices[i]))
                {
                    dest = (Vector2)transform.position + (choices[i] * 0.13f);
                    transform.rotation = Quaternion.AngleAxis(degrees[i], Vector3.forward);
                    //print(" new dest is: " + dest);
                    return;
                }
            }
        }
    }
}
