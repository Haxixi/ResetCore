using UnityEngine;
using UnityEditor;
using ResetCore.PlatformHelper;
using System.Collections.Generic;

public class TestBton {

	[MenuItem("Test/Test")]
	public static void EditorTest()
	{
        AndroidPluginBuilder.BuildMenifest(@"F:\UnityProjects\ResetCore\ResetCoreTemp\SDK\GeTui\build.xml", new List<string>());

    }
}
