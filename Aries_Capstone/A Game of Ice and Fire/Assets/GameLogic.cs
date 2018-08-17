using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic: MonoBehaviour {

    public static GameLogic instance; // this class will be singleton
    public PlayerStats playerStats;

    // game interactive hands
    public LaserController laserLeft;
    public LaserController laserRight;

    // protectee
    public GameObject fire; 

    // spawn
    public GameObject spawnPoint; 
    public Obstacle[] smallObjectPrefabs; 
    public Obstacle[] largeObjectPrefabs;
    public GameObject[] specials;
    public GameObject[] animations;
    public Obstacle parentPrefab;
    public Vector3 spawnColumnOffset = new Vector3(1, 0, 0);
    public Vector3 largeObjectZoffset = new Vector3(0, 0, -2);

    private bool spawnLarge;

    // level up 
    public GameObject saveAndLoad;
    public GameObject levelCanvas;
    public Text levelText; 

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        spawnLarge = true;
    } 

    public void StartNewGame ()
    {
        Debug.Log("Starting new game");
        playerStats = new PlayerStats(1, 0, 60);
        initGame();
    }

    public void LoadLastGame()
    {
        Debug.Log("Scene is loaded");
        playerStats = SaveAndLoad.localCopyOfData;
        initGame();
    } 

    public void initGame() {
        CancelInvoke();
        CleanUpObstacles();
        levelText.text = "LEVEL " + playerStats.level;
        Obstacle.movingSpeed = playerStats.obstacleMovingSpeed;
        AssistingBlocksController.movingSpeed = playerStats.obstacleMovingSpeed; 
        InvokeRepeating("Spawn", 3.0f, 5.0f);
        fire.SetActive(true); 
    } 

    void LevelUp ()
    {
        if (playerStats.spawnCount % 3 == 0)
        {
            levelText.text = "LEVEL " + (++playerStats.level);
            levelCanvas.GetComponent<Animator>().SetTrigger("levelUpAnim"); 
        }
        if (playerStats.level % 5 == 0)
        { 
            // obstacles moving faster
            Obstacle.movingSpeed = (playerStats.obstacleMovingSpeed += 10);
            AssistingBlocksController.movingSpeed = Obstacle.movingSpeed;
        } 
        else if (playerStats.level == 30)
        {
            // win the game
            saveAndLoad.gameObject.SendMessage("GameOver", true);
        }
    }

    void Spawn()
    {
        playerStats.spawnCount++;
        Debug.Log("Spawn is called");
        LevelUp();
        int howManyToSpawn = Random.Range(0, 3); // 0 = other, 1 = 1, 2 = 2
        int randomAnimation = Random.Range(0, animations.Length);
        RuntimeAnimatorController rAnim = animations[randomAnimation].GetComponent<Animator>().runtimeAnimatorController;
        Obstacle objectToSpawn = GetRandomOstacleFromList(smallObjectPrefabs);

        if (howManyToSpawn == 1)
        {
            Obstacle o = Instantiate(objectToSpawn, spawnPoint.transform.position, objectToSpawn.transform.rotation);
            Animate(o, rAnim);
        } 
        else if (howManyToSpawn == 2)
        { 
            Obstacle o1 = Instantiate(objectToSpawn, spawnPoint.transform.position - spawnColumnOffset, Quaternion.identity);
            Obstacle o2 = Instantiate(objectToSpawn, spawnPoint.transform.position + spawnColumnOffset, Quaternion.identity);
            o1.StartMoving();
            o2.StartMoving(); 
        }
        else // spawn groups
        {
            int shape = Random.Range(0, 6);
            Obstacle largeObstacleToSpawn = GetRandomOstacleFromList(largeObjectPrefabs);
            switch (shape)
            {
                case 0: // circle  
                    float Theta = 0f;
                    float ThetaScale = 0.125f;
                    int Size = (int)((1f / ThetaScale) + 1f); 
                    for (int i = 0; i < Size-1; i++)
                    {
                        Theta += (2.0f * Mathf.PI * ThetaScale);
                        float x = Mathf.Cos(Theta);
                        float y = Mathf.Sin(Theta); 
                        Obstacle o = Instantiate(objectToSpawn, spawnPoint.transform.position + new Vector3((float)x, 0f, (float)y), Quaternion.identity);
                        o.StartMoving();
                    } 
                    break;
                case 1: // box
                    for (int i = -1; i <= 1; ++i)
                    {
                        for (int j = -1; j <= 1; ++j)
                        {
                            Obstacle o = Instantiate(objectToSpawn, spawnPoint.transform.position + new Vector3(i, 0, j), Quaternion.identity);
                            o.StartMoving();
                        } 
                    } 
                    break;
                case 2: // triangle
                    for (int i = 0; i <= 2; ++i)
                    {
                        for (int j = -i; j <= i; j+=2)
                        {
                            Obstacle o = Instantiate(objectToSpawn, spawnPoint.transform.position + new Vector3((float)j/2, 0, (float)i/2), Quaternion.identity);
                            o.StartMoving();
                        }
                    } 
                    break;
                case 3: // upside down triangle
                    for (int i = 0; i >= -2; --i)
                    {
                        for (int j = i; j <= -i; j+=2)
                        {
                            Obstacle o = Instantiate(objectToSpawn, spawnPoint.transform.position + new Vector3((float)j / 2f, 0, (float)i / 2f), Quaternion.identity);
                            o.StartMoving();
                        }
                    } 
                    break;
                case 4: // specials 
                    GameObject special = Instantiate(specials[0], spawnPoint.transform.position - new Vector3(2, 0, 0), Quaternion.identity);
                    spawnLarge = false;
                    break;
                default: // spawn one by one  
                    StartCoroutine(SpawnOneByOne(3, objectToSpawn, rAnim));  
                    break; 
            }
            if (spawnLarge)
            {
                Obstacle l = Instantiate(largeObstacleToSpawn, spawnPoint.transform.position + largeObjectZoffset, largeObstacleToSpawn.transform.rotation);
                l.StartMoving();
            }
            else
            {
                spawnLarge = true;
            } 
        } 
    }

    private void CleanUpObstacles()
    {
        int layer = LayerMask.NameToLayer("Obstacles");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject o in allObjects)
        {
            if (o.layer == layer) Destroy(o);
        }
    }

    private Obstacle GetRandomOstacleFromList(Obstacle[] list)
    {
        int random = Random.Range(0, list.Length);
        return list[random];
    }

    private void Animate(Obstacle o, RuntimeAnimatorController rAnim)
    {
        Obstacle parent = Instantiate(parentPrefab, spawnPoint.transform.position, Quaternion.identity);
        o.gameObject.AddComponent<Animator>();
        o.gameObject.GetComponent<Animator>().runtimeAnimatorController = rAnim;
        o.gameObject.transform.SetParent(parent.transform);
        Destroy(o.GetComponent<Rigidbody>());
        parent.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        parent.StartMoving();
    }

    IEnumerator SpawnOneByOne(int count, Obstacle objectToSpawn, RuntimeAnimatorController rAnim)
    {
        for (int i = 0; i < count; i++)
        {
            Obstacle o = Instantiate(objectToSpawn, spawnPoint.transform.position, objectToSpawn.transform.rotation);
            Animate(o, rAnim);
            // yield return 0; // wait 1 Frame
            yield return new WaitForSeconds(0.8f); // wait 1 second per interval
        }  
    } 
}
