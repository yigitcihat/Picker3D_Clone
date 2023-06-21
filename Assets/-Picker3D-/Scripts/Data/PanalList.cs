using System.Collections.Generic;
using System.Linq;
using _Picker3D_.Scripts.UI;

namespace _Picker3D_.Scripts.Data
{
    public class PanalList
    {
        public static string GameStartPanel = "GameStartPanel";
        public static string InGamePanel = "InGamePanel";
        public static string TutorialPanel = "TutorialPanel";
        public static string WinPanel = "WinPanel";
        public static string FailPanel = "FailPanel";

        public static readonly Dictionary<string, Picker3dPanel> Panels = new Dictionary<string, Picker3dPanel>();

        private static string[] panelIDs = new string[]
        {
            "None",
            GameStartPanel,
            InGamePanel,
            TutorialPanel,
            WinPanel,
            FailPanel
       
        };
        public static List<string> PanelIDs => panelIDs.ToList();
    }
}
