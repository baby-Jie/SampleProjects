using System;
using System.Collections.Generic;
using System.Windows.Controls;
using AdvancedInkcanvas.Models;

namespace AdvancedInkcanvas.CommonTools
{
    public class AppConsts
    {
        public const string MSG_SwitchInkMode = "MSG_SwitchInkMode";

        public static readonly List<string> InkModeNames;  // InkMode 字符串集合 （包含 InkCanvasEditingMode字符串集合）

        public static readonly List<string> InkCanvasEditingModeNames; // InkCanvasEditingMode 字符串集合 

        static AppConsts()
        {
            var names = Enum.GetNames(typeof(InkMode));
            InkModeNames = new List<string>(names);

            names = Enum.GetNames(typeof(InkCanvasEditingMode));

            InkCanvasEditingModeNames = new List<string>(names);
        }
    }
}
