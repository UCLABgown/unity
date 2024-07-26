using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LipSyncConstants
{
    public static List<string> MouthShape = new List<string>() { "A.A", "B.B", "C.C", "D.D", "E.E", "F.F", "G.G", "H.H", "X.X" };
    public static List<string> EyebrowShape = new List<string>() { "Angry", "Sad", "Raised" };
    public const string AnimationLocalDirectory = "/Animations/Resources/";
    public static string AnimationDirectory;
}
