using UnityEditor;
using UnityEditor.Advertisements;
using UnityEngine;

[CustomEditor(typeof(AdsDataSO))]
public class AdsDataCustomEditor : ExtendedCustomEditor
{
    public override void OnInspectorGUI()
    {
        AdsDataSO adsData = target as AdsDataSO;

        serializedObject.Update();

        DrawAdsSettings(adsData);

        DrawAdmob(adsData);

        GUILayout.Space(10);
        serializedObject.ApplyModifiedProperties();
        DrawWatermark();
    }

    private void DrawAdsSettings(AdsDataSO data)
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawHeader("Ads Setting");

        EditorGUI.BeginDisabledGroup(false);
        SerializedProperty prop = serializedObject.FindProperty("_GDPR");
        EditorGUILayout.PropertyField(prop);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.HelpBox("Selected Ad Type will be used, Not both! ", MessageType.Info);
        DrawProperty("_adsType");

        bool isUnityAdLinked = AdvertisementSettings.enabled;

        if (data.controlInterstitial)
        {
            EditorGUI.BeginDisabledGroup(!data.InterstitialEnabled);
            EditorGUILayout.HelpBox("'Interstitial Ad Interval' is the number of gameplay count before the next interstitial is shown", MessageType.Info);
            DrawProperty("_interstitialAdInterval");
            EditorGUI.EndDisabledGroup();
        }
        if (data.controlRewarded)
        {
            EditorGUI.BeginDisabledGroup(!data.RewardedEnabled);
            DrawProperty("_rewardedAdFrequency");
            GUILayout.Space(5);
            EditorGUI.EndDisabledGroup();
        }
        if (data.controlInterstitial)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUI.BeginDisabledGroup(!data.InterstitialEnabled);
            DrawLabel("Minimum Delay Between Interstitial (seconds)");
            DrawProperty("_minDelayBetweenInterstitial", GUIContent.none);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
        }
        if (data.controlRewarded)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUI.BeginDisabledGroup(!data.RewardedEnabled);
            DrawLabel("Minimum Delay Between Rewarded (seconds)");
            DrawProperty("_minDelayBetweenRewarded", GUIContent.none);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
            GUILayout.Space(5);
        }

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawLabel("Toggle Enable Ads");

        if (!data.controlBanner)
        {
            SerializedProperty adProp = serializedObject.FindProperty("_enableBanner");
            adProp.boolValue = data.controlBanner;
        }
        else
        {
            DrawProperty("_enableBanner");
        }

        if (!data.controlInterstitial)
        {
            SerializedProperty adProp = serializedObject.FindProperty("_enableInterstitial");
            adProp.boolValue = data.controlInterstitial;
        }
        else
        {
            DrawProperty("_enableInterstitial");
        }

        if (!data.controlRewarded)
        {
            SerializedProperty adProp = serializedObject.FindProperty("_enableRewarded");
            adProp.boolValue = data.controlRewarded;
        }
        else
        {
            DrawProperty("_enableRewarded");
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();
        GUILayout.Space(20);
    }

    private void DrawAdmob(AdsDataSO data)
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        DrawHeader("Admob Ad Units");

        if (data.controlBanner)
        {
            EditorGUI.BeginDisabledGroup(!data.BannerEnabled);
            DrawProperty("idBanner");
            EditorGUI.EndDisabledGroup();
        }
        if (data.controlInterstitial)
        {
            EditorGUI.BeginDisabledGroup(!data.InterstitialEnabled);
            DrawProperty("idInterstitial");
            EditorGUI.EndDisabledGroup();
        }
        if (data.controlRewarded)
        {
            EditorGUI.BeginDisabledGroup(!data.RewardedEnabled);
            DrawProperty("idReward");
            EditorGUI.EndDisabledGroup();
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawUnityAds()
    {

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        {
            DrawHeader("Unity Settings");
            GUILayout.Space(5);

            EditorGUI.BeginDisabledGroup(true);
            SerializedProperty androidPProp = serializedObject.FindProperty("_androidGameId");
            androidPProp.stringValue = AdvertisementSettings.GetGameId(RuntimePlatform.Android);
            EditorGUILayout.PropertyField(androidPProp);
            SerializedProperty iosProp = serializedObject.FindProperty("_iosGameId");
            iosProp.stringValue = AdvertisementSettings.GetGameId(RuntimePlatform.IPhonePlayer);
            EditorGUILayout.PropertyField(iosProp);
            EditorGUI.EndDisabledGroup();

            DrawProperty("_testMode");
            GUILayout.Space(10);
        }
        {
            DrawLabel("Android Ad Unit ID");
            DrawProperty("_androInterstitialId");
            DrawProperty("_androRewardedId");
            DrawProperty("_androBannerId");
            GUILayout.Space(10);
        }
        {
            DrawLabel("iOS Ad Unit ID");
            DrawProperty("_iosInterstitialId");
            DrawProperty("_iosRewardedId");
            DrawProperty("_iosBannerId");
            GUILayout.Space(5);
        }
        EditorGUILayout.EndVertical();
    }
}
