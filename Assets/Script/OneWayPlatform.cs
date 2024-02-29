using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private GameObject player;
    private Rigidbody2D playerRb;
    private PlayerInput input;
    private TilemapCollider2D box2D;
    private float waitTime;
    public float pressTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        input = player.GetComponent<PlayerInput>();
        box2D = GetComponent<TilemapCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!input.Down)
        {
            effector.rotationalOffset = 0;
            waitTime = pressTime;
        }
        if (input.Down)
        {
            if(waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = pressTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (playerRb.velocity.y > 0.1f && box2D.isActiveAndEnabled)
        {
            box2D.enabled = false;
        }
        else if(playerRb.velocity.y <= 0 && !box2D.isActiveAndEnabled)
        {
            box2D.enabled = true;
        }
    }
}
