using System.Collections;
using System.Collections.Generic;
using System;

namespace Calc {

    public class RightTriangle {

        // sides
        public double sideA { get; set; }

        public double sideB { get; set; }

        public double sideC
        {
            get
            {
                return sideC;
            }

            set
            {
                if (value == 0)
                {
                    sideC = Math.Sqrt( (sideA * sideA) + (sideB * sideB) );
                }
                else
                {
                    sideC = value;
                }
            }
        }

        // Area, circumference
        public double area
        {
            get
            {
                return (sideA * sideB) / 2.0;
            }
        }

        public double circumference
        {
            get
            {
                return (sideA + sideB + sideC);
            }
        }

        // Angles
        public double angleA { get; set; }

        public double angleB {
            get
            {
                return 90 - angleA;
            }

            set
            {
                angleA = 90 - value;
            }
        }

        public double angleC
        {
            get
            {
                return 90.0;
            }

            set
            {
                angleA = Math.Atan(sideA / sideB) * (180 / Math.PI);
            }
        }

        // Constructor
        public RightTriangle(double a, double b, double alpha)
        {
            sideA = a;
            sideB = b;
            angleA = alpha;

            Calc();
        }

        public RightTriangle(double r, double alpha)
        {
            CirclePoint(r, alpha);
        }

        /**
         * Calculate a triangle to find a circle xy point
         * 
         * 
         */
        public void CirclePoint(double r, double alpha)
        {
            sideC = r;
            angleA = alpha;

            sideA = Math.Cos((alpha * Math.PI) / 180) * r;
            sideB = Math.Sqrt( (sideC * sideC) - (sideA * sideA) );
        }

        /**
         * Recalculate the angles and the C side
         * 
         */
        public void Calc()
        {
            sideC = 0;
            angleC = 0;
        }

        /**
         * IsValid
         * 
         * Checks the angles are valid
         * before it it will recalculate the angles and the C side
         * 
         * @param bool recalc 
         * @return bool
         */
        public bool IsValid(bool recalc = true)
        {
            if (recalc)
            {
                Calc();
            }

            if (angleA + angleB + angleC != 180)
            {
                return false;
            }

            return true;
        }

    }

}