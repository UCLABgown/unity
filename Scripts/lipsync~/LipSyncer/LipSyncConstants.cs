using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LipSyncConstants
{
    private static string a = "_mouth_Ee.pasted__M_teacher_body";
    private static string b = "motion_arm.pasted__M_teacher_body";
    //public static List<string> MouthShape = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "X" };
    public static List<string> MouthShape = new List<string>() { a, b, "C", "D", "E", "F", "G", "H", "X"};
    public static List<string> EyebrowShape = new List<string>() { "Angry", "Sad", "Raised" };
    public const string AnimationLocalDirectory = "/Animations/Resources/";
    public static string AnimationDirectory;
}
