using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public static Player player;

    private Vector2 startPos, endPos;
    public Vector3 targetPos;
    private bool istouch;
    
    public float speed = 1.5f;
    private Rigidbody rb;
    public bool ismove = false;
    public int bricks ;
    public int stacks;
    public GameObject foot;
    public GameObject stack;
    public LayerMask layer ;
    public enum Direct
    {
        Forward,
        Backward,
        Left,
        Right,
        None
    };
    public Direct direct = Direct.None;
    public static Player Instance
    {
        get
        {
            if (player == null)
            {
                player = FindObjectOfType<Player>();
                if (player == null)
                {
                    player = new GameObject().AddComponent<Player>();
                }
            }
            return player;
        }



    }
    void Update()
    {
        if (MobileInput.Instance != null)
        {
            if (MobileInput.Instance.swipeLeft)
            {               
                ismove = true;
                direct = Direct.Left;
                Move(direct);
            }
            else if (MobileInput.Instance.swipeRight)
            {               
                ismove = true;
                direct = Direct.Right;
                Move(direct);
            }
            else if (MobileInput.Instance.swipeDown)
            {                
                ismove = true;
                direct = Direct.Backward;
                Move(direct);
            }
            else if (MobileInput.Instance.swipeUp)
            {                
                ismove = true;
                direct = Direct.Forward;
                Move(direct);
            }
            else if (!ismove)
            {                
                direct = Direct.None;
                Move(direct);
                Centerx();
                Centerz();
            }
        }
        if(direct == Direct.Left || direct == Direct.Right)
        { 
             Centerz(); 
        }
        if (direct == Direct.Forward || direct == Direct.Backward)
        { 
            Centerx(); 
        }
        Checkunbrick();
    }
    void touch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            istouch = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            istouch = true;
        }
        if (istouch)
        {
            float deltaX = startPos.x - endPos.x;
            float deltaY = startPos.y - endPos.y;
            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                if (deltaX < 0)
                {
                    //targetPos = transform.position + speed * new Vector3(1, 0, 0);
                    rb.velocity = speed * Vector3.right;
                    Debug.Log("Swipe right");
                    direct = Direct.Right;
                }
                else
                {
                    //targetPos = transform.position + speed * new Vector3(-1, 0, 0);
                    rb.velocity = speed * Vector3.left;

                    Debug.Log("Swipe left");
                    direct = Direct.Left;
                }
            }
            else
            {
                if (deltaY < 0)
                {
                    //targetPos = transform.position + speed * new Vector3(0, 0, 1);
                    rb.velocity = speed * Vector3.forward;

                    Debug.Log("Swipe up");
                    direct = Direct.Forward;
                }
                else
                {
                    //targetPos = transform.position + speed * new Vector3(0, 0, -1);
                    rb.velocity = speed * Vector3.back;

                    Debug.Log("Swipe down");
                    direct = Direct.Backward;

                }
            }
            // Move();
            Debug.Log(bricks);
            istouch = false;
            direct = Direct.None;
        }
    }
    public void Oninit()
    {
        
        rb = GetComponent<Rigidbody>();
        bricks = 0;
        stacks = 0;
        if (UImanager.instance != null)
        {
            UImanager.instance.stack(bricks);
        }
        //Setpos(Createlvl.Instance.startpoint.transform.position);

    }
    void Awake()
    {
        Oninit();
    }
    public void Addbrick(GameObject brick)
    {
        bricks++;
        if (UImanager.instance != null)
        {
           UImanager.instance.stack(bricks);
        }
        stacks++;
        brick.transform.SetParent(foot.transform);
        Vector3 pos = stack.transform.localPosition;
        pos.y -= 0.1f;
        brick.transform.localPosition = pos;
        Vector3 playerpos = transform.position;
        Vector3 stackpos1 = this.stack.transform.position;
    
        stack = brick;
        stack.GetComponent<BoxCollider>().isTrigger = false;
        Vector3 stackpos2 = this.stack.transform.position;
        playerpos.y += stackpos1.y - stackpos2.y;
        transform.position = playerpos;


    }
    public void Checkunbrick()
    {

        Vector3 raypos = this.stack.transform.position;
        
        
            Debug.DrawRay(raypos, Vector3.down * 3f, Color.red, 1f);
            RaycastHit hit;
            Stack stack = Removebrick();
            
            if (Physics.Raycast(raypos, Vector3.down * 3f, out hit, 1f, layer))
            {
            /*
            if(hit.collider.tag == "Brick")
            {
            hit.collider.gameObject.tag = "Normal";
            Addbrick(hit.collider.gameObject);
            hit.collider.gameObject.AddComponent<Stack>();
            }
            
            else 
            */
            if (hit.collider.tag == "Unbrick")
                {
                    if (stacks > 0)
                    {
                     Vector3 stackpos1 = this.stack.transform.position;
                    stacks--;                    
                    stack.transform.SetParent(hit.collider.transform);
                        stack.transform.gameObject.tag = "Untagged";
                        Vector3 pos = new Vector3(0, 0.55f, 0);
                        stack.transform.localPosition = pos;
                        stack.Ondespawn();
                        Vector3 playerpos = transform.position;
                        
                        this.stack = Removebrick().gameObject;
                    Vector3 stackpos2 = this.stack.transform.position;
                    playerpos.y -= stackpos2.y - stackpos1.y;
                    transform.position = playerpos;
                    hit.collider.tag = "Normal";
                    Debug.Log(this.stack.transform.position.y);
                }
                    
                }
                else if(hit.collider.tag == "Endpoint")
                {
                    Stop();
                if(UImanager.instance != null)
                {
                    UImanager.instance.endgame();
                }
                }
            }
        
    }
    public Stack Removebrick()
    {
        List<Stack> lstack = foot.GetComponentsInChildren<Stack>().ToList();
        int n = lstack.Count()-1;
        return lstack[n];
    }
    public void Move(Direct dir)
    {
        switch(dir)
        {
            case Direct.Left:
                {
                    rb.velocity = speed * Vector3.left ;
                }
                break;
            case Direct.Right:
                {
                    rb.velocity = speed * Vector3.right ;
                }
                break;
            case Direct.Backward:
                {
                    rb.velocity = speed * Vector3.back ;
                }
                break;
            case Direct.Forward:
                {
                    rb.velocity = speed * Vector3.forward ;
                }
                break;
            case Direct.None:
                {
                    rb.velocity = Vector3.zero;
                }
                break;
           
        }
    }
    public void Stop()
    {
        //rb.velocity = Vector3.zero;
        ismove = false;        
        direct = Direct.None;
    }
    public void Centerx()
    {
        
        Vector3 pos = transform.position;
        if(Mathf.Abs(pos.x - Mathf.RoundToInt(pos.x))>0.001)
        {
            pos.x = Mathf.RoundToInt(pos.x);
            transform.position = pos;
        }
        
    }
    public void Centerz()
    {

        Vector3 pos = transform.position;
        if (Mathf.Abs(pos.z - Mathf.RoundToInt(pos.z)) > 0.001)
        {
            pos.z = Mathf.RoundToInt(pos.z);
            transform.position = pos;
        }
     
    }

    public void Setpos(Vector3 pos)
    {
        transform.position = pos;
    }
}
    // Update is called once per frame


