using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Image fillTime;
    [SerializeField] float maxTime;
    [SerializeField] float timeToAdd;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text scoreText;
    float score;
    float time;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        time = maxTime;
    }
    private void Update()
    {
        if (time < 0)
        {
            time = 0;
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        else
        {
            time -= Time.deltaTime;
            fillTime.fillAmount = time / maxTime;
        }
    }
    public void AddTime()
    {
        time += timeToAdd;
        time = Mathf.Clamp(time,0,maxTime);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    public void AddScore(float toAdd)
    {
        score += toAdd;

        scoreText.text = score.ToString();
    }
}
