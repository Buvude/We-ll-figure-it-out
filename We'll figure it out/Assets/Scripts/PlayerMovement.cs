using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float vert, horz;
    public GameObject self;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vert = Input.GetAxis("Vertical");
        horz = Input.GetAxis("Horizontal");
        self.transform.Translate( new Vector3(horz, vert)/speed);
    }
}
