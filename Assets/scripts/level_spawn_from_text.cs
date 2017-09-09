using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;

public class level_spawn_from_text : MonoBehaviour {

    public string publicFileName;
    public GameObject wall;
    public GameObject pellet;
    public GameObject player;
    public Camera main_camera;

    

    // Use this for initialization
    void Start () {
        create_level();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restart_game();
        }
    }

    void create_level()
    {
        string path = Application.dataPath;
        string fileName = path + "/" + publicFileName;
        List<string> file_lines = new List<string>();
        // Handle any problems that might arise when reading the text
        try
        {
            string line;
            // Create a new StreamReader, tell it which file to read and what encoding the file
            // was saved as
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);
            // Immediately clean up the reader after this block of code is done.
            // You generally use the "using" statement for potentially memory-intensive objects
            // instead of relying on garbage collection.
            // (Do not confuse this with the using directive for namespace at the 
            // beginning of a class!)
            using (theReader)
            {
                // While there's lines left in the text file, do this:
                do
                {
                    line = theReader.ReadLine();


                    if (line != null)
                    {
                        // Do whatever you need to do with the text line, it's a string now
                        // In this example, I split it into arguments based on comma
                        // deliniators, then send that array to DoStuff()
                        string[] entries = line.Split(' ');
                        if (entries.Length > 0)
                            //print(line);
                            file_lines.Add(line);
                    }
                }
                while (line != null);
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
            }
        }
        // If anything broke in the try block, we throw an exception with information
        // on what didn't work
        catch (Exception e)
        {
            print(e);
        }


        float width = 0.13f;
        float height = 0.13f;

        int map_width = 0;
        // make the world from the list
        int i;
        for (int j = 0; j < file_lines.Count; ++j)
        {
            i = 0;
            foreach (char c in file_lines[j])
            {
                if (c == 'w')
                {
                    // make a wall
                    GameObject mynewwall = (GameObject)Instantiate(wall, new Vector3(i * width, (file_lines.Count - j) * height), Quaternion.identity, transform);
                }
                else if (c == 'p')
                {
                    // make a pellet
                    GameObject mynewwall = (GameObject)Instantiate(pellet, new Vector3(i * width, (file_lines.Count - j) * height), Quaternion.identity, transform);
                }
                else if (c == ' ')
                {
                    // make an empty
                }
                else if (c == 's')
                {
                    // make player
                    GameObject mynewwall = (GameObject)Instantiate(player, new Vector3(i * width, (file_lines.Count - j) * height), Quaternion.identity, transform);
                }
                ++i;
            }
            if (i > map_width) map_width = i;
        }

        // center camera on map we made
        main_camera.transform.position = new Vector3(map_width * width * 0.5f, height * file_lines.Count * 0.5f, main_camera.transform.position.z);
    }

    public void restart_game()
    {
        // delete all children
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child_transform in transform) children.Add(child_transform.gameObject);
        children.ForEach(child => Destroy(child));

        create_level();
    }

}
