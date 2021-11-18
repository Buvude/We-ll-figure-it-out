using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float vert, horz;
    public GameObject self;
    public float Negativespeeduntouched, NegativeSpeed;
    public bool hoeEquiped;
    private List<bool> InventoryItems = new List<bool>();
    private List<bool> InventoryIndex = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        vert = Input.GetAxis("Vertical");
        horz = Input.GetAxis("Horizontal");
        self.transform.Translate( new Vector3(horz, vert)/NegativeSpeed);
        if (Input.GetKey(KeyCode.Alpha1)||Input.GetKey(KeyCode.Keypad1))
        {
            Inventory(1);
        }

    }
    public void Inventory(int index)
    {
        switch (index)
        {
            case 1:

                break;
        }
    }
}
