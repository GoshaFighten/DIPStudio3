using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DymAnalizCalculation
{
    class Vector
    {
        public double[] value; //can make it private, but have to define []

        public Vector()
        { 
            value = new double[]{};
        }
        public Vector(int count)
        {
            List<double> tempList = new List<double>();
            for (int i = 0; i <= count; i++)
                tempList.Add(0);
            value = tempList.ToArray();
        }
        public Vector(double[] array)
        {
            value = array;
        }
        public Vector(double[,] matrix, int column)
        {
            List<double> tempList = new List<double>();
            for (int i = 0; i < matrix.GetLength(1); i++) 
                tempList.Add(matrix[column, i]);
            value = tempList.ToArray();
        }

        public static Vector operator + (Vector vector0, Vector vector1)
        {
            Vector tempVector;
            int minLength;
            if (vector0.value.Length > vector1.value.Length)
            {
                tempVector = vector0;
                minLength = vector1.value.Length;
            }
            else
            {
                tempVector = vector1;
                minLength = vector0.value.Length;
            }
            for (int i = 0; i < minLength; i++)
            {
                tempVector.value[i] = vector0.value[i] + vector1.value[i];
            }
            return tempVector;
        }
        public static Vector operator + (Vector vector, double constant)
        {
            Vector tempVector = new Vector();
            for (int i = 0; i < vector.value.Length; i++)
            {
                tempVector.value[i] = vector.value[i] + constant;
            }
            return tempVector;
        }

        public static Vector operator - (Vector vector0, Vector vector1)
        {
            Vector tempVector;
            int minLength;
            if (vector0.value.Length > vector1.value.Length)
            {
                tempVector = vector0;
                minLength = vector1.value.Length;
            }

            else
            {
                tempVector = vector1;
                minLength = vector0.value.Length;
            }
            for (int i = 0; i < minLength; i++)
            {
                tempVector.value[i] = vector0.value[i] - vector1.value[i];
            }
            return tempVector;
        }
        public static Vector operator - (Vector vector, double constant)
        {
            Vector tempVector = new Vector();
            for (int i = 0; i < vector.value.Length; i++)
            {
                tempVector.value[i] = vector.value[i] - constant;
            }
            return tempVector;
        }

        public static Vector operator * (Vector vector, double constant)
        {
            Vector tempVector = new Vector(vector.value.Length);
            for (int i = 0; i < vector.value.Length; i++)
            {
                tempVector.value[i] = vector.value[i] * constant;
            }
            return tempVector;
        }

        public static Vector operator / (Vector vector, double constant)
        {
            Vector tempVector = new Vector(vector.value.Length);
            for (int i = 0; i < vector.value.Length; i++)
            {
                tempVector.value[i] = vector.value[i] / constant;
            }
            return tempVector;
        }

        public static Vector operator ^ (Vector vector, double constant)
        {
            Vector tempVector = new Vector(vector.value.Length);
            for (int i = 0; i < vector.value.Length; i++)
            {
                tempVector.value[i] = Math.Pow(vector.value[i], constant);
            }
            return tempVector;
        }
        public static double VectorMax (Vector vector)
        {
            double[] tempValue = vector.value;
            tempValue.OrderBy(x=>x);
            return tempValue[0];
        }
    }
}
