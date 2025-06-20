using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITowerManager : MonoBehaviour
{
    public static UITowerManager Instance { get; private set; }

    public GameObject popupUI;
    public RectTransform popupRect;
    private TowerSpot currentSpot;
    private bool skipNextClick = false;


    public GameObject buildTypePopup; // popup chọn loại tháp
    public RectTransform buildTypePopupRect;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        
    }

    private void Start()
    {
        PopupBuildTypeStart();
    }

    private void Update()
    {
        if (popupUI.activeSelf && currentSpot != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(currentSpot.transform.position);
            popupRect.position = screenPos + new Vector3(0, 50f, 0);
        }

        if (popupUI.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (skipNextClick)
            {
                skipNextClick = false;
                return;
            }

            if (!RectTransformUtility.RectangleContainsScreenPoint(popupRect, Input.mousePosition))
            {
                HidePopup();
            }
        }

        if (buildTypePopup.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(buildTypePopupRect, Input.mousePosition))
            {
                buildTypePopup.SetActive(false);
                HidePopup();
            }
        }
    }


    public void HidePopup()
    {
        popupUI.SetActive(false);
        buildTypePopup.SetActive(false);
        currentSpot = null;
    }

    #region Actions Menu

    public void ShowPopup(TowerSpot spot)
    {
        HidePopup();
        currentSpot = spot;
        popupUI.SetActive(true);
        skipNextClick = true;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(spot.transform.position);
        popupRect.position = screenPos + new Vector3(0, 50f, 0);

        Button buildButton = popupUI.transform.Find("BuildButton").GetComponent<Button>();
        Button upgradeButton = popupUI.transform.Find("UpgradeButton").GetComponent<Button>();
        Button destroyButton = popupUI.transform.Find("DestroyButton").GetComponent<Button>();
        buildButton.gameObject.SetActive(!spot.isOccupied);
        upgradeButton.gameObject.SetActive(spot.isOccupied);
        destroyButton.gameObject.SetActive(spot.isOccupied);

        buildButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();
        destroyButton.onClick.RemoveAllListeners();

        buildButton.onClick.AddListener(OnBuildClicked);
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        destroyButton.onClick.AddListener(OnDestroyClicked);
    }

    private void OnBuildClicked()
    {
        buildTypePopup.SetActive(true);

        // Hiện popup tại cùng vị trí popupUI
        buildTypePopupRect.position = popupRect.position;

        // Ẩn popup gốc nếu muốn
        popupUI.SetActive(false);
    }

    private void OnUpgradeClicked()
    {
        currentSpot.UpgradeTower();
        HidePopup();
    }

    private void OnDestroyClicked()
    {
        currentSpot.DestroyTower();
        HidePopup();
    }

    #endregion

    #region Type Of Tower

    private void PopupBuildTypeStart()
    {
        GameObject button = Resources.Load<GameObject>("UI/ButtonTowerType");
        TowerBase[] towers = Resources.LoadAll<TowerBase>("Towers");
        foreach (var tower in towers)
        {
            GameObject newButton = Instantiate(button, buildTypePopupRect);
            newButton.name = tower.name; // Set name to match tower type
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = tower.price.ToString(); // Assuming button has a Text component for display
            if(tower.price > GoldManager.Instance.Gold)
            {
                newButton.GetComponent<Image>().color = Color.gray;
                newButton.GetComponent<Button>().interactable = false; // Disable button if not enough gold
            }
            else
            {
                newButton.GetComponent<Image>().color = Color.white;
                newButton.GetComponent<Button>().interactable = true; // Enable button if enough gold
            }
            
            // Tìm GameObject con chứa Image trong newButton
            Transform childImageTransform = newButton.transform.Find("Image");
            if (childImageTransform != null)
            {
                Image childImage = childImageTransform.GetComponent<Image>();
                if (childImage != null)
                {
                    childImage.sprite = tower.sprite.sprite;
                }
            }

            // Add listener for button click
            Button btnComponent = newButton.GetComponent<Button>();
            btnComponent.onClick.AddListener(() => OnSelectTowerType(tower.name));
        }
    }

    public void ShowPopupBuildType()
    {
        HidePopup();
        buildTypePopup.SetActive(true);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(currentSpot.transform.position);
        buildTypePopupRect.position = screenPos + new Vector3(0, 50f, 0);

        // Clear previous listeners
        foreach (Transform child in buildTypePopupRect)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                string towerType = child.name; // Assuming button name matches tower type
                button.onClick.AddListener(() => OnSelectTowerType(towerType));
            }
        }
    }

    private void OnSelectTowerType(string towerType)
    {
        currentSpot.BuildTower(towerType);
        buildTypePopup.SetActive(false);
        HidePopup();
    }

    #endregion
}