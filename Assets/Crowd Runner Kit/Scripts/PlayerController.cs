using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake() => Instance = this;

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
    private int nextRunnerId = 0;

    public Sprite currentTower;

    void Start()
    {
        nextRunnerId = 0;
        startGapWidth = gapWidth;
        GameObject firstCharacter = Instantiate(characterPrefab, transform.position, Quaternion.identity, transform);
        PlaceCharacterOnRoad(firstCharacter.transform);
        AddCharacter(firstCharacter);
    }

    void AddCharacter(GameObject character)
    {
        Runner charRunner = character.GetComponent<Runner>();
        charRunner.RunnerId = nextRunnerId;
        nextRunnerId++;
        characters.Add(character);
    }

    public void UpdateShooting(int shotSpeed, GameObject projectile, Sprite newTower)
    {
        shootSpeed = shotSpeed;
        ProjectilePrefab = projectile;

        currentTower = newTower;
    }

    void Update()
    {
        if (Dead || !GameManager.Instance.GameStarted) return;

        TimeSpan ts = DateTime.Now - lastShot;
        if (ts.TotalMilliseconds > shootSpeed)
        {
            lastShot = DateTime.Now;
            if (GameManager.Instance.NumEnemies > 0) Shoot();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
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
            float speed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
            MoveCrowd(speed * sensitivity * 0.1f);
        }
    }

    private void MoveCrowd(float deltaX)
    {
        Vector3 newPosition = transform.position + new Vector3(deltaX, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;
    }

    public void MultiplyCharacters(int multiplier)
    {
        ApplyCrowdCountOutcome(CrowdCountCalculator.Multiply(characters.Count, multiplier, MaxCrowd));
    }

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

    private void UpdateCharacterPositions()
    {
        int numCharacters = characters.Count;
        int numColumns = Mathf.CeilToInt(Mathf.Sqrt(numCharacters));
        int numRows = Mathf.CeilToInt(numCharacters / (float)numColumns);

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

    private void UpdateCharacterScales()
    {
        float scale = Mathf.Pow(characters.Count, -scaleFactor);
        gapWidth = startGapWidth * scale;

        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private void PlaceCharacterOnRoad(Transform characterTransform)
    {
        int layerMask = 1 << 3;
        RaycastHit hit;
        if (Physics.Raycast(characterTransform.position, -Vector3.up, out hit, Mathf.Infinity, layerMask))
        {
            characterTransform.position = hit.point;
        }
    }

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

    public void Shoot()
    {
        foreach (GameObject character in characters)
        {
            character.GetComponent<Runner>().PrepareShot(currentTower);
        }
    }

    public void SetAnimationRunning()
    {
        for (int i = 0; i < characters.Count; i++)
            characters[i].GetComponent<Animator>().SetFloat("Speed", 1);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            enemy.transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", 1);
    }
}
