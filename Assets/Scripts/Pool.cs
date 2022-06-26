using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int amount;
    public bool expandable;
}

public class Pool : MonoBehaviour
{
    public static Pool singleton;
    public List<PoolItem> items;
    public List<GameObject> pooledItems;
    public GameObject ship;
    public GameObject healthBar;
    public bool gameIsActive;
    public GameObject restartBtn;

    private void Awake()
    {
        singleton = this;
    }

    public GameObject Get(string tag)
    {
        for (var i = 0; i < pooledItems.Count; i++)
        {
            if (!pooledItems[i].activeInHierarchy && pooledItems[i].tag == tag)
            {
                return pooledItems[i];
            }
        }

        foreach (PoolItem item in items)
        {
            if (item.prefab.tag == tag && item.expandable)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
                return obj;
            }
        }
        return null;
    }
    void Start()
    {
        pooledItems = new List<GameObject>();
        foreach (PoolItem item in items)
        {
            for (var i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pooledItems.Add(obj);
            }
        }
        GameActivator(true);
    }

    public void RestartGame()
    {
        healthBar.GetComponent<Slider>().value = 100;
        ship.transform.position = new Vector3(0, ship.transform.position.y, 0);
        foreach (var item in pooledItems)
        {
            item.SetActive(false);
        }
        GameActivator(true);
    }

    public void GameActivator(bool x)
    {
        healthBar.SetActive(x);
        ship.SetActive(x);
        gameIsActive = x;
        restartBtn.SetActive(!x);
    }
}
