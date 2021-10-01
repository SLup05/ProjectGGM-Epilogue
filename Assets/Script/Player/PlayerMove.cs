using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator animator = null;
    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;

    [SerializeField] private GameObject[] LifeObject = new GameObject[3];

    public bool CanMove = true;

    private bool CanMoveLeft = true;
    private bool CanMoveRight = true;


    public bool MoveLeft = false;
    public bool MoveRight = false;

    public bool isDodge = false;
    private float DodgeDelay = 1f;


    [SerializeField]
    private GameObject bulletPrefab = null;
    [SerializeField]
    private Transform bulletPosition = null;

    [SerializeField]
    private GameObject EfctPrefab = null;

    private float fireRate = 0f;
    public bool DoFire = false;
    private bool IfFire = false;

    private int PlayerLife = 0;
    private bool isDamaged = false;
    public bool isDead = false;

    private float PlSpeed = 0f;
    private float PlDodgeSpeed = 0f;
    private float PlNormalSpeed = 1.4f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        fireRate = 0.3f;
        IfFire = true;

        PlNormalSpeed = 2.3f;
        PlSpeed = PlNormalSpeed;
        PlDodgeSpeed = 5f;

        PlayerLife = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            StartCoroutine(GameManager.Instance.Restart());
            Explosion();
            return;
        }

        if (GameManager.Instance.PlMinPos.x > transform.localPosition.x)
            CanMoveLeft = false;        
        else
            CanMoveLeft = true;

        if (GameManager.Instance.PlMaxPos.x < transform.localPosition.x)
            CanMoveRight = false;        
        else
            CanMoveRight = true;

        if (MoveLeft && CanMoveLeft)
            transform.Translate(Vector2.left * PlSpeed * Time.deltaTime);        

        if (MoveRight && CanMoveRight)
            transform.Translate(Vector2.right * PlSpeed * Time.deltaTime);

        if (isDodge && CanMove)
        {
            StartCoroutine(Dodge());
        }
        if (DoFire && IfFire && CanMove)
        {
            //print("Fire True");
            IfFire = false;
            Fire();
            Invoke("Reload", fireRate);
        }
    }

    private IEnumerator Dodge()
    {
        PlayerAudioManager.Instance.PlaySound("PlayerDodge");
        CanMove = false;
        PlSpeed = PlDodgeSpeed;
        StartCoroutine(DodgeEfct());
        yield return new WaitForSeconds(DodgeDelay);
        CanMove = true;
        PlSpeed = PlNormalSpeed;
    }

    private IEnumerator DodgeEfct()
    {
        GameObject Efct;
        
        for (int i = 0; i < 4; i++)
        {
            if (GameManager.Instance.playerEfctPoolManager.transform.childCount > 0)
            {
                Efct = GameManager.Instance.playerEfctPoolManager.transform.GetChild(0).gameObject;
                Efct.transform.position = transform.position;
                Efct.SetActive(true);
            }
            else
            {
                Efct = Instantiate(EfctPrefab, transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void Fire()
    {
        PoolOrSpawn();
    }

    private void PoolOrSpawn()
    {
        //print("Ready2Shoot");
        PlayerAudioManager.Instance.PlaySound("PlayerShoot");
        //print("POS In");
        GameObject bullet;
        if (GameManager.Instance.playerpoolManager.transform.childCount > 0)
        {
            bullet = GameManager.Instance.playerpoolManager.transform.GetChild(0).gameObject;
            bullet.transform.position = bulletPosition.position;
            bullet.SetActive(true);
            bullet.GetComponent<BulletMove>().SetTrue();
        }
        else
        {
            //print("MakeBullet");
            bullet = Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);
        }
        bullet.transform.SetParent(null);
    }

    private void Reload()
    {
        IfFire = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;
        if (isDead) return;
        if (collision.CompareTag("Enemy"))
        {
            isDamaged = true;
            StartCoroutine(Damage());
        }
    }

    private IEnumerator Damage()
    {

        if (isDead) yield return null;
        AudioManager.Instance.PlaySound("PlayerHit");
        StartCoroutine(PlayerDeadCheck());
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        isDamaged = false;
    }
    private void LifeExp()
    {
        if (isDead)
        {
            print("CantMove");
            return;
        }
        //print("ReadyAnim");
        if(PlayerLife-1 <0) { return; }
        else
            LifeObject[PlayerLife - 1].GetComponent<PlayerLife>().PlayExp();
    }

    public IEnumerator PlayerDeadCheck()
    {
        LifeExp();
        PlayerLife--;
        
        if (PlayerLife <= 0)
        {
            Explosion();
            isDead = true;
            yield return new WaitForSeconds(0.7f);
        }
    }

    public void Explosion()
    {
        animator.Play("Player_Anim_PlayerExp");
    }    
}
