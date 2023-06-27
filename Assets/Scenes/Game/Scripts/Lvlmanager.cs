using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvlmanager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;
    [SerializeField] GameObject map;
    public string Lvl  ;
    public int num;
    public static Lvlmanager instance;
    public static Lvlmanager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Lvlmanager>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<Lvlmanager>();
                }
            }
            return instance;
        }



    }
    void Awake()
    {

        replay();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changelvl()
    {
        if ( num < 5)
        {
            num++;
            Lvl = "Lvl" + num.ToString();
            set();

        }
    }
    public void replay()
    {
        num = 1;
        Lvl = "Lvl" + num.ToString();
        set();

    }
    public void set()
    {
        if (Createlvl.Instance != null)
        {
            Player.Destroy(Player.Instance.gameObject);
            Player.Destroy(Player.Instance);

           Createlvl.Destroy(Createlvl.Instance.gameObject);
            Createlvl.Destroy(Createlvl.Instance);

            
        }
        
        Instantiate(map, transform.position, transform.rotation);
        //Instantiate(player, map.GetComponent<Createlvl>().startpoint.transform.position, transform.rotation);

    }
}
