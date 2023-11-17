using System.Linq;
using UnityEngine;
using UnityEditor;

public class Ctrl_OnRobotRG2 : MonoBehaviour
{
    // Private constants.
    //  The motion parameters of the OnRobot RG2 end-effector.
    //      Stroke in mm.
    private const float s_min = 0.0f;
    private const float s_max = 100.0f;
    //      Velocity in mm/s.
    private const float v_min = 55.0f;
    private const float v_max = 180.0f;
    //  Polynomial coefficients.
    private readonly float[] coefficients = new float[] { 2.858763157442875e-09f, -1.443536999119206e-07f,
                                                          1.0835088633169088e-05f, 0.008994451797350918f,
                                                          2.6805554267921243e-07f };

    // Private variables.
    //  Motion parameters.
    private float __speed;
    private float __stroke;
    private float __theta;
    private float __theta_i;

    //  Parts (left, right hand) to be transformed.
    private GameObject R_Arm_ID_0; private GameObject R_Arm_ID_1;
    private GameObject R_Arm_ID_2;
    private GameObject L_Arm_ID_0; private GameObject L_Arm_ID_1;
    private GameObject L_Arm_ID_2;
    //  Others.
    private int ctrl_state;

    // Public variables.
    public bool start_movemet;
    //  Input motion parameters.
    public float speed;
    public float stroke;

    #if UNITY_EDITOR
        // The [Read-only] attributes that are read-only in the Unity Inspector.
        [ReadOnly]
        public bool in_position;
    #else
        private bool in_position;
    #endif

    // Start is called before the first frame update
    void Start()
    {
        // Initialization of the end-effector movable parts.
        //  Right arm.
        R_Arm_ID_0 = transform.Find("R_Arm_ID_0").gameObject; R_Arm_ID_1 = transform.Find("R_Arm_ID_1").gameObject;
        R_Arm_ID_2 = R_Arm_ID_0.transform.Find("R_Arm_ID_2").gameObject;
        //  Left arm.
        L_Arm_ID_0 = transform.Find("L_Arm_ID_0").gameObject; L_Arm_ID_1 = transform.Find("L_Arm_ID_1").gameObject;
        L_Arm_ID_2 = L_Arm_ID_0.transform.Find("L_Arm_ID_2").gameObject;


        // Reset variables.
        ctrl_state = 0;
        //  Reset the read-only variables to null.
        in_position = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (ctrl_state)
        {
            case 0:
                {
                    // If the values are out of range, clamp them.
                    __stroke = Mathf.Clamp(stroke, s_min, s_max);
                    __speed = Mathf.Clamp(speed, v_min, v_max);

                    if (start_movemet == true)
                    {
                        ctrl_state = 1;
                    }
                }
                break;

            case 1:
                {
                    // Reset variables.
                    in_position = false;

                    // Convert the stroke to the angle in degrees.
                    __theta = Polyval(coefficients, __stroke) * Mathf.Rad2Deg;

                    ctrl_state = 2;
                }
                break;

            case 2:
                {
                    // Interpolate the orientation between the current position and the target position.
                    __theta_i = Mathf.MoveTowards(__theta_i, __theta, __speed * Time.deltaTime);

                    // Change the orientation of the end-effector arm.
                    //  Right arm.
                    R_Arm_ID_0.transform.localEulerAngles = new Vector3(0.0f, -__theta_i, 0.0f);
                    R_Arm_ID_1.transform.localEulerAngles = new Vector3(0.0f, -__theta_i, 0.0f);
                    R_Arm_ID_2.transform.localEulerAngles = new Vector3(0.0f, __theta_i, 0.0f);
                    //  Left arm.
                    L_Arm_ID_0.transform.localEulerAngles = new Vector3(0.0f, __theta_i, 0.0f);
                    L_Arm_ID_1.transform.localEulerAngles = new Vector3(0.0f, __theta_i, 0.0f);
                    L_Arm_ID_2.transform.localEulerAngles = new Vector3(0.0f, -__theta_i, 0.0f);

                    if (__theta_i == __theta)
                    {
                        in_position = true; start_movemet = false;
                        ctrl_state = 0;
                    }
                }
                break;

        }
    }

    public float Polyval(float[] coefficients, float x)
    {
        /*
             Description:
                A function to evaluate a polynomial at a specific value.

                Equation:
                    y = coeff[0]*x**(n-1) + coeff[1]*x**(n-2) + ... + coeff[n-2]*x + coeff[n-1]

            Args:
                (1) coefficients [Vector<float>]: Polynomial coefficients.
                (2) x [float]: An input value to be evaluated.

            Returns:
                (1) parameter [float]: The output value, which is evaluated using the input 
                                       polynomial coefficients.
         */

        float y = 0.0f; int n = coefficients.Length - 1;
        foreach (var (coeff_i, i) in coefficients.Select((coeff_i, i) => (coeff_i, i)))
        {

            y += coeff_i * Mathf.Pow(x, (n - i));
        }

        return y;
    }
}
