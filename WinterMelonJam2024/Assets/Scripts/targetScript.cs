using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class targetScript : MonoBehaviour
{
    public string gameState = "play";
    public int x = -1;
    public float[] xrange;
    public float[] yrange;
    public GameObject charge;
    public GameObject intensityManager;
    public GameObject gameController;
    public TextMeshProUGUI scoreText;
    public GameObject redzap;
    public GameObject bluezap;
    public int score = 0;
    public int pointsPer = 1;
    public float combo = 0;
    public float speedMultiplier = 5;
    public float lastTime = 0;
    public float time = 0;
    public Sprite red;
    public Sprite blue;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = red;
        transform.position = new Vector3(Random.Range(xrange[0], xrange[1]), Random.Range(yrange[0], yrange[1]), 0);
        scoreText = GameObject.FindGameObjectWithTag("scoreText").GetComponent<TextMeshProUGUI>();
        intensityManager = GameObject.FindGameObjectWithTag("IntensityManager");
        gameController = GameObject.FindGameObjectWithTag("GameController");

        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        speedMultiplier = intensityManager.GetComponent<intensityManager>().intensity;
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(time-lastTime > 0.1)
        {
            Debug.Log("HITTTT");
            Debug.Log("charge: " + charge.GetComponent<SpriteRenderer>().sprite.name + ", target: " + gameObject.GetComponent<SpriteRenderer>().sprite.name);
            if (collision.tag == "ball")
            {
                if ((charge.GetComponent<SpriteRenderer>().sprite.name == "plus1" && gameObject.GetComponent<SpriteRenderer>().sprite.name == "bluephoton1") || (charge.GetComponent<SpriteRenderer>().sprite.name == "minus1" && gameObject.GetComponent<SpriteRenderer>().sprite.name == "redphoton1"))
                {
                    Debug.Log("yes");
                    
                    if(gameObject.GetComponent<SpriteRenderer>().sprite.name == "redphoton1")
                    {
                        redzap.transform.position = transform.position;
                        redzap.GetComponent<ParticleSystem>().Play();
                    }
                    else
                    {
                        bluezap.transform.position = transform.position;
                        bluezap.GetComponent<ParticleSystem>().Play();
                    }

                    score += intensityManager.GetComponent<intensityManager>().score();
                    gameController.GetComponent<gameController>().playSound("Score");
                    gameController.GetComponent<gameController>().cameraAnim.Play("screenshake1");
                    scoreText.text = score.ToString();
                }
                else
                {
                    gameObject.GetComponent<Animator>().Play("miss");
                    intensityManager.GetComponent<intensityManager>().miss();
                    gameController.GetComponent<gameController>().playSound("Miss");


                }
                x = Random.Range(0, 2);
                if (x < 0.5)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = red;
                }
                else if (x >= 0.5)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = blue;
                }
                transform.position = new Vector3(Random.Range(xrange[0], xrange[1]), Random.Range(yrange[0], yrange[1]), 0);
                lastTime = time;
            }
        }
        
        
    }
}
