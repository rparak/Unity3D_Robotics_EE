# Unity3D Industrial Robotics: End-Effector Control

<p align="center">
<img src=https://github.com/rparak/Unity3D_Robotics_EE/blob/main/images/Background.png width="800" height="450">
</p>

 ## Requirements:

**Software:**
```bash
Blender, Unity3D 2022.3.2f1 (LTS), Visual Studio 2019/2022
```

**Supported on the following operating systems:**
```bash
Universal Windows Platform, Android
```

| Software/Package      | Link                                                                                  |
| --------------------- | ------------------------------------------------------------------------------------- |
| Blender               | https://www.blender.org/download/                                                     |
| Unity3D               | https://unity3d.com/get-unity/download/archive                                        |
| Visual Studio         | https://visualstudio.microsoft.com/downloads/                                         |

## Project Description:

The project focuses on the integration and control of various types of end-effectors, including Robotiq, OnRobot, ABB Smart Gripper, and SMC's custom grippers, within the Unity3D environment. The project leverages the power of Unity3D, a robust game development engine, to provide a versatile and user-friendly platform for simulating, testing, and controlling a variety of robotic end-effectors.

The motion parameters of a specific end-effector can be found in the following table.

| Software/Package  | Velocity       | Total Stroke |
| ----------------- | -------------- | ------------ |
| Robotiq 2F-85     | 20 to 150 mm/s | 0 to 85 mm   |
| OnRobot RG2       | 55 to 180 mm/s | 0 to 100 mm  |
| ABB Smart Gripper | 5 to 25 mm/s   | 0 to 50 mm   |

The other end-effectors are pneumatically controlled and do not use this type of parameter for their control.

The repository that describes the design of SMC's custom grippers, and other useful information can be found in the link below.

[SMC Industrial Automation: End-Effector Prototypes](https://github.com/rparak/SMC_End_Effector_Prototype)

For more information about how to use end-effectors within the Unity3D environment, please watch the video below.

## Project Hierarchy:

```bash
[/CAD/]
Description:
  3D models of the individual parts of the end-effector.

[/Helpers/]
Description:
  Some useful Python programs to understand how to control the end-effector of Robotiq and OnRobot.

[/Unity3D_Robotics_EE/]
Description:
  The main Unity3D project with additional dependencies.
  
```

<p align="center">
  <img src="https://github.com/rparak/Unity3D_Robotics_EE/blob/main/images/Unity3D_App_1.png" width="800" height="500">
  <img src="https://github.com/rparak/Unity3D_Robotics_EE/blob/main/images/Unity3D_App_2.png" width="800" height="500">
</p>

## Result:

<p align="center">
  <a href="https://www.youtube.com/watch?v=Lx78z1vqGL0">
    <img src=https://github.com/rparak/Unity3D_Robotics_EE/blob/main/images/YouTube.png width="275" height="200">
  </a>
</p>

## Contact Info:
Roman.Parak@outlook.com

## Citation (BibTex)
```bash
@misc{RomanParak_Unity3D,
  author = {Roman Parak},
  title = {A digital-twins in the field of industrial robotics integrated into the unity3d development platform},
  year = {2020-2023},
  publisher = {GitHub},
  journal = {GitHub repository},
  howpublished = {\url{https://github.com/rparak/Unity3D_Robotics_Overview}}
}
```
