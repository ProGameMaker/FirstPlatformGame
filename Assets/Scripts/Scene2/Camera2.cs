using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public float MAX_X = 5f;
    public float MIN_X = -9f;
    public float MAX_Y = 6f;
    public float MIN_Y = -3f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
        Mathf.Clamp(player.transform.position.x, MIN_X, MAX_X),
        Mathf.Clamp(player.transform.position.y, MIN_Y, MAX_Y),
        -10);
        
    }

}
