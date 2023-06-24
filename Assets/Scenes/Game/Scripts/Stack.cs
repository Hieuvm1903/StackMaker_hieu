using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
      

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Brick")
        {
            other.gameObject.tag = "Normal";
            Player.player.Addbrick(other.gameObject);
            other.gameObject.AddComponent<Stack>();
            //Destroy(this);
        }
        if(other.tag == "Wall")
        {
            
            Player.player.Stop();
            Player.player.Centerx();
            Player.player.Centerz();

        }
        if(other.tag == "Unbrickwall")
        {
            if(Player.player.stacks > 0 )
            {
                Destroy(other.gameObject);
            }
            else
            {
                Player.player.Stop();
                Player.player.Centerx();
                Player.player.Centerz();
            }

        }
    }
    public void Ondespawn()
    {
        Destroy(this);
    }
}
