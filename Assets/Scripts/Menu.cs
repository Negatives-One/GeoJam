using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject musics;
    public void ToGame()
    {
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(musics);
        musics.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
    }
}
