using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static float Angle = 0;
    public static float Force = 0;
    public static float MaxHeight = 0;
    public static float FallSpeed = 0;
    public static float TestTime = 0;
    public static float InitialXVelocity = 0;
    public static float InitialYVelocity = 0;
    public Text angle;
    public Text force;
    public Text maxHeight;
    public Text fallSpeed;
    public Text testTime;
    public Text initialVelocity;

    // Update is called once per frame
    void Update () {
        angle.text = "Angle: " + ((Angle - 180) * -1).ToString ();
        force.text = "Power: " + Force.ToString ();
        maxHeight.text = "Max. Height: " + MaxHeight.ToString () + "m";
        fallSpeed.text = "Fall Speed: " + FallSpeed.ToString () + "m/s";
        testTime.text = "Time Until Max.: " + TestTime.ToString () + "s";
        initialVelocity.text = "Initial Velocity: X = " + InitialXVelocity + "m/s Y = " + InitialYVelocity + "m/s";
    }
}