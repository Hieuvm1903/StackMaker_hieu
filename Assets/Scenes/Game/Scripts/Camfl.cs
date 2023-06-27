using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camfl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public Vector3 offset;
    public float val;
    void Start()
    {
        if(Player.Instance != null)
        if(Player.Instance.stack != null)
        target = Player.Instance.stack.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Player player = FindObjectOfType<Player>();
        settarget(player.transform);
        Vector3 pos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, pos, val * Time.deltaTime);
        
    }
    public void settarget(Transform target)
    {
        this.target = target.transform;
    }
}
