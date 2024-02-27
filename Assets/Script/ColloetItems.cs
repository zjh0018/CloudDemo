using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ColloetItems : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int collectionNum;
    public TMP_Text numbers;
    private BoxCollider2D box2D;
    private Rigidbody2D rb;
    void Start()
    {
        collectionNum = 0;
        box2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SpecialItem")
        {
            Destroy(collision.gameObject);
            PlayerController.instance.isJumpInSky = true;
            PlayerController.instance.initJumpNum();
        }
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            collectionNum++;
            numbers.text = collectionNum.ToString();
        }
        if (collision.tag == "DeadLine")
        {
            PlayerController.instance.setPlayerHurt(true);
            rb.gravityScale = 0;
            Invoke("Restart", 1f);
        } 
        if (collision.tag == "trap")
        {
            PlayerController.instance.setPlayerHurt(true);
            box2D.isTrigger = true;
            rb.gravityScale = 0;
        }
    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
