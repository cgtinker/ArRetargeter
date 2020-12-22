﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;

namespace ArRetarget
{
    public class TrackingDataManager : MonoBehaviour
    {
        #region refs
        private string persistentPath;
        public bool _recording = false;
        public bool captureIntrinsics = true;
        private int frame = 0;
        private ARSession arSession;

        private List<IGet<int, bool>> getters = new List<IGet<int, bool>>();
        private List<IJson> jsons = new List<IJson>();
        private List<IInit<string>> inits = new List<IInit<string>>();
        private List<IStop> stops = new List<IStop>();
        private List<IPrefix> prefixs = new List<IPrefix>();
        #endregion

        #region initialize tracking session
        void Start()
        {
            //set persistent path
            persistentPath = Application.persistentDataPath;
            _recording = false;

            //match the frame rate of ar and unity updates
            if (arSession == null)
            {
                arSession = GameObject.FindGameObjectWithTag("arSession").GetComponent<ARSession>();
            }

            if (!arSession.matchFrameRate)
                arSession.matchFrameRate = true;

            Debug.Log("Session started");
        }

        //resetting as not all tracking models include all tracker interfaces
        public void ResetTrackerInterfaces()
        {
            var referencer = GameObject.FindGameObjectWithTag("referencer");
            if (referencer != null)
            {
                var script = referencer.GetComponent<TrackerReferencer>();
                script.assigned = false;
            }

            getters.Clear();
            jsons.Clear();
            inits.Clear();
            stops.Clear();
            prefixs.Clear();
        }

        //the tracking references always contains some of the following interfaces
        public void SetRecorderReference(GameObject obj)
        {
            if (obj.GetComponent<IInit<string>>() != null)
            {
                inits.Add(obj.GetComponent<IInit<string>>());
            }

            if (obj.GetComponent<IPrefix>() != null)
            {
                prefixs.Add(obj.GetComponent<IPrefix>());
            }

            if (obj.GetComponent<IJson>() != null)
            {

                jsons.Add(obj.GetComponent<IJson>());
            }

            if (obj.GetComponent<IGet<int, bool>>() != null)
            {
                getters.Add(obj.GetComponent<IGet<int, bool>>());
            }

            if (obj.GetComponent<IStop>() != null)
            {
                stops.Add(obj.GetComponent<IStop>());
            }

            Debug.Log("Received: " + obj.name);
        }
        #endregion

        #region capturing
        //called by the recording button in the capture interface
        public void ToggleRecording()
        {
            if (!_recording)
            {
                lastFrame = false;
                StartCoroutine(OnInitRetargeting());
            }

            else
            {
                OnStopRetargeting();
            }

            _recording = !_recording;

            Debug.Log($"Recording: {_recording}");
        }

        WaitForEndOfFrame waitForFrame = new WaitForEndOfFrame();
        public IEnumerator OnInitRetargeting()
        {
            Debug.Log("new folder");
            //folder to store files
            string curTime = FileManagement.GetDateTime();

            string folderPath = $"{persistentPath}/{curTime}_{prefixs[0].j_Prefix()}";
            FileManagement.CreateDirectory(folderPath);
            Debug.Log("created dir");

            //time to create dir
            yield return waitForFrame;
            //initialize trackers
            Debug.Log("init subpath");
            string subPath = $"{folderPath}/{curTime}";
            InitTrackers(subPath);

            Debug.Log("Enabled retargeting");
            //time to init
            yield return waitForFrame;
            frame = 0;
            OnEnableTracking();
        }

        //each tracker creates a file to write json data while tracking
        private void InitTrackers(string path)
        {
            Debug.Log("Attempt to init");
            foreach (var init in inits)
            {
                init.Init(path);
            }
        }

        //called by toggle button event
        private void OnStopRetargeting()
        {
            if (stops.Count > 0)
            {
                Debug.Log("stops: " + stops.Count);
                foreach (var stop in stops)
                {
                    stop.StopTracking();
                }
            }
        }

        protected virtual void OnEnableTracking()
        {
            Debug.Log("Enabled Tracking");
            Application.onBeforeRender += OnBeforeRenderPreformUpdate;
        }

        protected virtual void OnDisableTracking()
        {
            Debug.Log("Disabled Tracking");
            Application.onBeforeRender -= OnBeforeRenderPreformUpdate;
        }

        private bool lastFrame;
        //update tracking data before the render event
        protected virtual void OnBeforeRenderPreformUpdate()
        {
            if (_recording && getters.Count > 0 && !lastFrame)
            {
                GetFrameData();
            }

            else if (!_recording && getters.Count > 0 && !lastFrame)
            {
                lastFrame = true;
                GetFrameData();
                OnDisableTracking();
            }
        }

        private void GetFrameData()
        {
            frame++;
            foreach (var getter in getters)
            {
                getter.GetFrameData(frame, lastFrame);
            }
        }
        #endregion

        //Todo: fix pathing and determine which files actually needs to use old serialization method
        public string SerializeJson()
        {
            /*
            string time = FileManagement.GetDateTime();
            string dir_path = $"{persistentPath}/{time}_{prefixs[0].j_Prefix()}";
            string msg = $"{FileManagement.GetParagraph()}{time}_{prefixs[0].j_Prefix()}";

            FileManagement.CreateDirectory(dir_path);

            for (int i = 0; i < jsons.Count; i++)
            {
                string contents = jsons[i].j_String();
                string prefix = prefixs[i].j_Prefix();
                string filename = $"{time}_{prefix}.json";

                FileManagement.WriteDataToDisk(data: contents, persistentPath: dir_path, filename: filename);
            }

            //only if scene loaded
            if (PlayerPrefs.GetInt("recordCam", -1) == -1 && PlayerPrefs.GetInt("scene", -1) == 1)
            {
                return msg;
            }

            else
            {
                string tmp = $"{msg}{FileManagement.GetParagraph()}{FileManagement.GetParagraph()}recording saved to gallery";
                return tmp;
            }
            */
            return "";
        }
    }
}