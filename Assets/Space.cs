using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField] GameObject rocketObject;
    Rocket rocket;
    float yOffSet;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rocket = Rocket.Instance;
        yOffSet = transform.position.y - rocket.startPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketObject != null)
        {
            Vector3 offset = new Vector3 (transform.position.x, rocketObject.transform.position.y + yOffSet, transform.position.z);
            transform.position = offset;
        }
    }
}
