using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour{

    [SerializeField] float splashScreenWaitTime;
    
    //constants
    const string MAIN_MENU_NAME = "MainMenu";
    const string GAME_OVER_SCENE_NAME = "GameOver";
    const string SPLASH_SCREEN_NAME = "SplashScreen";

    //states
    int currentSceneIndex;
    // Start is called before the first frame update
    void Start(){
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0 && SceneManager.GetActiveScene().name == SPLASH_SCREEN_NAME){
            Debug.Log("We're getting there");
            loadMainMenu(splashScreenWaitTime);
        }
    }

    public void loadScene(int sceneIndex) {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }


    public void loadScene(string sceneName) {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void loadScene(int sceneIndex, float delay) {
        StartCoroutine(loadWithDelay(sceneIndex, delay));
    }


    public void loadScene(string sceneName, float delay) {
        StartCoroutine(loadWithDelay(sceneName, delay));
    }

    public void loadNextScene(float delay) {
        if (currentSceneIndex +1 < (SceneManager.sceneCountInBuildSettings)) {
            loadScene(currentSceneIndex + 1, delay);
        } else {
            loadMainMenu(delay);
        }
    }

    public void loadNextScene() {
        if (currentSceneIndex +1 < (SceneManager.sceneCountInBuildSettings)) {
            loadScene(currentSceneIndex +1);
        } else {
            loadMainMenu();
        }
    }

    public void loadGameOver(float delay) {
        loadScene( GAME_OVER_SCENE_NAME, delay);
    }

    public void reloadScene() {
        Time.timeScale = 1;
        loadScene(currentSceneIndex);   
    }

    public void reloadScene(float delay) {
        loadScene(currentSceneIndex, delay);   
    }

    public void loadMainMenu() {
        Time.timeScale = 1;
        loadScene(MAIN_MENU_NAME);
    }

    public void loadMainMenu(float delay) {
        loadScene(MAIN_MENU_NAME, delay);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void QuitGame(float delay) {
        StartCoroutine(QuitWithDelay(delay));
    }

    private IEnumerator QuitWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }

    private IEnumerator loadWithDelay(int sceneIndex, float secondsDelay) {
        yield return new WaitForSeconds(secondsDelay);
        Time.timeScale = 1f;
        loadScene(sceneIndex);
    }

    private IEnumerator loadWithDelay(string sceneName, float secondsDelay) {
        yield return new WaitForSeconds(secondsDelay);
        Time.timeScale = 1f;
        loadScene(sceneName);
    }


}
