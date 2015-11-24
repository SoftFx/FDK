namespace Mql2Fdk
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public partial class MqlAdapter
    {
        /// <summary>
        /// Returns the absolute value of the specified numeric value.
        /// </summary>
        /// <param name="value">a numeric value</param>
        /// <returns>the absolute value</returns>
        protected static double MathAbs(double value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Returns the absolute value of the specified numeric value.
        /// </summary>
        /// <param name="value">a numeric value</param>
        /// <returns>the absolute value</returns>
        protected static int MathAbs(int value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// The MathArccos function returns the arccosine of x within the range 0 to π (in radians).
        /// If x is less than -1 or exceeds 1, the MathArccos returns NaN (indeterminate value).
        /// </summary>
        /// <param name="x">value between -1 and 1 the arccosine of which to be calculated.</param>
        /// <returns></returns>
        protected static double MathArccos(double x)
        {
            return Math.Acos(x);
        }

        /// <summary>
        /// The MathArcsin function returns the arcsine of x in the range -π/2 to π/2 radians.
        /// If x is less than -1 or exceeds 1, the arcsine returns NaN (indeterminate value).
        /// </summary>
        /// <param name="x">value the arcsine of which to be calculated</param>
        /// <returns></returns>
        protected static double MathArcsin(double x)
        {
            return Math.Asin(x);
        }

        /// <summary>
        /// The MathArctan returns the arctangent of x.
        /// If x is 0, MathArctan returns 0. MathArctan returns a value within the range of -π/2 to π/2 radians.
        /// </summary>
        /// <param name="x">A number representing a tangent.</param>
        /// <returns></returns>
        protected static double MathArctan(double x)
        {
            return Math.Atan(x);
        }

        /// <summary>
        /// The MathCeil function returns a numeric value representing the smallest integer that exceeds or equals to x. 
        /// </summary>
        /// <param name="x">numeric value</param>
        /// <returns></returns>
        protected static double MathCeil(double x)
        {
            return Math.Ceiling(x);
        }

        /// <summary>
        /// Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="value">An angle measured in radians.</param>
        /// <returns></returns>
        protected static double MathCos(double value)
        {
            return Math.Cos(value);
        }

        /// <summary>
        /// Returns the value of e raised to the power of d. At overflow, the function returns INF (infinity), and it returns 0 at underflow. 
        /// </summary>
        /// <param name="d">A number specifying the power.</param>
        /// <returns></returns>
        protected static double MathExp(double d)
        {
            return Math.Exp(d);
        }

        /// <summary>
        /// The MathFloor function returns a numeric value representing the largest integer that is less than or equal to x.
        /// </summary>
        /// <param name="x">a numeric value</param>
        /// <returns></returns>
        protected static double MathFloor(double x)
        {
            return Math.Floor(x);
        }

        /// <summary>
        /// The MathLog function returns the natural logarithm of x if successful.
        /// If x is negative, these functions return NaN (indeterminate value). If x is 0, they return INF (infinity).
        /// </summary>
        /// <param name="x">a value logarithm of which to be found</param>
        /// <returns></returns>
        protected static double MathLog(double x)
        {
            return Math.Log(x);
        }

        /// <summary>
        /// Returns the maximum value of two numeric values.
        /// </summary>
        /// <param name="value1">the first numeric value</param>
        /// <param name="value2">the second numeric value</param>
        /// <returns>maximum of two values</returns>
        protected static double MathMax(double value1, double value2)
        {
            return Math.Max(value1, value2);
        }

        /// <summary>
        /// Returns the maximum value of two numeric values.
        /// </summary>
        /// <param name="value1">the first numeric value</param>
        /// <param name="value2">the second numeric value</param>
        /// <returns>maximum of two values</returns>
        protected static int MathMax(int value1, int value2)
        {
            return Math.Max(value1, value2);
        }

        /// <summary>
        /// Returns the minimum value of two numeric values.
        /// </summary>
        /// <param name="value1">the first numeric value</param>
        /// <param name="value2">the second numeric value</param>
        /// <returns>minimum of two values</returns>
        protected static double MathMin(double value1, double value2)
        {
            return Math.Min(value1, value2);
        }

        /// <summary>
        /// Returns the minimum value of two numeric values.
        /// </summary>
        /// <param name="value1">the first numeric value</param>
        /// <param name="value2">the second numeric value</param>
        /// <returns>minimum of two values</returns>
        protected static int MathMin(int value1, int value2)
        {
            return Math.Min(value1, value2);
        }

        /// <summary>
        /// The function returns the floating-point remainder of division of two numbers.
        /// 
        /// The MathMod function calculates the floating-point remainder f  of x / y  such that x = i * y + f , where i  is an integer, f  has the same sign as x, and the absolute value of f  is less than the absolute value of y. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        protected static double MathMod(double value, double value2)
        {
            return (int)value % (int)value2;
        }

        /// <summary>
        /// Returns the value of the base expression raised to the specified power (exponent value).
        /// </summary>
        /// <param name="basis">base value</param>
        /// <param name="exponent">exponent value</param>
        /// <returns></returns>
        protected static double MathPow(double basis, double exponent)
        {
            return Math.Pow(basis, exponent);
        }

        /// <summary>
        /// The MathRand function returns a pseudorandom integer within the range of 0 to 32767
        /// </summary>
        /// <returns></returns>
        protected int MathRand()
        {
            return this.random.Next(1 + short.MaxValue);
        }

        /// <summary>
        /// Returns value rounded to the nearest integer of the specified numeric value.
        /// </summary>
        /// <param name="value">a numeric value to be rounded.</param>
        /// <returns></returns>
        protected static double MathRound(double value)
        {
            return Math.Round(value);
        }

        /// <summary>
        /// Returns the sine of the specified angle.
        /// </summary>
        /// <param name="value">an angle measured in radians</param>
        /// <returns></returns>
        protected static double MathSin(double value)
        {
            return Math.Sin(value);
        }

        /// <summary>
        /// The MathSqrt function returns the square root of x.
        /// If x is negative, MathSqrt returns an indefinite (same as a quiet NaN).
        /// </summary>
        /// <param name="x">positive numeric value</param>
        /// <returns></returns>
        protected static double MathSqrt(double x)
        {
            return Math.Sqrt(x);
        }

        /// <summary>
        /// The MathSrand() function sets the starting point for generating a series of pseudorandom integers.
        /// </summary>
        /// <param name="seed"></param>
        protected void MathSrand(int seed)
        {
            this.random = new Random(seed);
        }

        /// <summary>
        /// MathTan returns the tangent of x.
        /// If x is greater than or equal to 263, or less than or equal to -263, a loss of significance in the result occurs, in which case the function returns an indefinite (same as a quiet NaN).
        /// </summary>
        /// <param name="x">angle in radians</param>
        /// <returns></returns>
        protected static double MathTan(double x)
        {
            return Math.Tan(x);
        }
    }
}
