using System;
using System.Collections;
using System.Collections.Generic;

public class Utils
{
    public static Random random = new Random();
    public static float getRandomFloat(float minValue, float maxValue)
    {
        return (float)(random.NextDouble() * (maxValue - minValue)) + minValue;
    }

    public static int getRandomInteger(int min, int max)
    {
        return random.Next(min, max);
    }
}
