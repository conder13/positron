using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManager : MonoBehaviour
{
    public List<GameObject> steps;
    public GameObject charge;
    public GameObject electron;
    public GameObject final;
    public GameObject left;
    public GameObject right;
    public GameObject pointer;
    public int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
       
        if (current == 2 || current == 3)
        {
            activateCharge();
        }
        else if (current == 1 || current == 4)
        {
            charge.SetActive(false);
        }

        if(current == 3)
        {
            electron.SetActive(true);
        }
        else if (current != 3 && current != 0)
        {
            electron.SetActive(false);
        }

        if(current == 5)
        {
            pointer.SetActive(false);
            final.SetActive(true);
            right.SetActive(false);
            left.SetActive(false);
        }
    }

    public void setup()
    {
        if (GameObject.FindGameObjectWithTag("howtocharge") != null)
        {
            charge = GameObject.FindGameObjectWithTag("howtocharge");
            electron = GameObject.FindGameObjectWithTag("howtoelectron");
            final = GameObject.FindGameObjectWithTag("howtofinal");
            final.SetActive(false);
            pointer.SetActive(true);
            right.SetActive(true);
            left.SetActive(true);
            step(1);
            activate();
        }

    }
    public void step(int step)
    {
        for (int i = 0; i < steps.Count; i++)
        {
            steps[i].SetActive(false);
        }
        steps[step - 1].SetActive(true);
        current = step;
    }

    public void activate()
    {
        charge.GetComponent<ChargeScript>().moveWithMouse = false;

    }

    public void allowMouse()
    {
        charge.GetComponent<ChargeScript>().moveWithMouse = true;
    }

    public void nextStep()
    {
        if (current + 1 != steps.Count+1)
        {
            steps[current - 1].SetActive(false);
            current++;
            steps[current - 1].SetActive(true);
        }

    }
    public void lastStep()
    {

        if (current - 1 != 0)
        {
            steps[current - 1].SetActive(false);
            current--;
            steps[current - 1].SetActive(true);
        }

    }

    public void activateCharge()
    {
        charge.SetActive(true);
    }
}
