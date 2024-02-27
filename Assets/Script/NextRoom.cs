using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    public GameObject unactive_Crame;
    public GameObject active_Crame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (unactive_Crame.activeInHierarchy == true)
            {
                active_Crame.SetActive(true);
                unactive_Crame.SetActive(false);
            }
        }
    }
}
