using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    public PlayerMovement Player;
    public WeaponManager WeaponManager;
    public GameObject instance;
    public Canvas deathMenu;
    public Canvas pauseMenu;
    public Canvas pauseButton;
    public bool GameStartMenu = false;

    public void Start()
    {
        if (!GameStartMenu)
        {
            instance = GameObject.FindGameObjectWithTag("Menu");
            if (instance != this.gameObject)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(gameObject);
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            WeaponManager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponManager>();
        }
    }
    public void Restart()
    {
        Player.HP = 10;
        Player.money = 0;
        Player.transform.position = new Vector3(0, 0, 0);
        WeaponManager.swordLevel = 1;
        WeaponManager.orbLevel = 0;
        WeaponManager.flamethrowerLevel = 0;
        Player.alive = true;
        Player.round = 0;
        Player.playerAnimator.SetBool("IsDying", false);
        Player.GetComponentInChildren<WeaponManager>().ResetWeapon();
        SceneManager.LoadScene("SampleScene");
        Player.OrbHUD.enabled = false;
        Player.FlamethrowerHUD.enabled = false;
        Player.UpdateHud(false, false);
        deathMenu.GetComponent<Canvas>().enabled = false;
        pauseMenu.GetComponent<Canvas>().enabled = false;
        pauseButton.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 1;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.GetComponent<Canvas>().enabled = true;
        pauseButton.GetComponent<Canvas>().enabled = false;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.GetComponent<Canvas>().enabled = false;
        pauseButton.GetComponent<Canvas>().enabled = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
