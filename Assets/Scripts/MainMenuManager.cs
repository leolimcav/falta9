using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string levelJogo;

    public void Jogar(){
        SceneManager.LoadScene(levelJogo);
    }
}
