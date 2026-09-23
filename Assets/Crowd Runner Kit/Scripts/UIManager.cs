using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake() => Instance = this;

    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelGameHUD;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI labelCount;
    [SerializeField] private TextMeshProUGUI labelDistance;

    void Start()
    {
        ShowPanelMainMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) ReTry();
    }

    private void HideAllPanels()
    {
        panelMainMenu.SetActive(false);
        panelGameHUD.SetActive(false);
        panelGameOver.SetActive(false);
    }

    public void ShowPanelMainMenu()
    {
        HideAllPanels();
        panelMainMenu.SetActive(true);
    }

    public void ShowPanelGameHUD()
    {
        HideAllPanels();
    }

    public void ShowPanelGameOver()
    {
        HideAllPanels();
        panelGameOver.SetActive(true);
        labelDistance.text = $"Distance travelled: {Mathf.FloorToInt(GameManager.Instance.Distance)}";
    }

    public void UpdateCount(int amount)
    {
        labelCount.gameObject.SetActive(false);
        labelCount.text = amount.ToString();
    }

    public void BeginGame()
    {
        GameManager.Instance.BeginGame();
        ShowPanelGameHUD();
    }

    public void ReTry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
