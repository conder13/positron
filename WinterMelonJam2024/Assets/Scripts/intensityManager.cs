using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class intensityManager : MonoBehaviour
{
    public float intensity = 1;
    public float lastIncrement = 1;
    public float increment = 0.05f;
    public float maxPower = 10f;
    public GameObject charge;
    public GameObject target;
    public GameObject death;
    public GameObject clear;
    public GameObject gameController;
    public float speed;
    public int amountOfDeaths;
    public float pointsPer = 1;
    public float speedOfDeaths;
    public float combo = 0;
    public float neededForClear = 2;
    public int maxDeath = 10;
    public float lastScore = 0;
    public float time = 0;
    public float bonusScore = 3f;
    public float deathAsk = 0.15f;
    public float deathIncrement = 0.025f;
    public float bonusSecs = 1.5f;
    public int maxPointsPer = 15;
    public float roundIncrease = 1f;
    public float roundIncreaseIncrease = 0.5f;
    public float powerIncrease = 1f;
    public float enemySpeedIncrease = 0.25f;
    void Start()
    {
        death = ObjectPool.SharedInstance.GetPooledObject();
        death.transform.position = new Vector3(5f,1f,0f);
        death.SetActive(true);

        clear.SetActive(false);
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

    }
    
    public void increaseIntensity()
    {
        if (amountOfDeaths <= maxDeath)
        {

            intensity += increment;
            if (intensity - lastIncrement >= deathAsk)
            {
                GameObject newDeath = ObjectPool.SharedInstance.GetPooledObject();
                if (newDeath != null)
                {
                    newDeath.transform.position = death.transform.position;
                    newDeath.transform.rotation = death.transform.rotation;
                    newDeath.SetActive(true);
                }
                amountOfDeaths++;
                deathAsk -= deathIncrement;
                lastIncrement = intensity;
            }
            if (intensity >= neededForClear)
            {
                clear.SetActive(true );
                Debug.Log("clear spawn");
            }
        }
    }

    public int score()
    {
        pointsPer++;
        combo++;
        int score = 0;
        if(time -  lastScore <= bonusSecs)
        {
            score = (int)((pointsPer +bonusScore)*combo);
        }
        else
        {
            score = (int)(pointsPer * combo);

        }
        lastScore = time;
        increaseIntensity();
        return score;
    }

    public void miss()
    {
        combo = 0;
        lastScore = time;
    }

    public void clearScreen()
    {
        clear.SetActive(false);
        gameController.GetComponent<ObjectPool>().removePool();
        gameController.GetComponent<gameController>().cameraAnim.Play("screenshake3");
        gameController.GetComponent<gameController>().playSound("Clear");
        neededForClear += roundIncrease;
        roundIncrease += roundIncreaseIncrease;
        amountOfDeaths = 1;
        charge.GetComponent<ChargeScript>().power += powerIncrease;
        gameController.GetComponent<ObjectPool>().increaseSpeed(enemySpeedIncrease);
        pointsPer += 5;

    }
}
