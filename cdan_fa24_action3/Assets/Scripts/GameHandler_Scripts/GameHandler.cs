using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    private GameObject player;
    public static int playerHealth = 100;
    public int StartPlayerHealth = 100;
    public TMP_Text healthText;

    public static int gotTokens = 0;
    //public TMP_Text tokensText;

    public bool isDefending = false;

    public static bool stairCaseUnlocked = false;
    //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

    private string sceneName;
    public static string lastLevelDied;  //allows replaying the Level where you died

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        //if (sceneName=="MainMenu"){ //uncomment these two lines when the MainMenu exists
        playerHealth = StartPlayerHealth;
        //}
        updateStatsDisplay();
    }

/*
    public void playerGetTokens(int newTokens)
    {
        gotTokens += newTokens;
        updateStatsDisplay();
    }
*/
    public void playerGetHit(int damage)
    {
        if (isDefending == false)
        {
            playerHealth -= damage;

			//only screenshake when hurt, not when getting health poserup:
			if (damage > 0){
				GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>().ShakeCamera(0.15f, 0.3f);
			}

            if (playerHealth >= 0)
            {
                updateStatsDisplay();
            }
            if (damage > 0)
            {
                player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
            }
        }

        if (playerHealth > StartPlayerHealth)
        {
            playerHealth = StartPlayerHealth;
            updateStatsDisplay();
        }

        if (playerHealth <= 0)
        {
			playerHealth = 0;
            updateStatsDisplay();
			//temp remove death for respawn system:
            //playerDies();
        }
    }

    public void updateStatsDisplay()
    {
        healthText.text = "HEALTH: " + playerHealth;
        //tokensText.text = "GOLD: " + gotTokens;
    }

    public void playerDies()
    {
        player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
        lastLevelDied = sceneName;       //allows replaying the Level where you died
        StartCoroutine(DeathPause());
    }

    IEnumerator DeathPause()
    {
        //player.GetComponent<PlayerMove>().isAlive = false;
        //player.GetComponent<PlayerJump>().isAlive = false;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("EndLose");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Cutscene1");
    }

    // Return to MainMenu
    public void RestartGame()
    {
        Time.timeScale = 1f;
        GameHandler_PauseMenu.GameisPaused = false;
		// Reset all static variables here, for new games:
        playerHealth = StartPlayerHealth;
        SceneManager.LoadScene("MainMenu");
        

    }

    // Replay the Level where you died
    public void ReplayLastLevel()
    {
        Time.timeScale = 1f;
        GameHandler_PauseMenu.GameisPaused = false;
		// Reset all static variables here, for new games:
        playerHealth = StartPlayerHealth;
        SceneManager.LoadScene(lastLevelDied);
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}