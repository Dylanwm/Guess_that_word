using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public MenuStates menuState;
    public GameObject[] panels;                                         //what the different panels are stored in
    public GameplayManager gameplayManager;
    void Start()
    {
        PanelChange(0);                                                 //when the game first loads "mainMenu" panel will be displayed
        gameplayManager.SelectHiddenWord();
    }

    public void PanelChange(int z)                                             //the function responsible for changing between different panels
    {
        menuState = (MenuStates)z;                                      
        switch (menuState)
        {
            case MenuStates.mainMenu:
                for (int i = 0; i < panels.Length; i++)                 //for each panel
                {
                    panels[i].SetActive(false);                         //deactive/hide
                }
                panels[0].SetActive(true);                              //set only the main menu active
                gameplayManager.isPlaying = false;
                break;
            case MenuStates.pauseMenu:                                  //sets the pause menu active
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(false);
                }
                panels[1].SetActive(true);
                gameplayManager.isPlaying = false;
                break;
            case MenuStates.gameplay:                                   //sets the gameplay panel
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(false);
                }
                panels[2].SetActive(true);
                gameplayManager.isPlaying = true;
                break;
            case MenuStates.gameOverWon:                                //sets the Game Won panel
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(false);
                }
                panels[3].SetActive(true);
                gameplayManager.isPlaying = false;
                break;
            case MenuStates.gameOverLost:                               //sets the Game Lost panel
                for (int i = 0; i < panels.Length; i++)
                {
                    panels[i].SetActive(false);
                }
                panels[4].SetActive(true);
                gameplayManager.isPlaying = false;
                break;
        }
    }


}

public enum MenuStates
{
    mainMenu,
    pauseMenu,
    gameplay,
    gameOverWon,
    gameOverLost,
}
