﻿using UnityEngine;


/// <summary>
/// Color string key content value.
/// </summary>
public readonly struct ColorKey
{
    public const string Green = "green";
    public const string Yellow = "yellow";
    public const string Red = "red";
    public const string Bule = "bule";
    public const string Pubple = "Puble";
}

/// <summary>
/// The service utility helper for eaisy to use.
/// </summary>
public static class VectorUtil
{
    #region Vector
    public static float Distance(Vector3 a, Vector3 b) => (a - b).magnitude;
    public static float Distance(Vector2 a, Vector2 b) => (a - b).magnitude;

    public static Vector3 Direction(Vector3 form, Vector3 to) => (to - form).normalized;
    public static Vector2 Direction(Vector2 form, Vector2 to) => (to - form).normalized;

    #endregion


    public static Vector3 ScreenToWordPoint(Vector3 screenPos, Camera camera = null)
    {
        if (!camera)
            camera = Camera.main;

        return camera.ScreenToWorldPoint(screenPos);
    }

    public static Ray ScreenPointToRay(Vector3 screenPos, Camera camera = null)
    {
        if (!camera) camera =
                Camera.main;

        return camera.ScreenPointToRay(screenPos);
    }

    public static Vector3 WorldToScreenPoint(Vector3 worldPos, Camera camera = null)
    {
        if (!camera) camera =
                Camera.main;

        return camera.WorldToScreenPoint(worldPos);
    }
}

public static class QuaternionUtil
{
    public static readonly Quaternion Zero = Quaternion.Euler(Vector3.zero);

    public static Quaternion LookRotation2D(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, angle);
    }
}

public static class MathUtil
{
    #region Math
    public static float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
    public static int Clamp(int value, int min, int max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
    #endregion
}

public static class StringUtil
{
    #region Color
    public static string ToStringColor(this string value, string color)
    {
        string codeFont = "<color=" + color + ">";
        string codeEnd = "</color>";
        return codeFont + value + codeEnd;
    }

    public static bool IsNullOrEmpty(this string value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static bool IsNullOrSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    #endregion
}

public static class ArrayUtil
{

    public static int[] ConvertStringArrayToIntArray(this string[] stringArray)
    {
        int count = stringArray.Length;
        int[] intArray = new int[count];

        for (int i = 0; i < count; i++)
            intArray[i] = int.Parse(stringArray[i]);

        return intArray;
    }

    public static string[] ConvertIntArrayToStringArray(this int[] intArray)
    {
        int count = intArray.Length;
        string[] stringArray = new string[count];

        for (int i = 0; i < count; i++)
            stringArray[i] = intArray[i].ToString();

        return stringArray;
    }
}