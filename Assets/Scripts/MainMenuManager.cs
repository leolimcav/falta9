using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string levelJogo;
    [SerializeField] private string sceneJogo;

    public void Jogar(){
        SceneManager.LoadScene(levelJogo);
    }

     public void Instrucoes(){
        SceneManager.LoadScene(sceneJogo);
    }
}
