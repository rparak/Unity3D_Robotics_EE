using System.Linq;
using UnityEngine;
using UnityEditor;

public class Ctrl_ABB_SG : MonoBehaviour
{
    // Private constants.
    //  The motion parameters of the ABB Smart-Gripper end-effector.
    //      Stroke in mm.
    private const float s_min = 0.0f;
    private const float s_max = 50.0f;
    //      Velocity in mm/s.
    private const float v_min = 5.0f;
    private const float v_max = 25.0f;

    // Private variables.
    //  Motion parameters.
    private float __speed;
    private float __stroke;
    private float __stroke_i;

    //  Parts (left, right hand) to be transformed.
    private GameObject R_Arm_ID_0; 
    private GameObject L_Arm_ID_0;
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
        R_Arm_ID_0 = transform.Find("R_Arm_ID_0").gameObject;
        //  Left arm.
        L_Arm_ID_0 = transform.Find("L_Arm_ID_0").gameObject;

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
                    __stroke = Mathf.Clamp(stroke / 2.0f, s_min, s_max) / 100000.0f;
                    __speed = Mathf.Clamp(speed / 2.0f, v_min, v_max) / 100000.0f;

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

                    ctrl_state = 2;
                }
                break;

            case 2:
                {
                    // Interpolate the orientation between the current position and the target position.
                    __stroke_i = Mathf.MoveTowards(__stroke_i, __stroke, __speed * Time.deltaTime);

                    // Change the orientation of the end-effector arm.
                    //  Right arm.
                    R_Arm_ID_0.transform.localPosition = new Vector3(__stroke_i, 0.0f, 0.001142f);

                    //  Left arm.
                    L_Arm_ID_0.transform.localPosition = new Vector3(-__stroke_i, 0.0f, 0.001142f);

                    if (__stroke_i == __stroke)
                    {
                        in_position = true; start_movemet = false;
                        ctrl_state = 0;
                    }
                }
                break;

        }
    }
}
