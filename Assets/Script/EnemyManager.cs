using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject frogPrefab;
    public int frogNum;
    public float enemyCD;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    private float startTime;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        //初始化对象池
        FillPool();
    }
    private void FixedUpdate()
    {
        if (startTime != 0)
        {
            if (Time.time >= startTime + enemyCD)
            {
                GetFromPool();
                startTime = 0;
            }
        }
    }
    private void FillPool()
    {
        for (int i = 0; i < frogNum; i++)
        {
            var newFrog = Instantiate(frogPrefab);
            newFrog.transform.SetParent(transform);

            //取消启用,返回对象池
            ReturnPool(newFrog);
            GetFromPool();
        }
    }
    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        availableObjects.Enqueue(gameObject);
        
    }
    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            FillPool();
        }
        var outFrog = availableObjects.Dequeue();
        outFrog.SetActive(true);
        return outFrog;
    }
    public void setStartTime()
    {
        startTime = Time.time;
    }
}
