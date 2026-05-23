using System;
using System.IO;
using LicenseTool;

public static class LicenseLoader
{
    public static bool isLicense = false;

    public static string desc = "开始游戏";

    public static int state = 0;

    public static void Load()
    {
        string thisMachineCode;
        thisMachineCode = LicenseInfo.GetThisMachineCode();
        string text;
        text = ".\\" + LicenseInfo.GetThisMachineCode() + ".license";
        if (!File.Exists(text))
        {
            return;
        }
        LicenseInfo licenseInfo;
        licenseInfo = new LicenseInfo();
        licenseInfo.LoadFromFile(text);
        if (!licenseInfo.isReadDone || !(thisMachineCode == licenseInfo.machineCode))
        {
            return;
        }
        DateTime dateTime;
        dateTime = new DateTime(licenseInfo.year, licenseInfo.month, licenseInfo.day);
        if (DateTime.Now < dateTime)
        {
            LicenseLoader.isLicense = true;
            LicenseLoader.desc = $"已授权 ,授权过期时间:{licenseInfo.year}-{licenseInfo.month}-{licenseInfo.day}";
            LicenseLoader.state = 1;
            if ((dateTime - DateTime.Now).TotalDays < 3.0)
            {
                LicenseLoader.desc += "即将过期!";
                LicenseLoader.state = 2;
            }
        }
        else
        {
            LicenseLoader.desc = $"授权已过期，过期时间:{licenseInfo.year}-{licenseInfo.month}-{licenseInfo.day}";
            LicenseLoader.state = -1;
        }
    }
}
