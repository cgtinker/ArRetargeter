﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// player prefs handler to set defaults and reset values
/// </summary>
public class UserPreferences : Singleton<UserPreferences>
{
	#region Reference dictionarys for default values
	private Dictionary<string, string> StringPrefDict = new Dictionary<string, string>();

	public Dictionary<string, int> IntPrefDict = new Dictionary<string, int>()
	{
        //scene ref
        { "scene", 1 },     //sets startup scene and saves last loaded scene

        //tracker references
        { "recordCam", -1 },     // record video during cam capture
        { "recordFace", -1 },    // record video during face caputre
        { "cloud", -1 },         // recording point cloud data
    };

	private Dictionary<string, float> FloatPrefDict = new Dictionary<string, float>();

	public Dictionary<string, int> CameraConfigDict = new Dictionary<string, int>();
	public List<string> CameraConfigList = new List<string>();
	#endregion

	#region XRCamera Prefs
	//for referencing settings in face mode
	private void Awake()
	{
		if (CameraConfigList.Count == 0)
		{
			GetAvailableXRCameraConfigs();
		}
	}

	void GetAvailableXRCameraConfigs()
	{
		for (int i = 0; i < 6; i++)
		{
			var m_config = PlayerPrefs.GetString($"cameraConfig_{i}", "");

			if (m_config != "")
			{
				CameraConfigList.Add(m_config);
			}
		}
	}

	//referencing available camera settings
	public void ReferenceAvailableXRCameraConfigs(List<string> availableConfigs)
	{
		//settings can differ depending on front / back camera
		//only setting front camera settings as recording on android wasnt possible
		if (availableConfigs.Count != CameraConfigList.Count)
		{
			CameraConfigList.Clear();

			for (int i = 0; i < availableConfigs.Count; i++)
			{
				if (!CameraConfigList.Contains(availableConfigs[i]))
				{
					//reference in player prefs
					string configTitle = $"cameraConfig_{i}";
					PlayerPrefs.SetString(configTitle, availableConfigs[i]);
					//add to list
					CameraConfigList.Add(availableConfigs[i]);
				}
			}
		}
	}

	public void SetDefaultXRCameraConfig(string config)
	{
		for (int i = 0; i < CameraConfigList.Count; i++)
		{
			if (CameraConfigList[i] != config)
			{
				PlayerPrefs.SetInt(CameraConfigList[i], -1);
			}
			else
			{
				PlayerPrefs.SetInt(CameraConfigList[i], 1);
			}
		}
	}
	#endregion

	#region Get Values
	/*
	public float GetFloatPref(string key)
	{
		float defaultValue = FloatPrefDict[key];
		float m_value = PlayerPrefs.GetFloat(key: key, defaultValue: defaultValue);
		return m_value;
	}

	public int GetIntPref(string key)
	{
		int defaultValue = IntPrefDict[key];
		int m_int = PlayerPrefs.GetInt(key: key, defaultValue: defaultValue);
		return m_int;
	}

	public string GetStringPref(string key)
	{
		string defaultValue = StringPrefDict[key];
		string m_string = PlayerPrefs.GetString(key, defaultValue: defaultValue);
		return m_string;
	}
	*/
	#endregion

	private void SavePreferences()
	{
		PlayerPrefs.Save();
	}
}