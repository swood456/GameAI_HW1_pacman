using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacman_controller : MonoBehaviour {

    public float movespeed = 5.0f;
    

    Vector2 move_dir = new Vector2(-1, 0);

	// Use this for initialization
	void Start () {
        transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
    }

    bool is_valid_move(int x, int y)
    {
        // eventually actually check for valid stuff
        Vector2 pos = transform.position;
        move_dir += new Vector2(move_dir.x * 0.45f, move_dir.y * 0.45f);
        RaycastHit2D hit = Physics2D.Linecast(pos + move_dir, pos);
        print("hit name: " + hit.collider.name);
        return hit.collider.name == "pellet" || hit.collider.name == "pacman" || (hit.collider == GetComponent<Collider2D>());
    }
	
	// Update is called once per frame
	void Update () {

        Vector2 update_pos = transform.position;

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //check if up is valid
            if(is_valid_move(0, 1))
            {
                // set facing to up
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);

                move_dir.x = 0;
                move_dir.y = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //check if up is valid
            if (is_valid_move(0, -1))
            {
                transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                move_dir.x = 0;
                move_dir.y = -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //check if up is valid
            if (is_valid_move(1, 0))
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                move_dir.x = 1;
                move_dir.y = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //check if up is valid
            if (is_valid_move(-1, 0))
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                move_dir.x = -1;
                move_dir.y = 0;
            }
        }

        //transform.position = transform.position + new Vector3(movespeed * Time.deltaTime * move_dir.x, movespeed * Time.deltaTime * move_dir.y);
        GetComponent<Rigidbody2D>().velocity = new Vector2(movespeed * move_dir.x, movespeed * move_dir.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // see if it is a wall
        GameObject col_object = collision.gameObject;
        int col_layer = col_object.layer;
        if(col_layer == 8)
        {
            move_dir.x = 0;
            move_dir.y = 0;
            // TODO: stop eat animation
        }
    }
}
