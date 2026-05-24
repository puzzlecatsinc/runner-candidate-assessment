using UnityEngine;
using TMPro;

/// <summary>
/// Manages the UI for the game, including showing/hiding panels, updating the UI elements, and handling UI-related inputs.
/// </summary>
public class UIManager : MonoBehaviour
{
    #region SINGLETON
    public static UIManager Instance;
    private void Awake() => Instance = this;
    #endregion

    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelGameHUD;
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI labelCount;
    [SerializeField] private TextMeshProUGUI labelDistance;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        ShowPanelMainMenu();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) ReTry();
    }

    /// <summary>
    /// Hides all the panels.
    /// </summary>
    private void HideAllPanels()
    {
        panelMainMenu.SetActive(false);
        panelGameHUD.SetActive(false);
        panelGameOver.SetActive(false);
    }

    /// <summary>
    /// Shows the main menu panel and hides all the others.
    /// </summary>
    public void ShowPanelMainMenu()
    {
        HideAllPanels();
        panelMainMenu.SetActive(true);
    }

    /// <summary>
    /// Shows the game HUD panel and hides all the others.
    /// </summary>
    public void ShowPanelGameHUD()
    {
        HideAllPanels();
        //panelGameHUD.SetActive(true);
    }

    /// <summary>
    /// Shows the game over panel, updates the distance travelled, and hides all the others.
    /// </summary>
    public void ShowPanelGameOver()
    {
        HideAllPanels();
        panelGameOver.SetActive(true);
        labelDistance.text = $"Distance travelled: {Mathf.FloorToInt(GameManager.Instance.Distance)}";
    }

    /// <summary>
    /// Updates the count label with the given amount.
    /// </summary>
    /// <param name="amount">The amount to display in the count label.</param>
    public void UpdateCount(int amount)
    {
        labelCount.gameObject.SetActive(false);
        labelCount.text = amount.ToString();
    }

    /// <summary>
    /// Begins the game by calling the BeginGame method on the GameManager instance and showing the game HUD panel.
    /// </summary>
    public void BeginGame()
    {
        GameManager.Instance.BeginGame();
        ShowPanelGameHUD();
    }

    /// <summary>
    /// Restarts the game by reloading the active scene.
    /// </summary>
    public void ReTry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
