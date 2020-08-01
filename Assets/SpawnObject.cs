using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] AudioClip explosiveSound;
    [SerializeField] ParticleSystem explosiveParticle;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] int hitCount = 1;
    int hit = 0;
    public float weight = 0;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (GetComponent<Rigidbody>() != null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        else
        {
            print("Rigidbody does not have in" + this.gameObject.name);
        }
    }


    // Update is called once per frame
    void Update()
    {
        ApplyWeight();
    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            default:
                Explosive();
                Invoke("DestroyObject", 0.5f);
                break;
        }
    }

    private void DestroyObject()
    {
        hit += 1;
        if (hit >= hitCount)
        {
            Destroy(gameObject);
        }
    }
    void Explosive()
    {
        audioSource.PlayOneShot(explosiveSound);
        explosiveParticle.Play();
    }

    void ApplyWeight()
    {
        if (rigidbody != null)
        {
            rigidbody.AddRelativeForce(Vector3.down * weight * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
