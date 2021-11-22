using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;

    //Prefabs
    public GameObject bombPrefab;

    private bool paused = false;

    [SerializeField]
    private bool canDropBombs = true;
    [SerializeField]
    private float speedMove = 2;
    private float graviryForce;

    private Vector3 moveVector;

    //Cached components
    private Transform Ch_Transform;
    private CharacterController ch_controller;
    private Animator ch_animator;
    private JoystickBehavior JoyBeh;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
        Ch_Transform = transform;
        JoyBeh = GameObject.FindGameObjectWithTag("Joystick").GetComponent<JoystickBehavior>();
    }

    void FixedUpdate()
    {
        CharacterMove();
        GamingGravity();
    }

    private void CharacterMove()
    {
        moveVector = Vector3.zero;
        moveVector.x = JoyBeh.Horizontal() * speedMove;
        moveVector.z = JoyBeh.Vertical() * speedMove;

        //moveVector.x = Input.GetAxis("Horizontal") * speedMove;
        //moveVector.z = Input.GetAxis("Vertical") * speedMove;

        if (canDropBombs && Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }

        //анимация передвижения
        if (moveVector.x != 0 || moveVector.z != 0)
        {
            ch_animator.SetBool("IsMoving", true);
        }
        else
            ch_animator.SetBool("IsMoving", false);

        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }

        moveVector.y = graviryForce;
        transform.position += moveVector * Time.deltaTime;
        //ch_controller.Move(moveVector * Time.deltaTime * speedMove);
        

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                print("Pause");
                Time.timeScale = 0;
                paused = true;
                GameManager.instance.sceneGamePause.SetActive(true);
            }
            else
            {
                print("unPause");
                Time.timeScale = 1;
                paused = false;
                GameManager.instance.sceneGamePause.SetActive(false);
            }
        }
    }

    private void GamingGravity()
    {
        if (!ch_controller.isGrounded)
        {
            graviryForce -= 20f * Time.deltaTime;
        }
        else
        {
            graviryForce = -1f;
        }
    }

    private void DropBomb()
    {
        if (bombPrefab)
        {
            Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(Ch_Transform.position.x), bombPrefab.transform.position.y,
                Mathf.RoundToInt(Ch_Transform.position.z)), bombPrefab.transform.rotation);
        }
    }

    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);

            Destroy(other.gameObject);
        }
        if (other.CompareTag("Finish"))
        {
            gameObject.transform.position = new Vector3(9.5f, 2.0f, 1.5f);
        }
        //else if (other.CompareTag("Explosion"))
        //{
        //    health -= 1;
        //    print(health);
        //    if (health==0)
        //    {
        //        GameManager.instance.LoadGameOverPanel();
        //        Destroy(gameObject);
        //    }
        //}
    }
}
