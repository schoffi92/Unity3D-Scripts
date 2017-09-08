using System.Collections;
using System.Collections.Generic;
using System;

namespace Calc
{
    public class Angle
    {
        public struct Rotation
        {
            public float alpha;
            public float times;

            public float GetFullAngle()
            {
                return alpha + (360 * times);
            }

            public Rotation(float r)
            {
                this.alpha = Truncate(r);
                this.times = GetOverRotationCount(r);
            }
        }

        /**
         * Get Over Rotation Count
         * 
         * This function will return with the number of how many times we rotated 360 degree
         * 
         * @param float r
         * @return float
         */
        public static float GetOverRotationCount(float r)
        {
            return Convert.ToSingle(Math.Floor(r / 360));
        }

        /**
         * Truncate angle to (-360,+360) degree format
         * 
         * @param float r
         * @return float
         */
        public static float Truncate(float r)
        {
            if (Math.Abs(r) <= 360)
            {
                return r;
            }

            float times = GetOverRotationCount(r);
            return r - (360 * times);
        }

        /**
         * Convert Angle to (-180,180) degree format
         * 
         * @param flaot r
         * @return float
         */
        public static float To180(float r)
        {
            if (r < -180)
            {
                return Truncate(r) + 360;
            }

            if (r > 180)
            {
                return Truncate(r) - 360;
            }

            return Truncate(r);
        }

        /**
         * Convert Angle to (0,360) degree format
         * 
         * @param float r
         * @return float
         */
        public static float To360(float r)
        {
            if (r < 0)
            {
                return Truncate(r) + 360;
            }

            return Truncate(r);
        }

        /**
         * Get Smallest Rotation To Reach Rotation
         * The arguments cannot be bigger than 360 and smaller than 0
         * 
         * @param float from 
         * @param float to
         * @return float
         */
        public static float GetMinRotation(float from, float to)
        {
            from = To360(from);
            to = To360(to);

            if (to > from && to - from > 180)
            {
                return to - (360 - from);
            }

            if (to < from && to - from < -180)
            {
                return to + 360 - from;
            }

            return to - from;
        }
        
        /**
         * Get Largest Rotation To Reach Rotation
         * The arguments cannot be bigger than 360 and smaller than 0
         * 
         * @param float from 
         * @param float to
         * @return float
         */
        public static float GetMaxRotation(float from, float to)
        {
            float min = GetMinRotation(from, to);
            if (min >= 0) return 360 - min;
            
            return 360 + min;
        }
    }
}
