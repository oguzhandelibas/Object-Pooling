using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject asteroid;

    void Update()
    {
        if (!Pool.singleton.gameIsActive) return;
        if (Random.Range(0, 500) < 3)
        {
            GameObject a = Pool.singleton.Get("asteroid");
            if (a != null)
            {
                a.transform.position = this.transform.position + new Vector3(Random.Range(-9.5f, 9.5f), 0, 0);
                a.SetActive(true);
            }
        }

    }
}
