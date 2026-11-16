using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideContinue : MonoBehaviour
{
    public GameObject controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ball")
        {
            controller.GetComponent<gameController>().playSound("Score");
            controller.GetComponent<gameController>().cameraAnim.Play("screenshake1");
            controller.GetComponent<gameController>().menu();
            Destroy(transform.parent.parent.gameObject);
        }
    }
}
