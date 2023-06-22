using System.IO;
using _Picker3D_.Scripts.Data;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace _Picker3D_.Scripts
{
    [System.Serializable]
    public class Part
    {
        [HideInInspector] public LevelData levelData;
        [HideInInspector] public string filePath;
        [HideInInspector] public string id;

        [TableMatrix(HorizontalTitle = "Custom Cell Drawing", DrawElementMethod = "DrawColoredEnumElement",
            ResizableColumns = true, RowHeight = 20)]
        public bool[,] CustomCellDrawing = new bool[50, 16];

        [ShowInInspector, DoNotDrawAsReference]
        [TableMatrix(HorizontalTitle = "Transposed Custom Cell Drawing", DrawElementMethod = "DrawColoredEnumElement",
            ResizableColumns = false, RowHeight = 20, Transpose = true)]
        public bool[,] Transposed
        {
            get => CustomCellDrawing;
            set => CustomCellDrawing = value;
        }

        public Part()
        {
            this.id = System.Guid.NewGuid().ToString();
        }

        internal void OnLoad()
        {
            Initialize();
            Load(filePath);
        }

#if UNITY_EDITOR
        private bool DrawColoredEnumElement(Rect rect, bool value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value = !value;
                GUI.changed = true;
                Event.current.Use();

                // UnityEditor.Undo.RecordObject(levelData, "Change Cell Value");
                UnityEditor.EditorUtility.SetDirty(levelData);
                UnityEditor.AssetDatabase.SaveAssets();
                Save(filePath);
            }

            UnityEditor.EditorGUI.DrawRect(rect.Padding(1),
                value ? new Color(0.1f, 0.5f, 0.2f) : new Color(0, 0, 0, 0.5f));

            return value;
        }

        public void Save(string path)
        {
            var flatArray = new bool[50 * 16];
            for (var i = 0; i < 50; i++)
            {
                for (var j = 0; j < 16; j++)
                {
                    flatArray[i * 16 + j] = CustomCellDrawing[i, j];
                }
            }

            var json = JsonUtility.ToJson(new SerializationHelper<bool> { data = flatArray });
            File.Delete(path);
            File.WriteAllText(path, json);
        }

        public void Load(string path)
        {
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);

                var flatArray = JsonUtility.FromJson<SerializationHelper<bool>>(json).data;

                for (var i = 0; i < 50; i++)
                {
                    for (var j = 0; j < 16; j++)
                    {
                        CustomCellDrawing[i, j] = flatArray[i * 16 + j];
                    }
                }
            }
        }

#endif

        private void Initialize()
        {
            filePath = Application.persistentDataPath + "/CustomCellDrawing_" + id + ".json";
        }
    }

    [System.Serializable]
    public class SerializationHelper<T>
    {
        public T[] data;
    }
}