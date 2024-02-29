
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform groundCheck_left, groundCheck_right;
    public LayerMask ground;
    public bool isGround;
    public bool isFalling;
    public float jumpHeath;
    public float jumpCD;
    private void OnEnable()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck_left = transform.Find("GroundCheck_left");
        groundCheck_right = transform.Find("GroundCheck_right");
    }

    // Update is called once per frame
    void Update()
    {
        isGround = isTouchGround();
        isFalling = rb.velocity.y < 0f && !isGround;
    }
    private bool isTouchGround()
    {
        return Physics2D.Raycast(groundCheck_right.position, new Vector2(0, -1), 0.1f, ground) ||
            Physics2D.Raycast(groundCheck_left.position, new Vector2(0, -1), 0.1f, ground);
    }
    public void FrogJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeath * Time.fixedDeltaTime);
    }
}
