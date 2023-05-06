using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header("Pages")]
    public GameObject mainMenu;
    public GameObject levelSelectmenu;
    public GameObject ingameUi;
    public GameObject levelcomplete;
    public GameObject levelfail;

    [Header("Text Fields")]
    public Text timeElapsed;
    public Text timeMax;
    public Text coinsCollected;

    public Text coins_lc;
    public Text time_lc;


    private int coins;
    private int timeflow;
    private int maxTime;

    [Header("Events")]
    public static bool gameStart;

    public delegate void difficulty(string level);
    public static event difficulty levelDiff;

    public delegate void onTriggerEvents();
    public static event onTriggerEvents level_fail;




    private void OnEnable()
    {
        levelDiff += SetTimer;
        PlayerController.coincollect_update += UpdateScore;
        PlayerController.level_complete += levelComplete_Show;
        level_fail += LevelFail_Show;
    }

    private void OnDisable()
    {
        levelDiff -= SetTimer;
        PlayerController.coincollect_update -= UpdateScore;
        PlayerController.level_complete -= levelComplete_Show;
        level_fail -= LevelFail_Show;
    }


    void Start()
    {
        ReturnMainMenu();
    }

    private void Update()
    {
        QuitGame();
    }

    private void SetTimer(string page)
    {
        switch(page)
        {
            case "Easy":
                maxTime = 50;
                break;

            case "Medium":
                maxTime = 40;
                break;

            case "Hard":
                maxTime = 35;
                break;
        }
        timeMax.text = maxTime.ToString() + " Secs";
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        timeflow++;
        UpdateTime();

        if(gameStart)
            StartCoroutine(Timer());
    }

    public void OnClick_PageOpen(string page)
    {
        switch (page)
        {
            case "Play":
                mainMenu.SetActive(false);
                levelSelectmenu.SetActive(true);
                break;
            
            case "Easy":
                levelSelectmenu.SetActive(false);
                levelDiff?.Invoke(page);
                GameStart();
                break;
            
            case "Medium":
                levelSelectmenu.SetActive(false);
                levelDiff?.Invoke(page);
                GameStart();
                break;
            
            case "Hard":
                levelSelectmenu.SetActive(false);
                levelDiff?.Invoke(page);
                GameStart();
                break;
        }
    }

    private void GameStart()
    {
        gameStart = true;

        ingameUi.SetActive(true);
        StartCoroutine(Timer());
        CursorSwich();
    }

    private void ReturnMainMenu()
    {
        levelcomplete.SetActive(false);
        levelfail.SetActive(false);
        levelSelectmenu.SetActive(false);
        ingameUi.SetActive(false);

        mainMenu.SetActive(true);
    }

    private void UpdateTime()
    {
        timeElapsed.text = timeflow.ToString() +" Secs";

        if (timeflow >= maxTime)
        {
            //Debug.LogError(maxTime);
            level_fail?.Invoke();
        }
    }

    private void UpdateScore()
    {
        coins++;
        coinsCollected.text = coins.ToString();
    }    

    private void levelComplete_Show()
    {
        gameStart = false;
        levelcomplete.SetActive(true);
        time_lc.text = timeElapsed.text;
        coins_lc.text = coinsCollected.text;
        CursorSwich();
    }

    private void LevelFail_Show()
    {
        levelfail.SetActive(true);
        gameStart = false;
        CursorSwich();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    private void CursorSwich()
    {
        if (Cursor.visible)
            Cursor.visible = false;
        else
            Cursor.visible = true;
    }

    void QuitGame()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
}
