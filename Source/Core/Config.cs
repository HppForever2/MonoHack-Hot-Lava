using System;
using System.Globalization;
using System.IO;

namespace Hot_Lava_Cheat.Source.Core
{
    public class Config
    {
        private static string strConfigFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Furion HotLava");
        private static string strConfigFile = Path.Combine(strConfigFolder, "config.ini");

        public static void Save()
        {
            try
            {
                if (!Directory.Exists(strConfigFolder))
                    Directory.CreateDirectory(strConfigFolder);

                using (StreamWriter sw = new StreamWriter(strConfigFile))
                {
                    sw.WriteLine("[Main]");
                    sw.WriteLine($"bEnable_ExperienceTo60LVL={Vars.sTab.sMain.bEnable_ExperienceTo60LVL}");
                    sw.WriteLine($"bEnableGodMode={Vars.sTab.sMain.bEnableGodMode}");
                    sw.WriteLine($"bMultiplied100X_Currency={Vars.sTab.sMain.bMultiplied100X_Currency}");
                    sw.WriteLine($"bUnlock_WorldsAndToys={Vars.sTab.sMain.bUnlock_WorldsAndToys}");
                    sw.WriteLine($"bGetAllCards={Vars.sTab.sMain.bGetAllCards}");
                    sw.WriteLine($"flGravityMultiplier={Vars.sTab.sMain.flGravityMultiplier.ToString(CultureInfo.InvariantCulture)}");
                    sw.WriteLine($"flVelocityMultiplier={Vars.sTab.sMain.flVelocityMultiplier.ToString(CultureInfo.InvariantCulture)}");
                    sw.WriteLine($"bEnableBhop={Vars.sTab.sMain.bEnableBhop}");
                    sw.WriteLine($"kcBhopKey={Vars.sTab.sMain.kcBhopKey}");
                    sw.WriteLine($"iBhopMode={Vars.sTab.sMain.iBhopMode}");

                    sw.WriteLine();
                    sw.WriteLine("[MR]");
                    sw.WriteLine($"kcRecordKey={Vars.sTab.sMR.kcRecordKey}");
                    sw.WriteLine($"bRecordKeyToggle={Vars.sTab.sMR.bRecordKeyToggle}");
                    sw.WriteLine($"kcPlaybackKey={Vars.sTab.sMR.kcPlaybackKey}");
                    sw.WriteLine($"bPlaybackKeyToggle={Vars.sTab.sMR.bPlaybackKeyToggle}");
                    sw.WriteLine($"kcRewindKey={Vars.sTab.sMR.kcRewindKey}");
                    sw.WriteLine($"kcApplyFramesKey={Vars.sTab.sMR.kcApplyFramesKey}");
                    sw.WriteLine($"kcInitNewRecordKey={Vars.sTab.sMR.kcInitNewRecordKey}");
                    sw.WriteLine($"kcRewindSpeedUpKey={Vars.sTab.sMR.kcRewindSpeedUpKey}");
                    sw.WriteLine($"kcRewindSpeedDownKey={Vars.sTab.sMR.kcRewindSpeedDownKey}");
                    sw.WriteLine($"kcRewindForwardKey={Vars.sTab.sMR.kcRewindForwardKey}");
                    sw.WriteLine($"kcRewindBackwardKey={Vars.sTab.sMR.kcRewindBackwardKey}");
                    sw.WriteLine($"iRecordDelayFrames={Vars.sTab.sMR.iRecordDelayFrames}");
                    sw.WriteLine($"flRecordSlowmotion={Vars.sTab.sMR.flRecordSlowmotion.ToString(CultureInfo.InvariantCulture)}");
                    sw.WriteLine($"iRewindSpeedIndex={Vars.sTab.sMR.iRewindSpeedIndex}");
                    sw.WriteLine($"bTeleportPlaybackToStart={Vars.sTab.sMR.bTeleportPlaybackToStart}");

                    sw.WriteLine();
                    sw.WriteLine("[Other]");
                    sw.WriteLine($"iLanguage={Vars.sTab.sOther.iLanguage}");
                    sw.WriteLine($"iTheme={Vars.sTab.sOther.iTheme}");
                }
            }

            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[Config] Save error: {ex.Message}");
            }
        }

        public static void Load()
        {
            try
            {
                if (!File.Exists(strConfigFile))
                    return;

                string strCurrentSection = "";

                foreach (string strLine in File.ReadAllLines(strConfigFile))
                {
                    string strTrimmed = strLine.Trim();

                    if (string.IsNullOrEmpty(strTrimmed) || strTrimmed.StartsWith(";"))
                        continue;

                    if (strTrimmed.StartsWith("[") && strTrimmed.EndsWith("]"))
                    {
                        strCurrentSection = strTrimmed.Substring(1, strTrimmed.Length - 2);
                        continue;
                    }

                    string[] arrParts = strTrimmed.Split('=');

                    if (arrParts.Length != 2)
                        continue;

                    string strKey = arrParts[0].Trim();
                    string strValue = arrParts[1].Trim();

                    try
                    {
                        if (strCurrentSection == "Main")
                        {
                            switch (strKey)
                            {
                                case "bEnable_ExperienceTo60LVL":
                                    Vars.sTab.sMain.bEnable_ExperienceTo60LVL = bool.Parse(strValue);
                                    break;
                                case "bEnableGodMode":
                                    Vars.sTab.sMain.bEnableGodMode = bool.Parse(strValue);
                                    break;
                                case "bMultiplied100X_Currency":
                                    Vars.sTab.sMain.bMultiplied100X_Currency = bool.Parse(strValue);
                                    break;
                                case "bUnlock_WorldsAndToys":
                                    Vars.sTab.sMain.bUnlock_WorldsAndToys = bool.Parse(strValue);
                                    break;
                                case "bGetAllCards":
                                    Vars.sTab.sMain.bGetAllCards = bool.Parse(strValue);
                                    break;
                                case "flGravityMultiplier":
                                    Vars.sTab.sMain.flGravityMultiplier = ParseFloat(strValue);
                                    break;
                                case "flVelocityMultiplier":
                                    Vars.sTab.sMain.flVelocityMultiplier = ParseFloat(strValue);
                                    break;
                                case "bEnableBhop":
                                    Vars.sTab.sMain.bEnableBhop = bool.Parse(strValue);
                                    break;
                                case "bAutoBhop":
                                    Vars.sTab.sMain.bEnableBhop = bool.Parse(strValue);
                                    break;
                                case "kcBhopKey":
                                    Vars.sTab.sMain.kcBhopKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "iBhopMode":
                                    Vars.sTab.sMain.iBhopMode = ParseInt(strValue);
                                    break;
                            }
                        }

                        else if (strCurrentSection == "MR")
                        {
                            switch (strKey)
                            {
                                case "kcRecordKey":
                                    Vars.sTab.sMR.kcRecordKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "bRecordKeyToggle":
                                    Vars.sTab.sMR.bRecordKeyToggle = bool.Parse(strValue);
                                    break;
                                case "kcPlaybackKey":
                                    Vars.sTab.sMR.kcPlaybackKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "bPlaybackKeyToggle":
                                    Vars.sTab.sMR.bPlaybackKeyToggle = bool.Parse(strValue);
                                    break;
                                case "kcRewindKey":
                                    Vars.sTab.sMR.kcRewindKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "kcApplyFramesKey":
                                    Vars.sTab.sMR.kcApplyFramesKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "kcInitNewRecordKey":
                                    Vars.sTab.sMR.kcInitNewRecordKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "kcRewindSpeedUpKey":
                                    Vars.sTab.sMR.kcRewindSpeedUpKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "kcRewindSpeedDownKey":
                                    Vars.sTab.sMR.kcRewindSpeedDownKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "kcRewindForwardKey":
                                    Vars.sTab.sMR.kcRewindForwardKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "kcRewindBackwardKey":
                                    Vars.sTab.sMR.kcRewindBackwardKey = (UnityEngine.KeyCode)Enum.Parse(typeof(UnityEngine.KeyCode), strValue);
                                    break;
                                case "iRecordDelayFrames":
                                    Vars.sTab.sMR.iRecordDelayFrames = ParseInt(strValue);
                                    break;
                                case "flRecordSlowmotion":
                                    Vars.sTab.sMR.flRecordSlowmotion = ParseFloat(strValue);
                                    break;
                                case "iRewindSpeedIndex":
                                    Vars.sTab.sMR.iRewindSpeedIndex = ParseInt(strValue);
                                    break;
                                case "bTeleportPlaybackToStart":
                                    Vars.sTab.sMR.bTeleportPlaybackToStart = bool.Parse(strValue);
                                    break;
                            }
                        }

                        else if (strCurrentSection == "Other")
                        {
                            switch (strKey)
                            {
                                case "iLanguage":
                                    Vars.sTab.sOther.iLanguage = ParseInt(strValue);
                                    break;
                                case "iTheme":
                                    Vars.sTab.sOther.iTheme = ParseInt(strValue);
                                    NS_Visuals.Themes.ApplyTheme(Vars.sTab.sOther.iTheme);
                                    break;
                            }
                        }
                    }

                    catch (Exception exLine)
                    {
                        UnityEngine.Debug.LogError($"[Config] Load line error ({strCurrentSection}:{strKey}={strValue}): {exLine.Message}");
                    }
                }
            }

            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[Config] Load error: {ex.Message}");
            }
        }

        public static void OpenFolder()
        {
            try
            {
                if (!Directory.Exists(strConfigFolder))
                    Directory.CreateDirectory(strConfigFolder);

                System.Diagnostics.Process.Start(strConfigFolder);
            }

            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[Config] OpenFolder error: {ex.Message}");
            }
        }

        public static void OpenFile()
        {
            try
            {
                if (!File.Exists(strConfigFile))
                    Save();

                System.Diagnostics.Process.Start(strConfigFile);
            }

            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[Config] OpenFile error: {ex.Message}");
            }
        }

        public static string GetConfigFolderPath()
        {
            return strConfigFolder;
        }

        public static string GetConfigFilePath()
        {
            return strConfigFile;
        }

        private static float ParseFloat(string strValue)
        {
            if (float.TryParse(strValue, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float flInvariant))
                return flInvariant;

            if (float.TryParse(strValue, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out float flCurrent))
                return flCurrent;

            if (float.TryParse(strValue.Replace(',', '.'), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out float flNormalized))
                return flNormalized;

            throw new FormatException($"Invalid float value: {strValue}");
        }

        private static int ParseInt(string strValue)
        {
            if (int.TryParse(strValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int iInvariant))
                return iInvariant;

            if (int.TryParse(strValue, NumberStyles.Integer, CultureInfo.CurrentCulture, out int iCurrent))
                return iCurrent;

            throw new FormatException($"Invalid int value: {strValue}");
        }
    }
}