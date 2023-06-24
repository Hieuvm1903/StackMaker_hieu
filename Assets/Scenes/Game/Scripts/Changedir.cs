using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changedir : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Normal")
        {
            float deltax = Player.player.transform.position.x - transform.position.x;
            float deltaz = Player.player.transform.position.z - transform.position.z;
            Player.Direct dir = Player.player.direct;
            if (dir != Player.Direct.None)
            {
                //-90
                if (deltax > 0 && deltaz > 0)
                {

                    if (dir == Player.Direct.Left)
                    {
                        dir = Player.Direct.Forward;
                    }
                    else if (dir == Player.Direct.Backward)
                    {
                        dir = Player.Direct.Right;
                    }
                }
                //0
                else if (deltax > 0 && deltaz <0)
                {
                    if (dir == Player.Direct.Forward)
                    {
                        dir = Player.Direct.Right;
                    }
                    else if (dir == Player.Direct.Left)
                    {
                        dir = Player.Direct.Backward;
                    }
                }
                //180
                else if (deltax < 0 && deltaz > 0)
                {
                    if (dir == Player.Direct.Right)
                    {
                        dir = Player.Direct.Forward;
                    }
                    else if (dir == Player.Direct.Backward)
                    {
                        dir = Player.Direct.Left;
                    }
                }
                //90
                else if (deltax <0 && deltaz < 0)
                {
                    if (dir == Player.Direct.Right)
                    {
                        dir = Player.Direct.Backward;
                    }
                    else if (dir == Player.Direct.Forward)
                    {
                        dir = Player.Direct.Left;
                    }
                }
            }
            if(Player.player.ismove == true)
            {
                if(dir == Player.Direct.Left || dir == Player.Direct.Right)
                {
                    Player.player.Centerz();
                }
                else if (dir == Player.Direct.Forward || dir == Player.Direct.Backward)
                {
                    Player.player.Centerx();
                }
            }
            else
            {
                Player.player.Centerz();
                Player.player.Centerx();
            }
            Player.player.direct = dir;
            Player.player.Move(dir);        

        }

     }
}
