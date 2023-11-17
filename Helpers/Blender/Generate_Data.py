# BPY (Blender as a python) [pip3 install bpy]
import bpy
# Numpy (Array computing) [pip3 install numpy]
import numpy as np

"""
Description:
    Initialization of constants.
"""
# The angle of rotation in radians.
#   Robotiq (2F-85): 0.0 to 0.8014900991005511 rad.
#   OnRobot (RG2): 0.0 to 1.2026191210860813 rad.
CONST_THETA = {'min': 0.0, 'max': 0.8014900991005511}

def main():
    """
    Description:
        A program to generate the values of the angle depending on the end-effector stroke position.

        Note:
            Use only for Robotiq and OnRobot end-effectors.
    """

    # Output data.
    #   x: The stroke position in millimetres.
    #   y: The angle of rotation in radians.
    x = []
    y = []

    # Generate of the data.
    for _, th_i in enumerate(np.linspace(CONST_THETA['min'], CONST_THETA['max'], 5)):
        # Change the orientation of the end-effector arm.
        #   Right.
        bpy.data.objects['R_Arm_ID_0'].rotation_euler = [0.0,  th_i, 0.0]
        bpy.data.objects['R_Arm_ID_1'].rotation_euler = [0.0,  th_i, 0.0]
        bpy.data.objects['R_Arm_ID_2'].rotation_euler = [0.0, -th_i, 0.0]
        #   Left.
        bpy.data.objects['L_Arm_ID_0'].rotation_euler = [0.0, -th_i, 0.0]
        bpy.data.objects['L_Arm_ID_1'].rotation_euler = [0.0, -th_i, 0.0]
        bpy.data.objects['L_Arm_ID_2'].rotation_euler = [0.0,  th_i, 0.0]

        # Update the scene.
        bpy.context.view_layer.update()
        
        # Obtain the stroke in millimetres.
        #   Note:
        #       The obtained value from the finger must be multiplied by two because 
        #       the stroke of each arm is expressed relative to the center.
        stroke_i = (bpy.data.objects['R_Finger'].matrix_world.to_translation()[0] * 1000.0) * 2.0
        
        # Store the data.   
        x.append(stroke_i); y.append(th_i)
        
    # Display the results.
    print('[INFO] The generated data to be copied into the "../Obtain_Coefficient.py" script.')
    print(f'[INFO] >> np.array({x}, dtype=np.float64)')
    print(f'[INFO] >> np.array({y}, dtype=np.float64)')

    # Change the orientation of the end-effector arm to the 'Home' position.
    th_i = CONST_THETA['min']
    #   Right.
    bpy.data.objects['R_Arm_ID_0'].rotation_euler = [0.0,  th_i, 0.0]
    bpy.data.objects['R_Arm_ID_1'].rotation_euler = [0.0,  th_i, 0.0]
    bpy.data.objects['R_Arm_ID_2'].rotation_euler = [0.0, -th_i, 0.0]
    #   Left.
    bpy.data.objects['L_Arm_ID_0'].rotation_euler = [0.0, -th_i, 0.0]
    bpy.data.objects['L_Arm_ID_1'].rotation_euler = [0.0, -th_i, 0.0]
    bpy.data.objects['L_Arm_ID_2'].rotation_euler = [0.0,  th_i, 0.0]

    # Update the scene.
    bpy.context.view_layer.update()

if __name__ == '__main__':
    main()