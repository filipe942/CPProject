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
                
            }

            if(loadGameBtn){
                AudioManager.Instance.PlayMusic(AudioManager.Instance.music);
            }
        }

        private void Update()
        {
            if (loadGameBtn) {
            
                if (!isAnyKeyDown && Input.anyKeyDown) //if any key down
                {
                    SplashScreenClose(); //Splash screen disable
                }
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
            ScenesManager.Instance.LoadSceneWithoutLoading("TaskMenuTodos");
        }
        
        public void Daily()
        {
            ScenesManager.Instance.LoadSceneWithoutLoading("TaskMenuFrequents");
        }

        public void Habits()
        {
            ScenesManager.Instance.LoadSceneWithoutLoading("TaskMenuHabits");
        }

        public void DungeonsScreen()
        {
            ScenesManager.Instance.LoadSceneWithoutLoading("DungeonsScreen");
        }

        public void MainMenu()
        {
            ScenesManager.Instance.LoadSceneWithoutLoading("MainMenu");
        }

        public void Stats()
        {
            ScenesManager.Instance.LoadSceneWithoutLoading("StatsScreen");
        }

        public void CreateToDos()
        {
            ScenesManager.Instance.LoadSceneWithoutLoading("CreateToDos");
        }

        public void CreateDailys()
        {
            ScenesManager.Instance.LoadSceneWithoutLoading("CreateFrequents");
        }

        public void CreateHabits()
        {
            ScenesManager.Instance.LoadSceneWithoutLoading("CreateHabit");
        }

        public void LoadGame()
        {
            if (PlayerPrefs.GetString("Saved_Level") != "")
            {
                ScenesManager.Instance.continueGame = true;
                print(PlayerPrefs.GetString("Saved_Level"));
                ScenesManager.Instance.LoadLoadingScene(PlayerPrefs.GetString("Saved_Level"));
            }
            else
            {
                SaveManager.SaveDungeonFloor("Lvl_0");
                ScenesManager.Instance.LoadLoadingScene("Lvl_0");
            }
                
        }

        //Game quit
        public void Quit()
        {
            Application.Quit();
        }

    }
}