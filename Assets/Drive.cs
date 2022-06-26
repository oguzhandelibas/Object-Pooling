using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject bullet;
    public Slider healthbar;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("asteroid"))
        {
            healthbar.value -= 25;
            if (healthbar.value <= 0)
            {
                Destroy(healthbar.gameObject, 0.1f);
                Destroy(gameObject, 0.1f);
            }
        }
    }

    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);

        if (Input.GetKeyDown("space"))
        {
            //Instantiate(bullet, transform.position, Quaternion.identity);
            GameObject b = Pool.singleton.Get("bullet");
            if (b != null)
            {
                b.transform.position = this.transform.position;
                b.SetActive(true);
            }
        }

        if (healthbar != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position)
            + new Vector3(0, -130, 0);
            healthbar.transform.position = screenPos;
        }

    }
}