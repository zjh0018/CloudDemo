using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer shadowSprite;
    private SpriteRenderer playerSprite;
    private Color color;


    public float activeTime;
    public float activeStartTime;

    private float alpha;
    public float alphaSet;//��ʼֵ
    public float alphaMultiplier;//˥���ٶ�

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shadowSprite = GetComponent<SpriteRenderer>();
        playerSprite = GameObject.FindGameObjectWithTag("Sprite").transform.GetComponent<SpriteRenderer>();
        alpha = alphaSet;

        shadowSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;
        transform.rotation = player.rotation;

        activeStartTime = Time.time;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        alpha *= alphaMultiplier;
        color = new Color(0.5f, 0.5f, 1, alpha);
        shadowSprite.color = color;

        if (Time.time >= activeStartTime + activeTime)
        {
            //���ض����
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}
