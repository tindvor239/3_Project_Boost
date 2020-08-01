using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThurst = 100f;
    [SerializeField] float mainThurst = 850f;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] Save save;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    public Text scoreText;
    public Text fuelText;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] bool lockThurst;

    GameManager gameManager;
    bool isLeftPress;
    bool isRightPress;

    public int life;
    public Vector3 startPosition;
    private Rigidbody rigidbody;
    private AudioSource audioSource;
    public int score = 0;
    public int fuel = 100;
    private float delayScore = 0;
    enum State { Alive, Dying, Transcending }
    State state = State.Alive;
    #region Singleton
    public static Rocket Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        if (GetComponent<Rigidbody>() != null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.Log("This game object " + gameObject.name +" doesn't have any Rigidbody, are you missing something?");
        }

        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Debug.Log("This game object " + gameObject.name + " doesn't have any AudioSource, are you missing something?");
        }
        score = save.currentScore;
        fuel = save.fuel;
        life = save.life;
        startPosition = transform.position;
        transform.position = save.playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            ProcessInput();
            save.currentScore = score;
            save.fuel = fuel;
            save.life = life;
            save.playerPosition = transform.position;
}
    }
    private void Score()
    {
        delayScore -= Time.deltaTime;
        if (delayScore <= 0f)
        {
            save.money += 1;
            score += 1;
            fuel -= 1;
            scoreText.text = "Score: " + score.ToString();
            fuelText.text = "Fuel: " + fuel.ToString();
            delayScore = 1f;
        }
    }
    private void ProcessInput()
    {
        if (fuel <= 0)
        {
            lockThurst = true;
        }
        else
        {
            lockThurst = false;
        }
        LockPosition();
        RespondToThurstInput();
        LockRotation();
        RespondToRotate();
    }

    private void RespondToThurstInput()
    {
        if (lockThurst == false)
        {
            ApplyThurst();
            Score();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
    }

    private void ApplyThurst()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThurst * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticle.Play();
    }

    private void RespondToRotate()
    {
        rigidbody.freezeRotation = true; // take manual control of rotation.
        
        if (Input.GetKey(KeyCode.A)|| isLeftPress == true)
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) || isRightPress == true)
        {
            RotateRight();
        }
        rigidbody.freezeRotation = false; // resume physic control.
    }
    public void LeftPressed()
    {
        isLeftPress = true;
    }
    public void LeftReleased()
    {
        isLeftPress = false;
    }
    public void RightPressed()
    {
        isRightPress = true;
    }
    public void RightReleased()
    {
        isRightPress = false;
    }
    private void RotateLeft()
    {
        float rotationThisFrame = rcsThurst * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationThisFrame);
    }

    private void RotateRight()
    {
        float rotationThisFrame = rcsThurst * Time.deltaTime;
        transform.Rotate(-Vector3.forward * rotationThisFrame);
    }

    private void RecreateRocket()
    {
        if (life >= 0)
        {
            gameObject.active = true;
            state = State.Alive;
        }
    }

    private void LockPosition()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z);
    }

    private void LockRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                Invoke("DestroyObject", 0.5f);
                score = 0;
                break;
        }
    }
    private void DestroyObject()
    {
        gameObject.active = false;
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        save.fuel = 50;
        save.currentScore = 0;
        save.playerPosition = startPosition;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        mainEngineParticle.Stop();
        deathParticle.Play();
        life--;
        if (life > 0)
        {
            Invoke("RecreateRocket", levelLoadDelay);
        }

        if (life <= 0)
        {
            Invoke("LoadFirstLevel", levelLoadDelay);
        }
        print("Dead");
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(0); // todo allow for more than 2 levels.
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0); //todo allow for 
    }
}
