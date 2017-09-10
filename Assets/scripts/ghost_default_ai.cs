﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost_default_ai : MonoBehaviour {

	public float speed = 0.04f;
	Vector2 dest = Vector2.zero;
	Vector2 move_dir = -Vector2.right;
	int message = 0;
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// get the gameobject of what he hit, and see what type of object it is
		GameObject col_object = collision.gameObject;
		int col_layer = col_object.layer;
		switch (col_layer) {
		case 11:
			break;
		}
	}

	void turn(Vector2 dir){
		dest = (Vector2)transform.position + (dir * 0.13f);
		int angle = 90;
		if (dest == Vector2.up)
			angle = 90;
		else if (dest == Vector2.down)
			angle = 270;
		else if (dest == Vector2.left)
			angle = 180;
		else if (dest == Vector2.right)
			angle = 0;

		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		move_dir = dir;
	}

	bool valid(Vector2 dir)
	{
		Vector2 pos = transform.position;
		// we only want to look at ghost and walls, ignore pacman and pellets
		// all walls are on layer 8, ghost is on layer 11

		int wall_layer_mask = 1 << 8;
		// draw a line from the position of the square next to us to our square.
		// if the first thing that we hit is ghost, there is nothing blocking that space.
		// If the first thing we hit is NOT ghost, the space is occupied by something so player cannot move there
		RaycastHit2D hit = Physics2D.Linecast(transform.position, dest, wall_layer_mask);

		// return true if the thing we hit is NOT wall
		return hit.collider == null;
	}

	void randomDir(){
		Vector2[] choices = new Vector2[]{Vector2.left, Vector2.right, Vector2.up, Vector2.down};
		turn (choices [(int)Random.Range (0, 4)]);
	}
		
	void continueDir(){
		turn (move_dir);
	}

	// Update is called once per frame
	void FixedUpdate () {
		// if we can move in that direction, we move it.
		if (valid (move_dir)) {
			Vector2 p = Vector2.MoveTowards (transform.position, dest, speed);
			GetComponent<Rigidbody2D> ().MovePosition (p);
		} else {
			randomDir ();
			return;
		}

		// if it's not at the destination, continue
		if (Vector2.Distance(transform.position, dest) > 0.001){
			print ("position" + message + "," + Vector2.Distance(transform.position, dest));
			message++;
			return;
		} else {
			// continue the direction
			// continueDir ();
			randomDir();
			return;
		}
	}
}
