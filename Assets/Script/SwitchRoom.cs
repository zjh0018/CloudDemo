using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoom : MonoBehaviour
{
    private Transform parent;
    private GameObject roomCamera;
    private string roomCameraName;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        roomCameraName = parent.name + " Camera";
        
        roomCamera = parent.Find(roomCameraName).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (roomCamera.activeInHierarchy == true)
            {
                roomCamera.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (roomCamera.activeInHierarchy == false)
            {
                roomCamera.SetActive(true);
            }
        }
    }
}
