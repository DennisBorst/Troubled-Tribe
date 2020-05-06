using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float mustSurviveTime;
    [SerializeField] private float timeForEachDay;
    [SerializeField] private float peopleDecreaseTime;
    [SerializeField] private int maxAmountOfPeople;
    [SerializeField] private List<NPC> npcs = new List<NPC>();
    [SerializeField] private AudioSource audioSourceDecreasePeople;


    [Header("Each 10 level increase")]
    [SerializeField] private int increaseHand;
    [SerializeField] private int increaseStorage;
    [SerializeField] private int decreaseItemAmount;
    [SerializeField] private float decreaseItemTimer;
    [SerializeField] private float decreaseSpeed;
    [SerializeField] private ParticleSystem levelCapReachedParticle;

    [Header("Game Over")]
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TextMeshProUGUI endText;

    private float currentSurviveTime;
    private float currentDayTime;
    private int dayCount = 1;

    [HideInInspector] public int currentAmountOfPeople;
    private int currentInfo;
    private int levelCount = 1;
    private float currentPeopleDecreaseTime;

    private AudioSource audioSource;

    void Awake()
    {
        Time.timeScale = 1f;

        audioSource = GetComponent<AudioSource>();

        currentDayTime = timeForEachDay;
        currentAmountOfPeople = maxAmountOfPeople;
        currentInfo = maxAmountOfPeople;
        currentPeopleDecreaseTime = peopleDecreaseTime;
        UIManager.Instance.UpdateAmountOfPeople(currentAmountOfPeople, maxAmountOfPeople);

        instance = this;
    }

    void Update()
    {
        TimeManagement();
    }

    public void TimeManagement()
    {
        currentSurviveTime = Timer(currentSurviveTime);
        currentDayTime = Timer(currentDayTime);

        if (currentDayTime <= 0)
        {
            currentDayTime = timeForEachDay;
            dayCount++;
            levelCount++;
            if(levelCount >= 10) { IncreaseDifficulty(); }
            UIManager.Instance.IncreaseDayCount(dayCount);
        }
    }

    public void IncreaseDifficulty()
    {
        levelCount = 0;
        levelCapReachedParticle.Play();
        audioSource.Play();
        ItemCollections.Instance.hand.Wood.maxAmount += increaseHand;
        ItemCollections.Instance.hand.Food.maxAmount += increaseHand;
        ItemCollections.Instance.hand.Water.maxAmount += increaseHand;
        ItemCollections.Instance.IncreaseCapacity(increaseStorage);
        ItemCollections.Instance.decreaseAmount += decreaseItemAmount;
        ItemCollections.Instance.decreaseItemsTimer -= decreaseItemTimer;
        ItemCollections.Instance.decreaseSpeed += decreaseSpeed;
    }

    public void DecreasePeopleWithSlider(int amountOfSlidersLow)
    {
        currentPeopleDecreaseTime = Timer(currentPeopleDecreaseTime);


        if(currentPeopleDecreaseTime <= 0)
        {
            currentPeopleDecreaseTime = peopleDecreaseTime;

            if(amountOfSlidersLow == 3)
            {
                if(currentAmountOfPeople <= 3) { GameOver(); return; }
            }
            else if(amountOfSlidersLow == 2)
            {
                if (currentAmountOfPeople <= 2) { GameOver(); return; }
            }
            else if(amountOfSlidersLow == 1)
            {
                if (currentAmountOfPeople <= 1) { GameOver(); return; }
            }

            for (int i = 0; i < amountOfSlidersLow; i++)
            {
                DecreasePeople(npcs[currentAmountOfPeople - 1]);
            }
        }
    }

    public void DecreasePeople(NPC npc)
    {
        npcs.Remove(npc);
        currentAmountOfPeople--;
        npc.DieParticle();
        Destroy(npc.gameObject);
        UIManager.Instance.UpdateAmountOfPeople(currentAmountOfPeople, maxAmountOfPeople);
        Camera.main.transform.DOShakePosition(.4f, .3f, 20, 90, false, true);
        if (!audioSourceDecreasePeople.isPlaying) { audioSourceDecreasePeople.Play(); }
    }

    public void Replay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void GameOver()
    {
        //Lost the game
        UIManager.Instance.UpdateAmountOfPeople(0, maxAmountOfPeople);
        Time.timeScale = 0;
        endScreen.SetActive(true);
        endText.text = dayCount + " days.";
        Debug.Log("Game Over");
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }

    #region Singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    #endregion
}