using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Rigidbody2D rb;
    private BoxCollider2D box2D;
    private Transform groundCheck_left, groundCheck_right;
    private Transform wallCheck;
    public PlayerInput input; 
    public LayerMask ground;

    public float runMaxSpeed = 8f;
    
    [Header("Jump参数")]
    public float jumpForce = 10f;
    public bool isJumpInSky;
    public int allowJumpCount;


    private bool isGround;//Player是否位于地面
    private bool isFalling;
    private bool isDashing;
    private bool isHurt;
    private bool stopJump;
    [SerializeField] private bool isWall;//Player是否攀附在墙上
    [SerializeField] private bool wallJump;//区分登墙跳和普通跳跃
    public bool WallJump
    {
        get { return wallJump; }
        set { wallJump = value; }
    }
    private float PlayerMoveScale;//player朝向
    private bool jumpInHead;//玩家踩到怪物头上后再次跳跃


    [Header("Dash参数")]
    public float dashTime = 0.2f;
    public float dashSpeed = 30f;
    public float dashNum;
    private float dashTimeLeft;
    private float lastDasfTiem = -10f;
    private float dashDaceDir;
    private float lastShadowXpos;
    public float distanceBetweenShadows = 1f;
    private float tempGravity;

    [Header("Hurt参数")]
    public float hurtTime = 0.1f;
    private float hurtTimeLeft;
    public float upDistance = 1000f;
    public float downDistance = 1200f;
    private bool isDeath;

    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
        input = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        input.EnablePlayerActions();
        groundCheck_left = transform.Find("GroundCheck_left");
        groundCheck_right = transform.Find("GroundCheck_right");
        wallCheck = transform.Find("WallCheck");
        isJumpInSky = true;
        //isJumpInSky = SaveManager.instance.activeSave.isJumpInSky;
        initJumpNum();
        isHurt = false;
        hurtTimeLeft = hurtTime;
        wallJump = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        isGround = isTouchGround();
        isWall = isGripWall();
        isFalling = rb.velocity.y <= 0f && !isGround;
        if (isGround && !isDashing)
        {
            dashNum = 1;
        }
    }

    //与敌人的碰撞检测处理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (isFalling)
            {
                EnemyManager.instance.ReturnPool(collision.gameObject);
                EnemyManager.instance.setStartTime();
                setJumpInHead(true);
            }
            else
            {
                setPlayerHurt(true);
                box2D.isTrigger = true;
                rb.gravityScale = 0;
            }
        }
    }
    private bool isTouchGround()
    {
        return Physics2D.Raycast(groundCheck_right.position, new Vector2(0, -1), 0.1f, ground) ||
            Physics2D.Raycast(groundCheck_left.position, new Vector2(0, -1), 0.1f, ground);
    }
    private bool isGripWall()
    {
        return Physics2D.Raycast(wallCheck.position, new Vector2(transform.localScale.x, 0), 0.1f, ground) && !isGround && input.AxisX == transform.localScale.x;
    }
    public float MoveSpeed()
    {
        return Mathf.Abs(rb.velocity.x);
    }
    public void initJumpNum()
    {
        if (isJumpInSky)
        {
            allowJumpCount = 2;
        }
        else
        {
            allowJumpCount = 1;
        }
    }

    public void PlayerMove()
    {
        float currendSpeed = input.AxisX * runMaxSpeed;
        setVelocityX(currendSpeed);
        setTransformScale(input.AxisX);
    }
    public void PlayerMove(float value)
    {
        float currendSpeed = input.AxisX * value;
        setVelocityX(currendSpeed);
        setTransformScale(input.AxisX);
    }
    public void Jump()
    {
        if (getJumpInHead())
        {
            setVelocityY(jumpForce * 1.5f);
            setJumpInHead(false);
        }
        else
        {
            if (wallJump)
            {
                setVelocityX(runMaxSpeed * -input.AxisX);
                setTransformScale(-input.AxisX);
            }
            setVelocityY(jumpForce);
            allowJumpCount--;
        }
        
    }
    public void ReadyToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDasfTiem = Time.time;
        tempGravity = rb.gravityScale;
        setVelocity(new Vector2(0, 0));
        rb.gravityScale = 0;
        lastShadowXpos = transform.position.x;
    }
    public void PlayerDash()
    {
        if (isDashing)               
        {
            
            if (dashTimeLeft > 0)
            {
                float currendSpeed = transform.localScale.x * dashSpeed;
                setVelocity(new Vector2(currendSpeed, 0));
                dashTimeLeft -= Time.deltaTime;
                if(Mathf.Abs(transform.position.x - lastShadowXpos) > distanceBetweenShadows)
                {
                    ShadowPool.instance.GetFromPool();
                    lastShadowXpos = transform.position.x;
                }
                dashNum--;
            }
            if(dashTimeLeft <= 0)
            {         
                rb.gravityScale = tempGravity;
                isDashing = false;
            }
        } 
    }
    public void PlayerHurt()
    {
        if (isHurt)
        {
            if (hurtTimeLeft > 0)
            {
                float currendSpeed = upDistance * Time.fixedDeltaTime;
                setVelocityY(currendSpeed);
                hurtTimeLeft -= Time.deltaTime;
            }
            if(hurtTimeLeft <= 0)
            { 
                float currendSpeed = downDistance * Time.fixedDeltaTime * -1;
                setVelocityY(currendSpeed);
            }
        }
    }
    public float getPlayerMoveScale()
    {
        return PlayerMoveScale;
    }
    public void setVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
    public void setVelocityX(float velocityX)
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }
    public void setVelocityY(float velocityY)
    {
        rb.velocity = new Vector2(rb.velocity.x, velocityY);
    }
    public void setTransformScale(float scale)
    {
        if (scale != 0)
        {
            transform.localScale = new Vector3(scale, 1, 1);
        }
    }
    public void setJumpInHead(bool t_or_f)
    {
        jumpInHead = t_or_f;
    }
    public bool getJumpInHead()
    {
        return jumpInHead;
    }
    public void setPlayerHurt(bool t_or_f)
    {
        isHurt = t_or_f;
    }
    public bool playerIsHurt()
    {
        return isHurt;
    }
    public bool getStopJump()
    {
        return stopJump;
    }
    public bool playerIsWall()
    {
        return isWall;
    }
    public bool playerIsAllowJump()
    {
        return (input.Jump || input.JumpInputBuffer) && allowJumpCount > 0;
    }
    public bool playerIsAllowDash()
    {
        return input.Dash && (dashNum > 0);
    }
    public bool playerIsOnGround()
    {
        return isGround;
    }
    public bool playerIsFalling()
    {
        return isFalling;
    }
    public bool playerIsDashing()
    {
        return isDashing;
    }
    public void resetExtraJump()
    {
        if (isGround)
        {
            initJumpNum();
        }
    }
    public void setPlayerDeathState(bool t_or_f)
    {
        isDeath = t_or_f;
    }
    public bool playerIsDeath()
    {
        return isDeath;
    }
    public void SetGravity(float value)
    {
        rb.gravityScale = value;
    }
    public float GetGravity()
    {
        return rb.gravityScale;
    }
}
