using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargeScript : MonoBehaviour
{
    public float charge = 1;
    public Vector3 mousePos;
    public float speed = 1f;
    public GameObject ball;
    public float power = 1f;
    public Sprite red;
    public Sprite blue;
    public gameController controller;
    public float time = 0;
    public float lastSwitch = 0;
    public bool moveWithMouse = true;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = blue;
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (Input.touchCount > 1)
        {
            if(time - lastSwitch > 0.1)
            {
                if (charge > 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = blue;
                    controller.playSound("SwitchBlue");
                    charge = -1;

                }
                else if (charge < 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = red;
                    controller.playSound("SwitchRed");

                    charge = 1;

                }
                Debug.Log(charge);
                lastSwitch = time;
            }
            
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (moveWithMouse && Input.touchCount < 2)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = Vector2.Lerp(transform.position, mousePos, speed);
        }
        ball.GetComponent<Rigidbody2D>().AddForce((ball.gameObject.transform.position - transform.position) * power * charge * (1f / Vector3.Distance(ball.transform.position, transform.position)), ForceMode2D.Impulse);
    }
}
