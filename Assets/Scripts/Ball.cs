using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Rigidbody rb;
    Vector3 position;
    float maxHeight;
    float maxFallSpeed;
    float timeToReachMaxHeight;
    public bool useGravity;
    public float mass;
    public GameObject terrain;
    public float numberOfPoints;

    [Header ("Física")]
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
    public Vector3 gravity;

    // Start is called before the first frame update
    void Start () {
        //rb = GetComponent<Rigidbody>();
        position = transform.position;
        terrain = GameObject.FindWithTag ("Ground");
    }

    // Update is called once per frame
    void FixedUpdate () {
        timeToReachMaxHeight = maxHeight < transform.position.y ? timeToReachMaxHeight + Time.fixedDeltaTime : timeToReachMaxHeight;
        maxHeight = maxHeight < transform.position.y ? transform.position.y : maxHeight;
        maxFallSpeed = velocity.y < 0 && maxFallSpeed < velocity.y * -1 ? velocity.y * -1 : maxFallSpeed;
        float t = Time.fixedDeltaTime;
        acceleration = force / mass + ((useGravity) ? gravity : Vector3.zero);
        velocity += acceleration * t;
        position += velocity * t;
        transform.position = position;
        force = transform.position.y <= terrain.transform.position.y ? Vector3.zero : NewtonsToWeight (mass * acceleration);
        Debug.Log (force);
        GameController.MaxHeight = maxHeight;
        GameController.FallSpeed = maxFallSpeed;
        GameController.TestTime = timeToReachMaxHeight;
        DetectCollision ();
    }

    private void BounceOnCollide () {
        float reactionForce = velocity.y < 0 ? velocity.y * -0.50f : 0;
        velocity = reactionForce > 0 ? new Vector3 (velocity.x / reactionForce, reactionForce, velocity.z) : Vector3.zero;
        useGravity = velocity != Vector3.zero;
    }

    private void DetectCollision () {
        if (transform.position.y <= terrain.transform.position.y) {
            BounceOnCollide ();
        }
    }

    /// <summary>
    /// Convert Newtowns to kilograms
    /// </summary>
    /// <param name="toBeConverted">Unity in Newtons</param>
    /// <returns></returns>
    private Vector3 NewtonsToWeight (Vector3 toBeConverted) {
        Vector3 result = toBeConverted / 9.8f;
        return result;
    }
}