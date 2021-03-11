using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour {
    public Transform cannonAxis;
    public Transform shooter;
    public Transform direction;
    public float firePower;
    public GameObject bullet;

    private Vector3 _initialVelocity;
    private Vector3 _targetWorldPosition;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        _LookToTarget ();
        _UpdateForceDirection ();
        _Shoot ();
        _DefineForce ();
    }

    private void _Shoot () {
        if (Input.GetMouseButtonDown (0)) {
            bullet.GetComponent<Ball> ().velocity = _initialVelocity;
            GameController.InitialXVelocity = _initialVelocity.x;
            GameController.InitialYVelocity = _initialVelocity.y;
            Instantiate (bullet, shooter.position, shooter.rotation);
            Debug.Log (_targetWorldPosition);
        }
    }

    private void _DefineForce () {
        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            firePower++;
        }
        if (Input.GetKeyDown (KeyCode.DownArrow)) {
            firePower--;
        }
    }

    private void _LookToTarget () {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2) Camera.main.ScreenToViewportPoint (Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints (positionOnScreen, mouseOnScreen);

        //Ta Daaa
        transform.rotation = Quaternion.Euler (new Vector3 (angle, -90.0f, 0f));
        GameController.Angle = angle * -1;
        GameController.Force = firePower;

        _targetWorldPosition = direction.position;
    }

    float AngleBetweenTwoPoints (Vector3 a, Vector3 b) {
        return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void _UpdateForceDirection () {
        _initialVelocity = (_targetWorldPosition - transform.position).normalized * firePower;
    }
}