using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    Nothing,
    Wood,
    Food,
    Water
}

[System.Serializable]
public class Storage
{
    public Item Wood = new Item();
    public Item Food = new Item();
    public Item Water = new Item();
}

[System.Serializable]
public class Hand
{
    public Item Wood = new Item();
    public Item Food = new Item();
    public Item Water = new Item();
}

[System.Serializable]
public class Item
{
    public int currentAmmount = 0;
    public int maxAmount = 10;
    public int Current
    {
        get { return currentAmmount; }
        set
        {
            currentAmmount = value;
            if (currentAmmount > maxAmount)
            {
                currentAmmount = maxAmount;
            }
            else if (currentAmmount < 0)
            {
                currentAmmount = 0;
            }
        }
    }
    public void Cap()
    {
        maxAmount = 0;
    }
}
public class ItemCollections : MonoBehaviour
{
    public int decreaseAmount;
    public float decreaseSpeed;
    public float decreaseItemsTimer;
    [Space]
    public Storage storage = new Storage();
    public Hand hand = new Hand();

    [HideInInspector] public int currentHandAmount;

    private float currentDecreaseItemsTimer;

    private void Start()
    {
        currentDecreaseItemsTimer = decreaseItemsTimer;

        UIManager.Instance.UpdateUIItems(Items.Wood, storage.Wood.Current);
        UIManager.Instance.UpdateUIItems(Items.Food, storage.Food.Current);
        UIManager.Instance.UpdateUIItems(Items.Water, storage.Water.Current);
    }

    private void Update()
    {
        currentDecreaseItemsTimer = Timer(currentDecreaseItemsTimer);

        if(currentDecreaseItemsTimer < 0)
        {
            currentDecreaseItemsTimer = decreaseItemsTimer;
            storage.Wood.Current -= decreaseAmount;
            storage.Food.Current -= decreaseAmount;
            storage.Water.Current -= decreaseAmount;

            UIManager.Instance.UpdateUIItems(Items.Wood, storage.Wood.Current);
            UIManager.Instance.UpdateUIItems(Items.Food, storage.Food.Current);
            UIManager.Instance.UpdateUIItems(Items.Water, storage.Water.Current);
        }

        if(storage.Wood.Current <= 0 || storage.Food.Current <= 0 || storage.Water.Current <= 0)
        {
            int amountOfSliderLow = 0;
            if(storage.Wood.Current <= 0) { amountOfSliderLow++; }
            if(storage.Food.Current <= 0) { amountOfSliderLow++; }
            if(storage.Water.Current <= 0) { amountOfSliderLow++; }
            Debug.Log(amountOfSliderLow);
            GameManager.Instance.DecreasePeopleWithSlider(amountOfSliderLow);
        }
    }

    public void IncreaseHandItem(Items item, int amount)
    {
        if (item == Items.Wood)
        {
            hand.Wood.Current += amount;
            hand.Food.Current = 0;
            hand.Water.Current = 0;
            UIManager.Instance.UpdateCurrentItem(item, hand.Wood.Current);
        }
        else if (item == Items.Food)
        {
            hand.Food.Current += amount;
            hand.Wood.Current = 0;
            hand.Water.Current = 0;
            UIManager.Instance.UpdateCurrentItem(item, hand.Food.Current);
        }
        else if (item == Items.Water)
        {
            hand.Water.Current += amount;
            hand.Wood.Current = 0;
            hand.Food.Current = 0;
            UIManager.Instance.UpdateCurrentItem(item, hand.Water.Current);
        }
    }

    public void IncreaseItem(Items item, int amount)
    {
        if (item == Items.Wood)
        {
            storage.Wood.Current += amount;
            UIManager.Instance.UpdateUIItems(item, storage.Wood.Current);
        }
        else if (item == Items.Food)
        {
            storage.Food.Current += amount;
            UIManager.Instance.UpdateUIItems(item, storage.Food.Current);
        }
        else if (item == Items.Water)
        {
            storage.Water.Current += amount;
            UIManager.Instance.UpdateUIItems(item, storage.Water.Current);
        }
    }

    public void TransferHandItems(Items item)
    {
        if (item == Items.Wood)
        {
            IncreaseItem(item, hand.Wood.Current);
            hand.Wood.Current = 0;
            UIManager.Instance.UpdateCurrentItem(item, hand.Wood.Current);
        }
        else if (item == Items.Food)
        {
            IncreaseItem(item, hand.Food.Current);
            hand.Food.Current = 0;
            UIManager.Instance.UpdateCurrentItem(item, hand.Food.Current);
        }
        else if (item == Items.Water)
        {
            IncreaseItem(item, hand.Water.Current);
            hand.Water.Current = 0;
            UIManager.Instance.UpdateCurrentItem(item, hand.Water.Current);
        }
    }

    public void IncreaseCapacity(int increase)
    {
        storage.Wood.maxAmount += increase;
        storage.Food.maxAmount += increase;
        storage.Water.maxAmount += increase;
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime * decreaseSpeed;
        return timer;
    }

    #region Singleton

    private static ItemCollections instance;

    private void Awake()
    {
        instance = this;
    }

    public static ItemCollections Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new ItemCollections();
            }

            return instance;
        }
    }

    #endregion
}
