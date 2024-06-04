using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMusic : MonoBehaviour
{
    public AudioClip[] music;
    public AudioClip gameover;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = music[StageSelectButton.SelectStage - 1];
        GetComponent<AudioSource>().Play();
        Fruits.OnGameOver.AddListener(GameOver);
    }

    public void GameOver()
    {
        if (GetComponent<AudioSource>().clip != gameover)
        {
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = gameover;
            GetComponent<AudioSource>().Play();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
