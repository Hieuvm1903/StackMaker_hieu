using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager instance;
    public static UImanager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UImanager>();
                if(instance == null)
                {
                    instance = new GameObject().AddComponent<UImanager>();
                }
            }
            return instance;
        }
        


    }

    [SerializeField] Text stackstext;
    [SerializeField] Button replay;
    [SerializeField] Button nextlvl;
    [SerializeField] Button mainmenu;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        replay.gameObject.SetActive(false);
        nextlvl.gameObject.SetActive(false);
        mainmenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void stack(int stacks)
    {
        stackstext.text = "Stacks: " + stacks.ToString();
    }
    public void endgame()
    {
        replay.gameObject.SetActive(true);
        nextlvl.gameObject.SetActive(true);
        mainmenu.gameObject.SetActive( true);
        stackstext.gameObject.SetActive(false);

    }
    public void newgame()
    {
        replay.gameObject.SetActive(false);
        nextlvl.gameObject.SetActive(false);
        mainmenu.gameObject.SetActive(false);
        stackstext.gameObject.SetActive(true);
    }
    public void Start()
    {
        stackstext.gameObject.SetActive(true);
        replay.onClick.AddListener(Replay);
        nextlvl.onClick.AddListener(Nextlvl);
        mainmenu.onClick.AddListener(Mainmenu);
    }
    private void Replay()
    {
        Lvlmanager.Instance.set();
        newgame();
        Debug.Log("replay");
    }
    private void Nextlvl()
    {
        Lvlmanager.Instance.changelvl();
        newgame();
        Debug.Log("nxtlvl");
    }
    private void Mainmenu()
    {
        Lvlmanager.Instance.replay();
        newgame();
        Debug.Log("mainmenu");
    }

}
