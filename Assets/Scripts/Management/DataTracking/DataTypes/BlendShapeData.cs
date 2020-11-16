﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ArRetarget
{
    [System.Serializable]
    public class BlendShapeContainter
    {
        public List<BlendShapeData> blendShapeData;
    }

    /// <summary>
    /// all blendshapes at a frame
    /// </summary>
    [System.Serializable]
    public class BlendShapeData
    {
        public List<BlendShape> blendShapes;
        public int frame;
    }

    /// <summary>
    /// a blend shape contains a floating value between 0-1 and its position (name)
    /// </summary>
    [System.Serializable]
    public class BlendShape
    {
        public float value;
        public string shapeKey;
    }
}