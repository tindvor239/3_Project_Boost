using UnityEngine.SceneManagement;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;
    [SerializeField] Rigidbody rigidbody;
    float levelLoadDelay = 1f;
    float mainThurst = 650f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        for (int index = 0; index < particleSystems.Length; index++)
        {
            particleSystems[index].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ApplyThrust();
    }

    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThurst * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Invoke("LoadShopLevel", levelLoadDelay);
                break;
        }
    }
    private void LoadShopLevel()
    {
        SceneManager.LoadScene(2); // todo allow for more than 2 levels.
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
