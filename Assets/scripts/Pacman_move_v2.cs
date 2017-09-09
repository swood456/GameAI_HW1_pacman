using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pacman_move_v2 : MonoBehaviour {

    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;

    Vector2 move_dir = -Vector2.right;
    Text score_text;
    int cur_score = 0;

    // Use this for initialization
    void Start () {
        dest = (Vector2)transform.position - Vector2.right * 0.13f;
        transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);

        // find the text to update score
        Text[] text_boxes = FindObjectsOfType<Text>();
        foreach (Text t in text_boxes)
        {
            if(t.gameObject.name == "score_text")
            {
                score_text = t;
                break;
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate () {
        // Move closer to Destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
        
        // the player can only move pacman if he is in the center of a tile
        if ((Vector2)transform.position == dest)
        {
            // default behavior is that pacman continues on his path unless player
            //  changes pacman's direction
            if (valid(move_dir))
            {
                // if the next space in out line is valid, keep moving unless player says otherwise
                dest = (Vector2)transform.position + (move_dir * 0.13f);
            }

            // check to see if each of the arrow keys is pressed down. If they are, see if that
            // direction is valid to move. If it is, set our direction to that way
            
            // NOTE: currently the lower down arrow keys have higher "precedence" (if you press both up
            // and down, down overrides) I don't think it is an issue though
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
            {
                dest = (Vector2)transform.position + (Vector2.up * 0.13f);
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                move_dir = Vector2.up;
            }
            if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right * 0.13f;
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                move_dir = Vector2.right;
            }
            if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
            {
                dest = (Vector2)transform.position - Vector2.up * 0.13f;
                transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
                move_dir = -Vector2.up;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
            {
                dest = (Vector2)transform.position - Vector2.right * 0.13f;
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                move_dir = -Vector2.right;
            }
        }

    }

    // function that is called when pacman collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // get the gameobject of what he hit, and see what type of object it is
        GameObject col_object = collision.gameObject;
        int col_layer = col_object.layer;

        // layer 9 is for pellets, so for now we delete and eventually increment the score
        if(col_layer == 9)
        {
            Destroy(col_object);

            cur_score++;
            if(score_text)
            {
                score_text.text = cur_score.ToString("00000");
            }
        }
    }

    bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        // we only want to look at pacman and walls, ignore ghosts and pellets
        // all walls are on layer 8, pacman is on layer 10

        int wall_layer_mask = 1 << 10 | 1 << 8;
        // draw a line from the position of the square next to us to our square.
        // if the first thing that we hit is pacman, there is nothing blocking that space.
        // If the first thing we hit is NOT pacman, the space is occupied by something so player cannot move there
        RaycastHit2D hit = Physics2D.Linecast(pos + dir * 0.13f, pos, wall_layer_mask);

        // return if the thing we hit is pacman
        return (hit.collider == GetComponent<Collider2D>());
    }



}
