using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(FontTable), true)]
class FontTableEditor : LocalizedAssetTableEditor<Font> { }

