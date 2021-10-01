using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerPoolManager playerpoolManager = null;
    public EnemIntNumBulletPool enemIntNumBulletPool = null;
    public EnemFloatLeftBulletPool enemFloatLeftBulletPool = null;
    public EnemFloatRightBulletPool enemFloatRightBulletPool = null;
    public EnemCharBulletPool enemCharBulletPool = null;

    public PlayerEfctPoolManager playerEfctPoolManager = null;
    public PlayerMove playerMove = null;
    private RestartAnim RestartAnim = null;
    private OpenAndClose openAndClose = null;

    private bool Restarting = false;

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    public Vector2 MaxPos { get; private set; }
    public Vector2 MinPos { get; private set; }
    public Vector2 PlMaxPos { get; set; }
    public Vector2 PlMinPos { get; set; }

    private float stagestartDela = 2;
    [SerializeField] int SetStage = 0;

    private int ToClearBug = 0;
    public int NowClearBug = 0;

    public int ToClearWave = 0;
    public int NowClearWave = 0;

    private bool TimeToChangeWave = false;

    [SerializeField] private Text Text_ToClearBug = null;
    [SerializeField] private Text Text_NowWave = null;


    private float stagestartDelay = 3;
    public bool IntSpawnLeft = false;

    [SerializeField] GameObject IntPrefab = null;
    [SerializeField] GameObject FloatPrefab = null;
    [SerializeField] GameObject TranslatePrefab = null;
    [SerializeField] GameObject StringPrefab = null;
    void Awake()
    {
        playerpoolManager = FindObjectOfType<PlayerPoolManager>();
        playerEfctPoolManager = FindObjectOfType<PlayerEfctPoolManager>();
        enemIntNumBulletPool = FindObjectOfType<EnemIntNumBulletPool>();
        enemCharBulletPool = FindObjectOfType<EnemCharBulletPool>();

        print("LeftPoolIn");
        enemFloatLeftBulletPool = FindObjectOfType<EnemFloatLeftBulletPool>();
        enemFloatRightBulletPool = FindObjectOfType<EnemFloatRightBulletPool>();

        print("SetPlMove");
        playerMove = FindObjectOfType<PlayerMove>();
        RestartAnim = FindObjectOfType<RestartAnim>();
        openAndClose = FindObjectOfType<OpenAndClose>();

        MaxPos = new Vector2(3f, 7f);
        MinPos = new Vector2(-3f, -7f);

        PlMaxPos = new Vector2(2.3f, -2.2f);
        PlMinPos = new Vector2(-2.3f, -2.2f);

        switch (SetStage)
        {
            case 1:
                SetStage1();
                break;
            case 2:
                SetStage2();
                break;
            case 3:
                SetStage3();
                break;
        }

    }

    private void Update()
    {
        if (NowClearBug >= ToClearBug)
        {
            TimeToChangeWave = true;
            NowClearBug = 0;
        }
    }

    private void SetStage1()
    {
        ToClearBug = 7;
        ToClearWave = 2;
        NowClearWave++;
        UpdateUI();

        StartCoroutine(PlayStage1_1());
    }

    private IEnumerator PlayStage1_1()
    {
        yield return new WaitForSeconds(stagestartDelay);

        StartCoroutine(SpawnInt());

        while (true)
        {
            if (TimeToChangeWave)
                StartCoroutine(PlayStage1_2());
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator PlayStage1_2()
    {
        NextWaveSound();

        StopCoroutine("PlayStage1_1");

        StartCoroutine(SpawnFloat());
        NowClearWave++;
        NowClearBug = 0;
        ToClearBug = 15;
        TimeToChangeWave = false;
        UpdateUI();
        while (true)
        {
            if (TimeToChangeWave)
                SceneManager.LoadScene("Stage1_Clear");
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SetStage2()
    {
        ToClearBug = 3;
        ToClearWave = 3;
        NowClearWave++;
        UpdateUI();
        StartCoroutine(PlayStage2_1());
    }

    private IEnumerator PlayStage2_1()
    {
        yield return new WaitForSeconds(stagestartDelay);

        //print("StartStage2");
        StartCoroutine(SpawnTranslate());
        
        UpdateUI();
        while (true)
        {
            if (TimeToChangeWave)
            StartCoroutine(PlayStage2_2());
            yield return new WaitForSeconds(0.1f);
        }

    }

    private IEnumerator PlayStage2_2()
    {
        NextWaveSound();
        StopCoroutine("PlayStage2_1");

        StartCoroutine(SpawnInt());
        NowClearWave++;
        NowClearBug = 0;
        ToClearBug = 10;
        UpdateUI();

        TimeToChangeWave = false;

        while (true)
        {
            if (TimeToChangeWave)
            StartCoroutine(PlayStage2_3());
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator PlayStage2_3()
    {
        NextWaveSound();
        StopCoroutine(PlayStage2_2());

        StartCoroutine(SpawnFloat());
        NowClearWave++;
        NowClearBug = 0;
        ToClearBug = 15;
        UpdateUI();

        TimeToChangeWave = false;

        while (true)
        {
            if (TimeToChangeWave)
                SceneManager.LoadScene("Stage2_Clear");
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void SetStage3()
    {
        ToClearBug = 3;
        ToClearWave = 3;
        NowClearWave++;
        UpdateUI();

        StartCoroutine(PlayStage3_1());
    }
    private IEnumerator PlayStage3_1()
    {
            yield return new WaitForSeconds(stagestartDelay);

            StartCoroutine(SpawnString());
        
            TimeToChangeWave = false;

            while (true)
            {
                if (TimeToChangeWave)
                    StartCoroutine(PlayStage3_2());
                yield return new WaitForSeconds(0.1f);
            }
    }

    private IEnumerator PlayStage3_2()
    {
        NextWaveSound();

        StartCoroutine(SpawnString());
        StartCoroutine(SpawnInt());

        ToClearBug = 10;
        NowClearBug = 0;
        NowClearWave++;
        TimeToChangeWave = false;

        UpdateUI();

        while (true)
        {
            if (TimeToChangeWave)
                StartCoroutine(PlayStage3_3());
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator PlayStage3_3()
    {
        NextWaveSound();

        StopCoroutine(PlayStage3_2());
        StartCoroutine(SpawnFloat());
        StartCoroutine(SpawnTranslate());

        NowClearWave++;
        NowClearBug = 0;
        ToClearBug = 10;
        UpdateUI();

        TimeToChangeWave = false;

        while (true)
        {
            if (TimeToChangeWave)
                SceneManager.LoadScene("Ending");
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator SpawnInt()
    {
        while (true)
        {
            //print("StartInt");

            GameObject EnemInt;

            float posX;
            float posY;


            if (IntSpawnLeft)
                posX = -4;
            else
                posX = 4;

            posY = Random.Range(0f, 2f);

            for (int i = 0; i < 3; i++)
            {
                //print("MakeInt");

                /*if (GameManager.Instance.enemIntNumBulletPool.transform.childCount > 0)
                {
                    EnemInt =
                        GameManager.Instance.enemIntNumBulletPool.transform.GetChild(0).gameObject;
                    EnemInt.transform.position = new Vector2(posX, posY);
                    EnemInt.SetActive(true);
                }
                else
                {
                    EnemInt = Instantiate(IntPrefab, new Vector2(posX, posY), Quaternion.identity);
                }*/

                EnemInt = Instantiate(IntPrefab, new Vector2(posX, posY), Quaternion.identity);


                yield return new WaitForSeconds(0.9f);
            }

            if (IntSpawnLeft)
                IntSpawnLeft = false;
            else
                IntSpawnLeft = true;

            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator SpawnFloat()
    {
        //print("In SpawnF");
        float posX;
        float posY;
        while (true)
        {
            for (int i = 0; i < 1; i++)
            {
                posX = Random.Range(-2f, 2f);
                posY = 7.5f;

                Instantiate(FloatPrefab, new Vector2(posX, posY), Quaternion.identity);

                yield return new WaitForSeconds(4.5f);

            }
        }
    }
    
    private IEnumerator SpawnString()
    {
        float posX;
        float posY;
        while (true)
        {
            GameObject EnemString;

            posX = 4;
            posY = 3.5f;

            Instantiate(StringPrefab, new Vector2(posX, posY), Quaternion.identity);

            yield return new WaitForSeconds(5f);            
        }
    }
    private IEnumerator SpawnTranslate()
    {
        float posX, posY;
        posY = 7.5f;

        while(true)
        {
            for (int i = 0; i < 4; i++)
            {
                posX = playerMove.transform.position.x;

                Instantiate(TranslatePrefab, new Vector2(posX, posY), Quaternion.identity);

                yield return new WaitForSeconds(3f);
            }
            yield return new WaitForSeconds(5f);
        }

        
    }
    public void UpdateUI()
    {
        //if (ToClearWave == 2) print("Ready222222");
        Text_ToClearBug.text = string.Format("{0}", ToClearBug - NowClearBug);
        Text_NowWave.text = string.Format("WAVE {0} / {1}", NowClearWave, ToClearWave);
    }

    private void NextWaveSound()
    {
        ClearAudioManager.Instance.PlaySound();
    }

    public IEnumerator Restart()
    {
        if (Restarting) yield return null;
        else
        {
        Restarting = true;
        print("Restart Start");
        StartCoroutine(openAndClose.ScenceFadein());
        RestartAnim.PlayAnim();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        
    }
}
