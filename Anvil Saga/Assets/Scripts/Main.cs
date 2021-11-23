using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*Класс отвечает за UI,SceneManagment*/
public class Main : MonoBehaviour
{
    public Text ScoreText;
    public Text AmmoText;
    public GameObject MainMenu;
    public GameObject GameOverText;
    private bool isPaused = false;
    private bool gameOver = false;

    int Score = 0;

    /// <summary>
    /// Устанавливает текст UI/Ammo
    /// </summary>
    /// <param name="ammo"></param>
    public void SetAmmoText(int ammo)
    {
        AmmoText.text = "Ammo " + ammo.ToString();
    }

    /// <summary>
    /// Добавляет 1 Score UI/Score
    /// </summary>
    public void AddScore()
    {
        Score++;
        ScoreText.text = "Score " + Score.ToString();
    }

    /// <summary>
    /// Мутод позволяет поставить игру на паузу
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    /// <summary>
    /// Отключение паузы
    /// </summary>
    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Рестарт текущей сцены
    /// </summary>
    public void RestartGame()
    {
        gameOver = false;
        ContinueGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Метод вызывается при сценарии проигрыша игрока
    /// </summary>
    public void GameOver()
    {
        gameOver = true;
        ShowMainMenu();
        GameOverText.SetActive(true);
    }

    /// <summary>
    /// Change метод меняет состояние главного меню игры
    /// </summary>
    public void ShowMainMenu()
    {
        if (isPaused)
        {
            if (gameOver) return;
            MainMenu.SetActive(false);
            ContinueGame();
        }
        else
        {
            MainMenu.SetActive(true);
            PauseGame();
        }
        
    }
}
