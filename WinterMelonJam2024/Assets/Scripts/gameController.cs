using LeaderboardCreatorDemo;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;


public class gameController : MonoBehaviour
{
    public GameObject startingMats;
    public GameObject gameUI;
    public GameObject gameMats;
    public GameObject menuMats;
    public GameObject endMats;
    public GameObject leader;
    public GameObject howto;
    public GameObject howtoMats;
    public GameObject settingsMats;
    public GameObject blocker;

    public int highscore = 0;
    public int score;
    public bool inMenu = true;
    public GameObject pointer;
    public GameObject musicSlide;
    public GameObject sfxSlide;
    public Animator cameraAnim;
    public Animator fade;
    public Sound[] sounds;
    public float musicVol = 1f;
    public float sfxVol = 1f;
    public float fadeSpeed = 1.0f;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start()
    {
        UnityEngine.Cursor.visible = false;
        pointer.SetActive(true);
        playSound("Lobby");
        Destroy(gameMats);

        menu();
    }

    // Update is called once per frame
    void Update()
    {
        if (inMenu)
        {
            
            pointer.SetActive(true);
            menuMats.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "High Score: " + highscore.ToString();

        }

    }
    public void settings()
    {
        inMenu = true;
        menuMats.SetActive(false);
        settingsMats.SetActive(true);
    }
    public void howTo()
    {
        inMenu = false;
        menuMats.SetActive(false);
        howto.SetActive(true );
        Instantiate(howtoMats);
        howto.transform.GetChild(1).GetComponent<tutorialManager>().setup();

    }
    public void menu()
    {
        blocker.SetActive(false);
        settingsMats.SetActive(false);
        inMenu = true;
        howto.SetActive(false);
        fade.Play("fadein");
        inMenu = true;
        Debug.Log("menu");
        menuMats.SetActive(true);
        endMats.SetActive(false);
    }

    public void game()
    {

        blocker.SetActive(true);
        StartCoroutine(Game());

    }
    IEnumerator Game()
    {
        fade.Play("fadeout");

        yield return new WaitForSeconds(0.8f);
        StartCoroutine(FadeOutOldMusic_FadeInNewMusic("Lobby", "Game Music", true));

        fade.Play("fadein");
        inMenu = false;
        blocker.SetActive(false);
        pointer.SetActive(false);
        Debug.Log("game");
        menuMats.SetActive(false);
        endMats.SetActive(false);
        gameMats = Instantiate(startingMats);
        gameUI.SetActive(true);


    }

    public IEnumerator End()
    {
        blocker.SetActive(false);
        fade.Play("fadeout");
        Destroy(gameMats);

        StartCoroutine(FadeOutOldMusic_FadeInNewMusic("Game Music", "Lobby", true));

        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<ObjectPool>().removePool();

        fade.Play("fadein");

        leader.GetComponent<LeaderboardManager>().LoadEntries();
        


        inMenu = true;
        int.TryParse(gameUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text, out score);
        Debug.Log(gameUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        Debug.Log(score);
        gameUI.SetActive(false);
        endMats.SetActive(true);
        endMats.transform.GetChild(4).gameObject.SetActive(false);
        endMats.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = score.ToString();
        if (score >= highscore)
        {
            Debug.Log("ff");
            highscore = score;
            endMats.transform.GetChild(4).gameObject.SetActive(true);
        }

    }




    public void playSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s.source.clip.length >= 10)
        {
            s.source.time = UnityEngine.Random.Range(0f, s.source.clip.length);
        }
        s.source.Play();
    }

    public void endSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public IEnumerator FadeOutOldMusic_FadeInNewMusic(string name1, string name2, bool ran)
    {
        Sound s1 = Array.Find(sounds, sound => sound.name == name1);
        Sound s2 = Array.Find(sounds, sound => sound.name == name2);

        s2.volume = 0.000f;
        playSound(name2);
        while (s2.volume < 1f)
        {
            s1.volume -= fadeSpeed * 2;
            s2.volume += fadeSpeed * 2;
            //Debug.Log("Fade: " + lastTrackName + " " + _audioSources[lastTrackIndex].volume.ToString() + " Rise: " + playingTrackName + " " + _audioSources[playingTrackIndex].volume.ToString());
            yield return new WaitForSeconds(0.001f);
        }
        s1.volume = 0.000f; // Just In Case....
        s1.source.Stop();
    }

    public void changeVolume()
    {
        foreach (Sound s in sounds)
        {
            if (s.source.time > 5)
            {
                s.source.volume = s.volume * musicSlide.GetComponent<Slider>().value;
            }
            else if (s.source.time < 5)
            {
                s.source.volume = s.volume * sfxSlide.GetComponent<Slider>().value;

            }
        }
    }

    public void toggleScreenShake()
    {
        if (cameraAnim.isActiveAndEnabled)
        {
            cameraAnim.enabled = false;
        }
        else
        {
            cameraAnim.enabled = true;
        }
    }
}
