using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;

    // todo remove from inspector later.
    [Range(-1, 1)] [SerializeField] float movementFactor; // -1 for not moved, 1 for fully moved.
    [SerializeField] float period = 2f;

    GameManager gameManager;
    Vector3 startingPos; // must be stored for absolute movement.
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = transform.position;

        movementVector.x = gameManager.currentObstacleDistance; // run at middle.

        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; //grows continually from 0.

        const float tau = Mathf.PI * 2; // about 6.28.
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1.

        movementFactor = rawSinWave; // default 2f + 1f.

        if (movementVector.x != 0)
        {
            offset.x = movementVector.x * movementFactor;
        }
        if (movementVector.y != 0)
        {
            offset.y = movementVector.y * movementFactor;
        }
        if (movementVector.z != 0)
        {
            offset.z = movementVector.z * movementFactor;
        }
        // set movement factor.
        // todo protect against period is zero.
        transform.position = offset;
    }
}
