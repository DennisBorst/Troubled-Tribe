using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTool : MonoBehaviour
{
    [SerializeField] private List<GameObject> tools = new List<GameObject>();
    [SerializeField] private Material[] materials;
    [SerializeField] private GameObject textPanel;

    private int currentTool;
    private int previousTool;
    private bool colliding;
    private bool selecting;

    private PlayerMovement player;

    private void Start()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = Color.white;
        }

        textPanel.SetActive(false);
    }

    private void Update()
    {
        if (!colliding) { textPanel.SetActive(false); return; }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            selecting = true;
            if (!selecting) 
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i].color = Color.white;
                }
                return; 
            }

            if (player.toolEquiped)
            {
                player.DisableTool(currentTool);

                for (int i = 0; i < tools.Count; i++)
                {
                    tools[i].SetActive(true);
                }
            }
        }

        if (selecting)
        {
            player.isBusy = true;
            ToolSelection();
            EquipedTool();
            textPanel.SetActive(false);
        }
        else
        {
            player.isBusy = false;
            textPanel.SetActive(true);
        }
    }

    private void ToolSelection()
    {
        tools[previousTool].transform.localScale = new Vector3(2f, 2f, 2f);
        materials[previousTool].color = Color.white;
        materials[previousTool].SetFloat("_Metallic", 0);

        if (Input.GetKeyDown(KeyCode.Alpha1)) { currentTool = 0; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { currentTool = 1; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { currentTool = 2; }

        float inputScroll = Input.GetAxis("Mouse ScrollWheel");

        if (inputScroll > 0.1f)
        {
            if (currentTool >= 1)
            {
                currentTool--;
            }
            else { currentTool = 2; }
        }
        else if (inputScroll < -0.1f)
        {
            if (currentTool < tools.Count - 1)
            {
                currentTool++;
            }
            else { currentTool = 0; }
        }

        previousTool = currentTool;
        tools[currentTool].transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
        materials[currentTool].color = Color.green;
        materials[currentTool].SetFloat("_Metallic", 1);
    }

    private void EquipedTool()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            selecting = false;
            tools[currentTool].SetActive(false);
            textPanel.SetActive(false);
            player.EquipedTool(currentTool);

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color = Color.white;
                materials[i].SetFloat("_Metallic", 0);
            }

            if (currentTool == 0) { UIManager.Instance.UpdateCurrentItem(Items.Wood, ItemCollections.Instance.hand.Wood.Current); }
            if(currentTool == 1) { UIManager.Instance.UpdateCurrentItem(Items.Food, ItemCollections.Instance.hand.Food.Current); }
            if(currentTool == 2) { UIManager.Instance.UpdateCurrentItem(Items.Water, ItemCollections.Instance.hand.Water.Current); }
        }
    }

    #region Collision
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            colliding = true;
            player = collision.gameObject.GetComponent<PlayerMovement>();

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            colliding = false;
        }
    }
    #endregion
}
