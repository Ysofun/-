#if UNITY_EDITOR
using UnityEditor;
#endif
using FrameworkDesign;
using UnityEngine;

namespace CounterApp
{
    public class EditorCounterApp : EditorWindow, IController
    {
        [MenuItem("EditorCounterApp/Open")]
        private static void Open()
        {
            Debug.Log("EditorCounterApp");
            CounterApp.OnRegisterPatch += app =>
            {
                app.RegisterUtility<IStorage>(new EditorPrefsStorage());
            };

            var editorCounterApp = GetWindow<EditorCounterApp>();
            editorCounterApp.name = nameof(EditorCounterApp);
            editorCounterApp.position = new Rect(100, 100, 400, 600);
            editorCounterApp.Show();
        }

        public IArchitecture GetArchitecture()
        {
            return CounterApp.MyInterface;
        }

        private void OnGUI()
        {
            if (GUILayout.Button("+"))
            {
                this.SendCommand<AddCountCommand>();
            }

            GUILayout.Label(this.GetModel<ICounterModel>().Count.Value.ToString());

            if (GUILayout.Button("-"))
            {
                this.SendCommand<SubCountCommand>();
            }
        }
    }
}
