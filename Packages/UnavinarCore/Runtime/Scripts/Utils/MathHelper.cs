using System;
using System.Globalization;
using UnityEngine;

namespace Unavinar.HCUnavinarCore
{
    public static class MathHelper
    {
        // 1|y     **
        //  |    *    *
        //  |   *      *
        //  |  *        *
        //  | *          *
        //  |*            *
        //  |*____________*_x
        //  0      0.5    1
        //
        // Inverse parabolic interpolation (x = [0;1])
        public static float LerpInvParabola(float x)
        {
            float xt = (x * 2.0f - 1.0f);
            float y = -(xt * xt) + 1.0f;
            return y;
        }


        // 1|y     **
        //  |    *    *
        //  |   *      *
        //  |   *      *
        //  |   *      *
        //  |  *        *
        //  |*____________*_x
        //  0      0.5    1
        //
        // Smooth inverse parabolic interpolation (x = [0;1])
        public static float LerpInvParabolaSmooth(float x)
        {
            if (x <= 0.5f)
            {
                return Mathf.SmoothStep(0.0f, 1.0f, x / 0.5f);
            }
            else
            {
                return Mathf.SmoothStep(1.0f, 0.0f, ((x - 0.5f) / 0.5f));
            }
        }

        // Smooth inverse parabolic interpolation (x = [0;1])
        public static float LerpInvParabolaSmoothShift(
            float x,
            float xShift = 0.5f,
            float yBegin = 0.0f,
            float yPeak = 1.0f,
            float yEnd = 0.0f)
        {
            if (x <= xShift)
            {
                return Mathf.SmoothStep(yBegin, yPeak, x / xShift);
            }
            else
            {
                return Mathf.SmoothStep(yPeak, yEnd, ((x - xShift) / (1.0f - xShift)));
            }
        }

        public static int CountDigit(long n)
        {
            return (int)Mathf.Floor(Mathf.Log10(n) + 1);
        }

        public static bool IsPointInTriangle(Vector3 point, Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3[] vert = { a, b, c };

            int r = 0;

            for (int i = 0, j = 2; i < 3; j = i++)
            {
                if (    ((vert[i].z > point.z) != (vert[j].z > point.z))
                    &&  (point.x < (vert[j].x - vert[i].x) * (point.z - vert[i].z) / (vert[j].z - vert[i].z) + vert[i].x))
                {
                    if (r == 0)
                    {
                        r = 1;
                    }
                    else
                    {
                        r = 0;
                    }
                }
            }

            return r > 0;
        }

        public static bool CrossPoint(
	        Vector2 segment0Point0,
	        Vector2 segment0Point1,
	        Vector2 segment1Point0,
	        Vector2 segment1Point1,
	        out Vector2 result)
        {
	        Vector3 segment0 = segment0Point1 - segment0Point0;
	        Vector3 segment1 = segment1Point1 - segment1Point0;

    
	        Vector3 prod0 = Vector3.Cross(segment0, (segment1Point0 - segment0Point0));
	        Vector3 prod1 = Vector3.Cross(segment0, (segment1Point1 - segment0Point0));

    
	        if (Mathf.Sign(prod0.z) == Mathf.Sign(prod1.z) || prod0.z == 0 || prod1.z == 0)
            {
                result = Vector2.zero;
		        return false;
            }

	        prod0 = Vector3.Cross(segment1, (segment0Point0 - segment1Point0));
	        prod1 = Vector3.Cross(segment1, (segment0Point1 - segment1Point0));

	        if (Mathf.Sign(prod0.z) == Mathf.Sign(prod1.z) || prod0.z == 0 || prod1.z == 0)
            {
                result = Vector2.zero;
		        return false;
            }

	    
		    float a = Mathf.Abs(prod0.z) / Mathf.Abs(prod1.z - prod0.z);
		    result.x = segment0Point0.x + (segment0.x * a);
		    result.y = segment0Point0.y + (segment0.y * a);

	        return true;
        }

        public static bool Approximately(float a, float b, float spread = 0.00001f)
        {
            return Mathf.Abs(a - b) < Mathf.Max(Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)) * 1E-06f, spread);
        }

        public static bool Approximately(Vector3 a, Vector3 b, float spread = 0.00001f)
        {
            return Approximately(a.x, b.x, spread) && Approximately(a.y, b.y, spread) &&
                   Approximately(a.z, b.z, spread);
        }

        public static string ToAmericanMoneyStringFormat(this double amount, int roundTo, int doIfDigitsMoreThan = 2,
            bool useFlooredAmount = false)
        {
            int step = 3;
            string flooredAmountString = Math.Floor(amount).ToString();
            string roundedToSpecifiedAmountString = Math.Round(amount, roundTo).ToString();
            int divineCount = (int) Mathf.Floor((flooredAmountString.Length - doIfDigitsMoreThan) / 3f);

            if (divineCount <= 0)
            {
                if (useFlooredAmount) return flooredAmountString;
                else return roundedToSpecifiedAmountString;
            }
            else
            {
                string separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                string divinedAmountString = flooredAmountString
                    .Insert(flooredAmountString.Length - step * divineCount, separator);

                double divinedRoundedAmount = double.Parse(divinedAmountString);
                string divinedRoundedAmountString = divinedRoundedAmount.ToString($"F{roundTo}");

                for (int i = divinedRoundedAmountString.Length - 1; i >= 0; i--)
                {
                    if (divinedRoundedAmountString[i] == separator[0])
                    {
                        divinedRoundedAmountString = divinedRoundedAmountString.Remove(i);
                        break;
                    }
                    else if (divinedRoundedAmountString[i] == '0')
                    {
                        divinedRoundedAmountString = divinedRoundedAmountString.Remove(i);
                        continue;
                    }

                    break;
                }

                return divinedRoundedAmountString + (divineCount == 1 ? "K" : divineCount == 2 ? "M" : "B");
            }
        }
    }
}
