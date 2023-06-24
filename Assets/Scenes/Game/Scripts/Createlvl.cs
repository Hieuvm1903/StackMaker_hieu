using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Createlvl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextAsset file;
    [SerializeField] private GameObject brick;
    [SerializeField] private GameObject wall;
    [SerializeField] public GameObject startpoint;
    [SerializeField] public GameObject endpoint;
    [SerializeField] private GameObject unbrick;
    [SerializeField] private GameObject changedir;
    [SerializeField] private GameObject unbrickcell;
    [SerializeField] private GameObject unbrickcorner;
    public string Lvl;


    Vector3 start;
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Start()
    {
        
        transform.position = Vector3.zero;
        var content = "";
        if (file == null)
        {
            TextAsset textasset = (TextAsset)Resources.Load("Text/"+Lvl);
             content = textasset.text;
        }
        else
        {
             content = file.text;
        }
        
        var AllWords = content.Split("\n");
        string[] map = AllWords;
        for (int i = 0; i < map.Length; i++)
        {
           // Debug.Log(map[i]);
          string filterrow =  map[i].Replace("\r", string.Empty);
            string[] row = filterrow.Split(",");
            for (int j = 0; j < row.Length; j++)
            {
                
                Vector3 pos = new Vector3(j, 0, -i)+transform.position;
                
                switch (row[j])
                {
                    case "0":
                        {
                            //Wall
                            pos.y += 0.5f;
                            Instantiate(wall, pos, transform.rotation, this.transform);
                        }
                        break;
                    case "1":
                        {
                            //Brick
                            pos.y += 0.15f;
                            Instantiate(brick, pos, transform.rotation, this.transform);
                        }
                        break;
                    case "20":
                        {
                            //Turnbrick
                            pos.y += 0.15f;
                            Instantiate(brick, pos, transform.rotation, this.transform);
                            Instantiate(changedir, pos, transform.rotation, this.transform);

                        }
                        break;
                    case "21":
                        {
                            //Turnbrick
                            pos.y += 0.15f;
                            Instantiate(brick, pos, transform.rotation, this.transform);
                            Quaternion rot = Quaternion.Euler(0, 90, 0);
                            Instantiate(changedir, pos, rot, this.transform);
                        }
                        break;
                    case "22":
                        {
                            //Turnbrick
                            pos.y += 0.15f;
                            Instantiate(brick, pos, transform.rotation, this.transform);
                            Quaternion rot = Quaternion.Euler(0, 180, 0);
                            Instantiate(changedir, pos, rot, this.transform);
                            

                        }
                        break;
                    case "23":
                        {
                            //Turnbrick
                            pos.y += 0.15f;
                            Instantiate(brick, pos, transform.rotation, this.transform);
                            Quaternion rot = Quaternion.Euler(0, -90, 0);
                            Instantiate(changedir, pos, rot, this.transform);

                        }
                        break;
                    
                    case "3":
                        {
                            //Start
                            Instantiate(startpoint, pos, transform.rotation, this.transform);
                            start = pos;
                            start.y += 0.15f;

                        }
                        break;
                    case "4":
                        {
                            pos.y -= 0.7f;
                            Instantiate(endpoint, pos, transform.rotation, this.transform);
                        }
                        break;
                    case "5":
                        {
                            pos.y -= 0.5f;
                            Instantiate(unbrickcell, pos, transform.rotation, this.transform);
                        }
                        break;
                    case "6":
                        {
                            pos.y -= 0.7f;
                            Instantiate(unbrick, pos, transform.rotation, this.transform);
                        }
                        break;
                    case "70":
                        {
                            pos.y -= 0.7f;
                            Instantiate(unbrickcorner, pos, transform.rotation, this.transform);
                        }
                        break;
                    case "71":
                        {
                            pos.y -= 0.7f;
                            Quaternion rot = Quaternion.Euler(0, 90, 0);
                            Instantiate(unbrickcorner, pos, rot, this.transform);
                        }
                        break;
                    case "72":
                        {
                            pos.y -= 0.7f;
                            Quaternion rot = Quaternion.Euler(0, 180, 0);
                            Instantiate(unbrickcorner, pos, rot, this.transform);
                        }
                        break;
                    case "73":
                        {
                            pos.y -= 0.7f;
                            Quaternion rot = Quaternion.Euler(0, -90, 0);
                            Instantiate(unbrickcorner, pos, rot, this.transform);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        if (startpoint != null)
        {
            if (Player.player != null)
            {                
                Player.player.Setpos(start);
            }
        }


    }
}
