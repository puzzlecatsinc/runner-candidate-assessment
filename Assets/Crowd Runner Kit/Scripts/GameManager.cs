using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake() => Instance = this;

    public float Speed = 4;
    [SerializeField] private float speedIncreaseRate = 0.2f;
    [SerializeField] private GameObject[] Obstacles;
    [SerializeField] private GameObject testObstacle;
    [SerializeField] private Material gateMaterialRed;
    [SerializeField] private Material gateMaterialBlue;
    public int NumEnemies = 0;
    public bool GameStarted = false;
    public float Distance = 0;

    void Start()
    {
        SpawnObstacle(10, 14, 8, 2,
            GateType.ADD, GateType.MULTIPLY,
            gateMaterialRed, gateMaterialBlue);
        SpawnWeapon(20, 21, 86);
        SpawnObstacle(25, 1);
        SpawnWeapon(30, 18, 31);
        SpawnObstacle(40, 1);
        SpawnObstacle(50, 20, -3, 2,
            GateType.ADD, GateType.MULTIPLY,
            gateMaterialRed, gateMaterialBlue);
        SpawnObstacle(60, 1);
        SpawnWeapon(70, 19, 168);
        SpawnObstacle(80, 1);
        SpawnWeapon(90, 16, 66);
        SpawnObstacle(100, 1);
        SpawnObstacle(105, 1);
        SpawnWeapon(110, 11, 268);
        SpawnObstacle(120, 1);
        SpawnObstacle(130, 22, 2, -1,
            GateType.ADD, GateType.ADD,
            gateMaterialBlue, gateMaterialRed);
        SpawnWeapon(140, 17, 390);
        SpawnObstacle(150, 1);
        SpawnWeapon(160, 23, 540);
        SpawnObstacle(170, 10);
        SpawnObstacle(180, 10);
        SpawnObstacle(190, 10);
        SpawnObstacle(200, 10);
        SpawnObstacle(210, 10);
        SpawnObstacle(220, 10);
        SpawnObstacle(230, 10);
        SpawnObstacle(240, 10);
        SpawnObstacle(250, 10);
        SpawnObstacle(260, 10);
        SpawnObstacle(270, 10);
        SpawnObstacle(280, 10);
        SpawnObstacle(290, 10);
    }

    void Update()
    {
        if (!PlayerController.Instance.Dead && GameStarted)
        {
            Speed += (Time.deltaTime * speedIncreaseRate);
            Distance += Time.deltaTime * Speed;
        }
    }

    public void BeginGame()
    {
        GameStarted = true;
        PlayerController.Instance.SetAnimationRunning();
    }

    private void SpawnWeapon(float z, int obstacleIndex = -1, int healthOne = 0, int healthTwo = 0)
    {
        Vector3 pos = new Vector3(0, 1, z);
        if (obstacleIndex < 0)
        {
            obstacleIndex = UnityEngine.Random.Range(0, Obstacles.Length);
        }
        GameObject go = testObstacle != null ? testObstacle : Obstacles[obstacleIndex];
        GameObject spawned = Instantiate(go, pos, Quaternion.identity);
        if (healthOne != 0)
        {
            spawned.GetComponentsInChildren<Blocker>()[0].OverrideHealth(healthOne);
        }

        if (healthTwo != 0)
        {
            spawned.GetComponentsInChildren<Blocker>()[1].OverrideHealth(healthTwo);
        }
    }

    private void SpawnObstacle(float z, int obstacleIndex = -1, int gateOne = 0, int gateTwo = 0,
        GateType overrideGateTypeOne = GateType.NOOVERRIDE, GateType overrideGateTypeTwo = GateType.NOOVERRIDE,
        Material overrideGateMaterialOne = null, Material overrideGateMaterialTwo = null)
    {
        Vector3 pos = new Vector3(0, 1, z);
        if (obstacleIndex < 0)
        {
            obstacleIndex = UnityEngine.Random.Range(0, Obstacles.Length);
        }
        GameObject go = testObstacle != null ? testObstacle : Obstacles[obstacleIndex];
        GameObject spawned = Instantiate(go, pos, Quaternion.identity);
        if (gateOne != 0)
        {
            spawned.GetComponentsInChildren<Gate>()[0].SetValue(gateOne, overrideGateTypeOne, overrideGateMaterialOne);
        }
        if (gateTwo != 0)
        {
            spawned.GetComponentsInChildren<Gate>()[1].SetValue(gateTwo, overrideGateTypeTwo, overrideGateMaterialTwo);
        }
    }
}
