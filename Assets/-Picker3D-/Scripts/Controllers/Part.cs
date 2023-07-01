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
        [ShowInInspector] [EnumPaging] public CollectableTypes ObjectType;
        [ShowInInspector] private int containerSuccessSize;

        public int ContainerSuccessSize
        {
            get => containerSuccessSize;
            set => containerSuccessSize = value;
        }

        [TableMatrix(HorizontalTitle = "PartCellDrawing", DrawElementMethod = "DrawColoredEnumElement",
            ResizableColumns = true, RowHeight = 20)]
        public bool[,] PartCellDrawing = new bool[50, 16];


        [ShowInInspector, DoNotDrawAsReference]
        [TableMatrix(HorizontalTitle = "Transposed", DrawElementMethod = "DrawColoredEnumElement",
            ResizableColumns = false, RowHeight = 20, Transpose = true)]
        public bool[,] Transposed
        {
            get => PartCellDrawing;
            set => PartCellDrawing = value;
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
                    flatArray[i * 16 + j] = PartCellDrawing[i, j];
                }
            }

            var partData = new PartData
            {
                ObjectType = ObjectType,
                ContainerSuccessSize = ContainerSuccessSize,
                CellData = flatArray
            };

            var json = JsonUtility.ToJson(partData);
            File.Delete(path);
            
            File.WriteAllText(path, json);
        }

        public void Load(string path)
        {
            if (!File.Exists(path)) return;

            var json = File.ReadAllText(path);
            var partData = JsonUtility.FromJson<PartData>(json);

            ObjectType = partData.ObjectType;
            ContainerSuccessSize = partData.ContainerSuccessSize;

            var flatArray = partData.CellData;

            for (var i = 0; i < 50; i++)
            {
                for (var j = 0; j < 16; j++)
                {
                    PartCellDrawing[i, j] = flatArray[i * 16 + j];
                }
            }
        }

#endif
        [System.Serializable]
        internal class PartData
        {
            public CollectableTypes ObjectType;
            public int ContainerSuccessSize;
            public bool[] CellData;
        }

        private void Initialize()
        {
            filePath = Application.dataPath + "/CustomCellDrawing_" + id + ".json";
        }
        
        public enum CollectableTypes
        {
            Dice,
            Hamburger,
            Pokeball,
            Fish,
            Mushroom,
            Block
        }

        [System.Serializable]
        public class SerializationHelper<T>
        {
            public T[] data;
        }
    }
}