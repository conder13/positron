using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTarget : MonoBehaviour
{
    public float spawnTime = 5f;
    public float[] xrange;
    public float[] yrange;
    public GameObject gameController;
   //private Animator animator;
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    public float speed = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    public GameObject explode;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * speed;
    }

    void LateUpdate()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        //move enemy: 
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));

    }

    // Use this for initialization
    /*void Start()
    {
        InvokeRepeating("spawnDeath", spawnTime, spawnTime);
        gameController = GameObject.FindGameObjectWithTag("GameController");
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    */
    void spawnDeath()
    {
        Vector3 spawnPos = new Vector3(Random.Range(xrange[0], xrange[1]), Random.Range(yrange[0], yrange[1]), 0f);
        transform.position = spawnPos;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ball")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            explode.transform.position = collision.transform.position;
            explode.GetComponent<ParticleSystem>().Play();
            gameController.GetComponent<gameController>().cameraAnim.Play("screenshake2");
            Debug.Log("touched death");
            gameController.GetComponent<gameController>().playSound("Death");
            StartCoroutine(gameController.GetComponent<gameController>().End());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        calcuateNewMovementVector();
    }

    /*IEnumerator indicator(Vector3 spawnPos)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        animator.SetBool("spawning",true);
        transform.position = spawnPos;
        yield return new WaitForSecondsRealtime(1);

        gameObject.GetComponent<Collider2D>().enabled = true;
        animator.SetBool("spawning", false);
    }*/
}
