using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/*Attached Canvas object at hierarchy(At Both Scenes) */
public class UIManager : MonoBehaviour
{
    #region Variables
    public TextMeshProUGUI paintingWallProcessText;

    public int minProcess = 0;
    public int maxProcess = 100;
    public int currentProcess;

    public int timer;

    public HealthBar healthBar;

    public TextMeshProUGUI timerText;

    public TextMeshProUGUI paintingAlertText;

    public GameObject playButton;

    public GameObject DragFingerImage;

    public GameObject CongrulationsImage;

    public GameObject leaderboardPanel;

    public List<TextMeshProUGUI> players;

    public static UIManager instance;
    #endregion

    #region Init
    private void Awake()
    {
        instance = this;
        currentProcess = minProcess;
        healthBar.SetMaxHealth(maxProcess);
    }
    #endregion

    #region Updating Leaderboard
    private void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].text = LeaderboardController.instance.players[i].name;

        }
    } 
    #endregion
    #region Displaying Paint Ratio
    public void DisplayPaintingWallPercent(float amount)
    {
        currentProcess = (int)amount;
        healthBar.SetHealth(currentProcess);
        paintingWallProcessText.text = "%" + ((int)amount).ToString();
        if (currentProcess == 100)
        {
            CongrulationsImage.SetActive(true);
            StartCoroutine(OpenNextScene());
        }
    }
    IEnumerator OpenNextScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
    #endregion

    #region Behaviour of Play Button 
    public void PushPlayButton()
    {
        playButton.SetActive(false);
        timerText.gameObject.SetActive(true);
        GameManager.instance.StartCoroutine(GameManager.instance.DelayLoadingObstacles());
        StartCoroutine(TimerTextCount());
    }
    #endregion

    #region Displaying Timer
    IEnumerator TimerTextCount()
    {
        while (timer > 0)
        {
            DragFingerImage.SetActive(true);
            yield return new WaitForSeconds(1f);
            timer--;
            timerText.text = timer.ToString();
            if (timer == 0)
            {
                timerText.gameObject.SetActive(false);
                DragFingerImage.SetActive(false);
            }
        }
    }
    #endregion

    #region Active-Deactive Painting Alert Text
    public IEnumerator DisableAndEnablePaintingAlertText()
    {
        paintingAlertText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        paintingAlertText.gameObject.SetActive(false);
    }
    #endregion
}
