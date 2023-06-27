using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update

    public static Gamemanager instance;
    public static Gamemanager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Gamemanager>();
                if (instance == null)
                {
                    instance = new GameObject().AddComponent<Gamemanager>();
                }
            }
            return instance;
        }



    }
    [SerializeField] Lvlmanager lvlmanager;
    [SerializeField] Button btn;
    void Start()
    {
        btn.onClick.AddListener(onclick);
        Instantiate(lvlmanager, Vector3.zero, transform.rotation);
        //btn.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void onclick()
    {
        //Instantiate(lvlmanager, Vector3.zero, transform.rotation);
        GetComponentInParent<Image>().enabled = false;
        btn.gameObject.SetActive(false);
    }
}
