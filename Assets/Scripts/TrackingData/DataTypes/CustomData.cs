﻿using UnityEngine;
using System.Collections.Generic;

namespace ArRetarget
{
    /// <summary>
    /// descripes position and rotation at a given frame
    /// </summary>
    [System.Serializable]
    public class PoseData
    {
        public Vector3 pos;
        public Vector3 rot;
        public int frame;
    }

    #region vector data
    /// <summary>
    /// descripes a list of vertecies from a mesh at a given frame
    /// </summary>
    [System.Serializable]
    public class MeshData
    {
        public List<Vector3> pos;
        public int frame;
    }

    /// <summary>
    /// moving mesh vertecies container for json serialization
    /// </summary>
    [System.Serializable]
    public class MeshDataContainer
    {
        public List<MeshData> meshDataList;
    }


    [System.Serializable]
    public class PointCloud
    {
        public List<Vector3> points;
    }


    [System.Serializable]
    public class RefereceData
    {
        public List<Vector3> anchorData;
    }
    #endregion

    #region screen pos data
    /// <summary>
    /// contains a normalized screen pos and a global object pos at a given frame
    /// </summary>
    [System.Serializable]
    public class ScreenPosData
    {
        public Vector3 screenPos;
        public Vector3 objPos;
        public int frame;
    }

    [System.Serializable]
    public class ScreenPosContainer
    {
        public List<ScreenPosData> screenPosData;
    }
    #endregion

    #region blend shape data
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

    #endregion

    #region camera intrinsics data
    /// <summary>
    /// container for json serialization containing camera intrinsics
    /// </summary>
    public class CameraIntrinsicsContainer
    {
        //public List<CameraIntrinsics> cameraIntrinsics;
        public List<CameraProjectionMatrix> cameraProjection;
        public CameraConfig cameraConfig;
        public ScreenResolution resolution;
    }

    [System.Serializable]
    public class CameraProjectionMatrix
    {
        public Matrix4x4 cameraProjectionMatrix;
        public int frame;
    }

    [System.Serializable]
    public class ScreenResolution
    {
        public float screenWidth;
        public float screenHeight;
    }

    [System.Serializable]
    public class CameraConfig
    {
        public int fps;
        public float width;
        public float height;
    }
    #endregion

    #region viewer containers
    [System.Serializable]
    public class CameraPoseContainer
    {
        public List<PoseData> cameraPoseList;
    }

    [System.Serializable]
    public class BlendShapeContainter
    {
        public List<BlendShapeData> blendShapeData;
    }
    #endregion
}
