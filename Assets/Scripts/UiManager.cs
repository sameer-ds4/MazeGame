using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelectmenu;


    public delegate void difficulty(string level);
    public static event difficulty levelDiff;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
    }
    
    public void OnClick_PageOpen(string page)
    {
        switch (page)
        {
            case "Play":
                mainMenu.SetActive(false);
                levelSelectmenu.SetActive(true);
                break;
            
            case "LevelSelect":
                levelSelectmenu.SetActive(false);
                levelDiff?.Invoke(page);
                break;
        }
    }
}
