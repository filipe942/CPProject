﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace DungeonKIT
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Components")]
        public AnimatorManager mainMenuAnimatorManager; //Animation manager

        bool isAnyKeyDown; //splash screen status

        public GameObject loadGameBtn;

        private void Start()
        {
            if (PlayerPrefs.GetString("Saved_Level") != "")
            {
                if(loadGameBtn != null)
                {
                    loadGameBtn.SetActive(true);
                }
            }
                

            AudioManager.Instance.PlayMusic(AudioManager.Instance.music);
        }

        private void Update()
        {
            if (!isAnyKeyDown && Input.anyKeyDown) //if any key down
            {
                SplashScreenClose(); //Splash screen disable
            }
        }
        //Splash screen disable method
        void SplashScreenClose()
        {
            isAnyKeyDown = true;
            mainMenuAnimatorManager.PlayPlayableDirector(mainMenuAnimatorManager.timelineAssets[1], DirectorWrapMode.None); //Play main menu animation
        }
        //New game method
        public void NewGame()
        {
            ScenesManager.Instance.LoadLoadingScene("Lvl_0"); //Load level 1
        }

        public void Tasks()
        {
            ScenesManager.Instance.LoadLoadingScene("TaskMenuTodos");
        }
        
        public void DungeonsScreen()
        {
            ScenesManager.Instance.LoadLoadingScene("DungeonsScreen");
        }

        public void MainMenu()
        {
            ScenesManager.Instance.LoadLoadingScene("MainMenu");
        }

        public void Stats()
        {
            ScenesManager.Instance.LoadLoadingScene("StatsScreen");
        }

        public void CreateToDos()
        {
            ScenesManager.Instance.LoadLoadingScene("CreateToDos");
        }

        public void LoadGame()
        {
            ScenesManager.Instance.continueGame = true;
            ScenesManager.Instance.LoadLoadingScene(PlayerPrefs.GetString("Saved_Level"));
        }

        //Game quit
        public void Quit()
        {
            Application.Quit();
        }

    }
}