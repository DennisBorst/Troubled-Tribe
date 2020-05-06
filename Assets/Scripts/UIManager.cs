using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayTime;
    [SerializeField] private TextMeshProUGUI currentNumberInHand;
    [SerializeField] private Image switchIcon;
    [SerializeField] private Sprite[] itemIcons;
    [Space]
    [SerializeField] private TextMeshProUGUI amountOfPeople;
    [SerializeField] private Slider woodSlider;
    [SerializeField] private Slider foodSlider;
    [SerializeField] private Slider waterSlider;

    private void Start()
    {
        woodSlider.maxValue = ItemCollections.Instance.storage.Wood.maxAmount;
        foodSlider.maxValue = ItemCollections.Instance.storage.Food.maxAmount;
        waterSlider.maxValue = ItemCollections.Instance.storage.Water.maxAmount;
    }

    public void UpdateUIItems(Items item, int amount)
    {
        if (item == Items.Wood)
        {
            woodSlider.value = amount;
        }
        else if (item == Items.Food)
        {
            foodSlider.value = amount;

        }
        else if (item == Items.Water)
        {
            waterSlider.value = amount;
        }
    }

    public void UpdateCurrentItem(Items item, int amount)
    {
        if (item == Items.Wood)
        {
            switchIcon.sprite = itemIcons[0];
            currentNumberInHand.text = amount + "/" + ItemCollections.Instance.hand.Wood.maxAmount;
        }
        else if (item == Items.Food)
        {
            switchIcon.sprite = itemIcons[1];
            currentNumberInHand.text = amount + "/" + ItemCollections.Instance.hand.Food.maxAmount;

        }
        else if (item == Items.Water)
        {
            switchIcon.sprite = itemIcons[2];
            currentNumberInHand.text = amount + "/" + ItemCollections.Instance.hand.Water.maxAmount;
        }
    }

    public void IncreaseDayCount(int dayCount)
    {
        dayTime.text = "Day: " + dayCount;
    }

    public void UpdateAmountOfPeople(int currentAmountOfPeople, int maxAmountOfPeople)
    {
        amountOfPeople.text = currentAmountOfPeople + "/" + maxAmountOfPeople;
    }

    #region Singleton

    private static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }

            return instance;
        }
    }

    #endregion
}
