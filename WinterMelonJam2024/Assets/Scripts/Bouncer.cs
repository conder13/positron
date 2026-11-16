using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float power = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ball" || collision.gameObject.tag == "enemy")
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * power, ForceMode2D.Impulse);

        }
    }
}
