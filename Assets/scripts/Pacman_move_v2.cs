using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman_move_v2 : MonoBehaviour {

    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;

    Vector2 move_dir = -Vector2.right;

    // Use this for initialization
    void Start () {
        dest = (Vector2)transform.position - Vector2.right * 0.13f;
        transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
    }

    // Update is called once per frame
    void FixedUpdate () {
        // Move closer to Destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
        
        if ((Vector2)transform.position == dest)
        {
            if(valid(move_dir))
                dest = (Vector2)transform.position + (move_dir * 0.13f);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col_object = collision.gameObject;
        int col_layer = col_object.layer;

        if(col_layer == 9)
        {
            Destroy(col_object);
        }
    }

    bool valid(Vector2 dir)
    {
        // Cast Line from 'next to Pac-Man' to 'Pac-Man'
        Vector2 pos = transform.position;
        int wall_layer_mask = 1 << 10 | 1 << 8;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir * 0.13f, pos, wall_layer_mask);
        //RaycastHit2D hit = Physics2D.Linecast(pos + dir * 0.13f, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }



}
