using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Editors
{
    [CustomEditor(typeof(CircleSegmentBuilder))]
    public class CircleSegmentBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CircleSegmentBuilder circleSegmentBuilder = (CircleSegmentBuilder)FindObjectOfType(typeof(CircleSegmentBuilder));

            if (GUILayout.Button("BuildCircle"))
            {
                circleSegmentBuilder.CircleSegmentBuild();
            }
        }
    }
}
