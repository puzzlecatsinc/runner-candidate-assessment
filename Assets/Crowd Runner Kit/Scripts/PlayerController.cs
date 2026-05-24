using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player's characters including their movement, multiplying, dividing, adding, shooting, and dying.
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region SINGLETON
    public static PlayerController Instance;
    private void Awake() => Instance = this;
    #endregion

    public GameObject characterPrefab;
    public List<GameObject> characters = new List<GameObject>();
    [SerializeField] private float characterWidth = 1.0f;
    [SerializeField] private float gapWidth = .1f;
    [SerializeField] private float startGapWidth = .1f;
    [SerializeField] private float minX = -10.0f;
    [SerializeField] private float maxX = 10.0f;
    [SerializeField] private float scaleFactor = 1.0f; 
    [SerializeField] private float sensitivity = 1.0f;
    [SerializeField] private CameraFOVLerp fovLerp;
    public bool Dead = false;
    public GameObject ProjectilePrefab;
    private DateTime lastShot = DateTime.Now;
    public int MaxCrowd = 100;

    private int shootSpeed = 150;
    private int globaChracterId = 0;

    public Sprite currentTower;
    /// <summary>
    /// Start is called before the first frame update.
    /// It is used to initialize the game state and instantiate the first character.
    /// </summary>
    void Start()
    {
        globaChracterId = 0;
        startGapWidth = gapWidth;

        // Initialize the crowd with one character
        GameObject firstCharacter = Instantiate(characterPrefab, transform.position, Quaternion.identity, transform);
        PlaceCharacterOnRoad(firstCharacter.transform);
        AddCharacter(firstCharacter);
    }

    void AddCharacter(GameObject character)
    {
        Runner charRunner = character.GetComponent<Runner>();
        charRunner.RunnerId = globaChracterId;
        globaChracterId++;
        characters.Add(character);

    }
    public void UpdateShooting(int shotSpeed, GameObject projectile, Sprite newTower)
    {
        shootSpeed = shotSpeed;
        ProjectilePrefab = projectile;
        /*Material sharedMaterial = ProjectilePrefab.GetComponentInChildren<Renderer>().sharedMaterial;
        if (sharedMaterial)
        {
            Material material = new Material(sharedMaterial);
            material.color = shotColor; 
            ProjectilePrefab.GetComponentInChildren<Renderer>().material = material;

        }*/
        
        currentTower = newTower;
    }
    /// <summary>
    /// Update is called once per frame.
    /// It is used to update the game state, handle inputs and trigger shooting.
    /// </summary>
    void Update()
    {
        if (Dead || !GameManager.Instance.GameStarted) return;

        TimeSpan ts = DateTime.Now - lastShot;
        if(ts.TotalMilliseconds > shootSpeed)
        {
            lastShot = DateTime.Now;
            if(GameManager.Instance.NumEnemies > 0) Shoot();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // Calculate the position change and use it to move the crowd
                Vector2 deltaPosition = touch.deltaPosition;
                MoveCrowd(deltaPosition.x * sensitivity);
            }
          
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            MoveCrowd(mouseDelta.x * sensitivity);
        }
        else
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
            MoveCrowd(speed * sensitivity * 0.1f);
        }

    }

    /// <summary>
    /// Moves the crowd of characters based on the given input.
    /// </summary>
    /// <param name="deltaX">The change in x direction.</param>
    private void MoveCrowd(float deltaX)
    {
        // Calculate the new position
        Vector3 newPosition = transform.position + new Vector3(deltaX, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX); 

        // Move the parent transform
        transform.position = newPosition;
    }

    /// <summary>
    /// Multiplies the number of characters by the given multiplier, up to the maximum limit.
    /// </summary>
    /// <param name="multiplier">The multiplier for the number of characters.</param>
    public void MultiplyCharacters(int multiplier)
    {
        ApplyCrowdCountOutcome(CrowdCountCalculator.Multiply(characters.Count, multiplier, MaxCrowd));
    }

    /// <summary>
    /// Divides the number of characters by the given divisor, down to a minimum of one.
    /// </summary>
    /// <param name="divisor">The divisor for the number of characters.</param>
    public void DivideCharacters(int divisor)
    {
        ApplyCrowdCountOutcome(CrowdCountCalculator.Divide(characters.Count, divisor, MaxCrowd));
    }

    public void RemoveCharacters(int runnerId)
    {
        var go = characters.Find(s => s.GetComponent<Runner>().RunnerId == runnerId);
        characters.Remove(go);
        Destroy(go);
        if (characters.Count <= 0)
        {
            Die();
        }
    }
    
    /// <summary>
    /// Adds or removes characters based on the given amount.
    /// </summary>
    /// <param name="amount">The amount of characters to add or remove.</param>
    public void AddCharacters(int amount)
    {
        ApplyCrowdCountOutcome(CrowdCountCalculator.Add(characters.Count, amount, MaxCrowd));
    }

    private void ApplyCrowdCountOutcome(CrowdCountOutcome outcome)
    {
        if (outcome.IsNoOp)
        {
            return;
        }

        if (outcome.IsDead)
        {
            Die();
            return;
        }

        int previousCount = characters.Count;
        int targetCount = outcome.TargetCount;

        while (characters.Count < targetCount)
        {
            GameObject newCharacter = Instantiate(characterPrefab, transform);
            AddCharacter(newCharacter);
        }

        while (characters.Count > targetCount)
        {
            GameObject lastCharacter = characters[characters.Count - 1];
            characters.Remove(lastCharacter);
            Destroy(lastCharacter);
        }

        UpdateCharacterScales();
        UpdateCharacterPositions();
        UIManager.Instance.UpdateCount(characters.Count);

        if (characters.Count > previousCount)
        {
            fovLerp.FOVZoom(2);
            AudioManager.Instance.PlayClipIncrease();
        }
        else if (characters.Count < previousCount)
        {
            fovLerp.FOVZoom(-2);
            AudioManager.Instance.PlayClipDecrease();
        }
    }

    /// <summary>
    /// Updates the positions of the characters.
    /// </summary>
    private void UpdateCharacterPositions()
    {
        int numCharacters = characters.Count;
        int  numColumns = Mathf.CeilToInt(Mathf.Sqrt(numCharacters));
        int numRows = Mathf.CeilToInt(numCharacters / (float)numColumns);

        float squareSize = Mathf.Max(numRows, numColumns) * characterWidth;
        float gapSize = gapWidth;

        float startX = -(numColumns - 1) * gapSize / 2f;
        float startZ = -(numRows - 1) * gapSize / 2f;

    
        for (int i = 0; i < numCharacters; i++)
        {
            int row = i / numColumns;
            int column = i % numColumns;

            float xOffset = startX + column * gapSize;
            float zOffset = startZ + row * gapSize;

            Vector3 position = new Vector3(xOffset, 0f, zOffset);
            characters[i].transform.localPosition = position;

            PlaceCharacterOnRoad(characters[i].transform);
        }
    }

    /// <summary>
    /// Updates the scales of the characters.
    /// </summary>
    private void UpdateCharacterScales()
    {
        float scale = Mathf.Pow(characters.Count, -scaleFactor);
        gapWidth = startGapWidth * scale;

        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    /// <summary>
    /// Places a character on the road.
    /// </summary>
    /// <param name="characterTransform">The transform of the character to place.</param>
    private void PlaceCharacterOnRoad(Transform characterTransform)
    {
        int layerMask = 1 << 3; 

        // Raycast downwards from the character
        RaycastHit hit;
        if (Physics.Raycast(characterTransform.position, -Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            characterTransform.position = hit.point;
        }
    }

    /// <summary>
    /// Triggers the death of the player's characters.
    /// </summary>
    public void Die()
    {
        Dead = true;

        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].GetComponent<Animator>().SetBool("Dead", true);
        }

        UIManager.Instance.ShowPanelGameOver();
        AudioManager.Instance.PlayClipDie();
    }

    /// <summary>
    /// Shoots a projectile from each character.
    /// </summary>
    public void Shoot()
    {
        foreach (GameObject character in characters)
        {
            character.GetComponent<Runner>().PrepareShot(currentTower);
        }
    }

    /// <summary>
    /// Sets the running animation for all characters and enemies.
    /// </summary>
    public void SetAnimationRunning()
    {
        for (int i = 0; i < characters.Count; i++)
            characters[i].GetComponent<Animator>().SetFloat("Speed",1);

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            enemy.transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 1);
    }
}
