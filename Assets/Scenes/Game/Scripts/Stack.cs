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
            Player.Instance.Addbrick(other.gameObject);
            other.gameObject.AddComponent<Stack>();
            //Destroy(this);
        }
        

        if(other.tag == "Wall")
        {
            
            Player.Instance.Stop();
            Player.Instance.Centerx();
            Player.Instance.Centerz();

        }
        if(other.tag == "Unbrickwall")
        {
            if(Player.Instance.stacks > 0 )
            {
                Destroy(other.gameObject);
            }
            else
            {
                Player.Instance.Stop();
                Player.Instance.Centerx();
                Player.Instance.Centerz();
            }

        }
    }
    public void Ondespawn()
    {
        Destroy(this);
    }
}
