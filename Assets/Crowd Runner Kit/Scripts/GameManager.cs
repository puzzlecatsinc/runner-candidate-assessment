using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Manages the game state, including starting the game, updating the state, and managing obstacles.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager Instance;
    private void Awake() => Instance = this;
    #endregion

    public float Speed = 4;
    [SerializeField] private float maxSpeed = 15;
    [SerializeField] private float speedIncreaseRate = 0.2f;
    [SerializeField] private GameObject[] Obstacles;
    [SerializeField] private GameObject testObstacle;
    [SerializeField] private Material gateMaterialRed;
    [SerializeField] private Material gateMaterialBlue;
    public int NumEnemies = 0;
    public bool GameStarted = false;
    public float Distance = 0;

    private int[] intRewards = new int[] { };
    
    /// <summary>
    /// Start is called before the first frame update.
    /// It is used to spawn initial obstacles and start the game tick.
    /// </summary>
    void Start()
    {
        SpawnObstacle(10, false, 14, 8,2,
            GateType.ADD, GateType.MULTIPLY,
            gateMaterialRed, gateMaterialBlue);
        SpawnWeapon(20, forceMoving:false, 21, 86);
        SpawnObstacle(25, false, 1);
        SpawnWeapon(30, false, 18, 31);
        SpawnObstacle(40, false, 1);
        SpawnObstacle(50, false, 20, -3, 2,
            GateType.ADD, GateType.MULTIPLY,
            gateMaterialRed, gateMaterialBlue);
        SpawnObstacle(60, false, 1);
        SpawnWeapon(70, false, 19, 168);
        SpawnObstacle(80, false, 1);    
        SpawnWeapon(90, false, 16, 66);
        SpawnObstacle(100, false, 1);
        SpawnObstacle(105, false, 1);
        SpawnWeapon(110, false, 11, 268);
        SpawnObstacle(120, false, 1);
        SpawnObstacle(130, false, 22, 2, -1, 
            GateType.ADD, GateType.ADD, 
            gateMaterialBlue,gateMaterialRed);
        SpawnWeapon(140, false, 17,390);
        SpawnObstacle(150, false, 1);
        SpawnWeapon(160, false, 23, 540);
        SpawnObstacle(170, false, 10);
        SpawnObstacle(180, false, 10);
        SpawnObstacle(190, false, 10);
        SpawnObstacle(200, false, 10);
        SpawnObstacle(210, false, 10);
        SpawnObstacle(220, false, 10);
        SpawnObstacle(230, false, 10);
        SpawnObstacle(240, false, 10);
        SpawnObstacle(250, false, 10);
        SpawnObstacle(260, false, 10);
        SpawnObstacle(270, false, 10);
        SpawnObstacle(280, false, 10);
        SpawnObstacle(290, false, 10);
        
        Tick();
    }

    /// <summary>
    /// Update is called once per frame.
    /// It handles game input, updates the game speed, and updates the distance if the game has started and the player is not dead.
    /// </summary>
    void Update()
    {
        if (!PlayerController.Instance.Dead && GameStarted)
        {
            Speed += (Time.deltaTime * speedIncreaseRate);
            Distance += Time.deltaTime * Speed;
        }
    }

    /// <summary>
    /// Begins the game by setting the GameStarted flag and setting the player animation to running.
    /// </summary>
    public void BeginGame()
    {
        GameStarted = true;
        PlayerController.Instance.SetAnimationRunning();
    }

    /// <summary>
    /// This method is called at each game tick.
    /// It spawns an obstacle if the player is not dead and the game has started, and sets the interval for the next tick.
    /// </summary>
    private void Tick()
    {
        //if(!PlayerController.Instance.Dead && GameStarted) SpawnObstacle(45, false);

        float interval = Mathf.Lerp(4f, 0.5f, Speed / maxSpeed);
        Invoke("Tick", interval);
    }

    private void SpawnWeapon(float z, bool forceMoving, int obstacleIndex =-1, int healthOne=0, int healthTwo=0)
    {
        Vector3 pos = new Vector3(0, 1, z);
        if (obstacleIndex < 0)
        {
            obstacleIndex = UnityEngine.Random.Range(0, Obstacles.Length);
        }
        GameObject go = testObstacle != null ? testObstacle : Obstacles[obstacleIndex];
        Instantiate(go, pos, Quaternion.identity);
        //if(go.GetComponent<HorizontalMover>())
        //    go.GetComponent<HorizontalMover>().enabled = (UnityEngine.Random.value > 0.75f) || forceMoving;
        if (healthOne != 0)
        {
            go.GetComponentsInChildren<Blocker>()[0].OverrideHealth(healthOne);
        }

        if (healthTwo != 0)
        {
            go.GetComponentsInChildren<Blocker>()[1].OverrideHealth(healthTwo);
        }
       
    }
    
    /// <summary>
    /// Spawns an obstacle at a given z position.
    /// The obstacle can be forced to move by setting the forceMoving parameter to true.
    /// </summary>
    /// <param name="z">The z position at which to spawn the obstacle.</param>
    /// <param name="forceMoving">If set to true, forces the obstacle to move.</param>
    private void SpawnObstacle(float z, bool forceMoving, int obstacleIndex =-1, int gateOne=0, int gateTwo=0, 
        GateType overrideGateTypeOne=GateType.NOOVERRIDE, GateType overrideGateTypeTwo=GateType.NOOVERRIDE,
        Material overrideGateMaterialOne=null, Material overrideGateMaterialTwo=null)
    {
        Vector3 pos = new Vector3(0, 1, z);
        if (obstacleIndex < 0)
        {
            obstacleIndex = UnityEngine.Random.Range(0, Obstacles.Length);
        }
        GameObject go = testObstacle != null ? testObstacle : Obstacles[obstacleIndex];
        Instantiate(go, pos, Quaternion.identity);
        //if(go.GetComponent<HorizontalMover>())
        //    go.GetComponent<HorizontalMover>().enabled = (UnityEngine.Random.value > 0.75f) || forceMoving;
        if (gateOne != 0)
        {
            go.GetComponentsInChildren<Gate>()[0].SetValue(gateOne, overrideGateTypeOne, overrideGateMaterialOne);
        }
        if (gateTwo != 0)
        {
            go.GetComponentsInChildren<Gate>()[1].SetValue(gateTwo, overrideGateTypeTwo, overrideGateMaterialTwo);
        }
    }
}
