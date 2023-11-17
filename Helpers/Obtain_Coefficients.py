# Numpy (Array computing) [pip3 install numpy]
import numpy as np
np.set_printoptions(suppress=True)
# Typing (Support for type hints)
import typing as tp

"""
Description:
    Initialization of constants.
"""
CONST_CHECK_RESULTS = False

def Polyval(coefficients: tp.List[float], x: float) -> float:
    """
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
    """

    y = 0.0; n = coefficients.size - 1
    for i, coeff_i in enumerate(coefficients):
        y += coeff_i * x ** (n - i)

    return y

def main():
    """
    Description:        
        A program to obtain the coefficients of the polynomial from the values of stroke and angle of the end-effector 
        generated by Blender.

        For more information about data generation, please see the script below:
            ../Blender/Generate_Data.py

        The coefficients will use the 'Polyval' function to transform the stroke position into an angle.

        Note:
            Use only for Robotiq and OnRobot end-effectors.
    """

    # Input data.
    #   x: The stroke position in millimetres.
    #   y: The angle of rotation in radians.
    x = np.array([-2.9802322387695312e-05, 32.37515687942505, 61.486974358558655, 84.72365140914917, 100.00056028366089], dtype=np.float64)
    y = np.array([0.0, 0.3006547802715203, 0.6013095605430406, 0.901964340814561, 1.2026191210860813], dtype=np.float64)

    # Obtain the coefficients of the polynomial fitting.
    #   Degree of the polynomial.
    n = x.size - 1
    #   Obtain the coefficients of the polynomial using the least squares method.
    coefficients = np.array(np.polyfit(x, y, n), dtype=np.float64)

    # Display the coefficients obtained from the function.
    print(f'[INFO] Polynomial coefficients: {coefficients.tolist()}')

    # Check the results.
    if CONST_CHECK_RESULTS == True:
        for _, (x_i, y_i) in enumerate(zip(x, y)):
            print(f'[INFO] Value:')
            print(f'[INFO]  >> Original: {np.round(y_i, 3)}')
            print(f'[INFO]  >> Evaluated: {np.round(Polyval(coefficients, x_i), 3)}')

if __name__ == '__main__':
    main()
