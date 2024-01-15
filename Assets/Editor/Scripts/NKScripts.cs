#if UNITY_EDITOR
using UnityEngine;
using System.IO;
using UnityEditor;

namespace NK.NKEditor.ScriptsCreator
{
    public class NKScripts
    {
        public struct ScriptTemplate
        {
            public TextAsset TemplateFile;
            public string DefaultFileName;
        }

        private const string DEFAULT_PATH = "Assets/Editor/ScriptTemplates";

        private static readonly ScriptTemplate[] s_ScriptsTemplates =
        {
            new() { TemplateFile = GetTextAssetFile("DefaultMonoBehaviour.cs.txt"), DefaultFileName = "NewMonoBehaviour.cs" },
            new() { TemplateFile = GetTextAssetFile("DefaultScriptableObject.cs.txt"), DefaultFileName = "NewScriptableObject.cs" },
            new() { TemplateFile = GetTextAssetFile("DefaultClass.cs.txt"), DefaultFileName = "NewDefaultClass.cs" },
            new() { TemplateFile = GetTextAssetFile("DefaultStruct.cs.txt"), DefaultFileName = "NewDefaultStruct.cs" },
            new() { TemplateFile = GetTextAssetFile("DefaultInterface.cs.txt"), DefaultFileName = "NewDefaultInterface.cs" }
        };

        public static TextAsset GetTextAssetFile(string templateFileName, string defaultPath = DEFAULT_PATH)
        {
            string path = Path.Combine(defaultPath, templateFileName);
            return AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        }

        public static void CreateNewScript(ScriptTemplate template)
        {
            string templatePath = AssetDatabase.GetAssetPath(template.TemplateFile);
            ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, template.DefaultFileName);
        }

        [MenuItem("Assets/Create/Scripts/New MonoBehaviour", priority = 52)]
        public static void CreateMonoBehaviour()
        {
            CreateNewScript(s_ScriptsTemplates[0]);
        }

        [MenuItem("Assets/Create/Scripts/New ScriptableObject")]
        public static void CreateScriptableObject()
        {
            CreateNewScript(s_ScriptsTemplates[1]);
        }

        [MenuItem("Assets/Create/Scripts/New Class")]
        public static void CreateDefaultClass()
        {
            CreateNewScript(s_ScriptsTemplates[2]);
        }

        [MenuItem("Assets/Create/Scripts/New Struct")]
        public static void CreateDefaultStruct()
        {
            CreateNewScript(s_ScriptsTemplates[3]);
        }

        [MenuItem("Assets/Create/Scripts/New Interface")]
        public static void CreateDefaultInterface()
        {
            CreateNewScript(s_ScriptsTemplates[4]);
        }
    }
}
#endif