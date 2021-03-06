﻿
using System;
using System.Security.Cryptography;
using System.Text;

namespace NicEnabler
{
    public class Section
    {
        public string md5;
        public string displayname {get;}
        public string section { get; }
        public string deviceid { get; }
        public Section(string line)
        {
            string[] split = line.Split('=');
            if (split.Length >= 2)
            {
                displayname = split[0].Trim();
                string[] split2 = split[1].Split(',');
                if (split2.Length >= 2)
                {
                    section = "=" + split2[0].Trim() + ",";
                    deviceid = split2[1].Trim();
                }
            }
            md5 = GetMD5Hash();
        }

        public bool Valid()
        {
            if (displayname != null && section != null && deviceid != null)
            {
                return true;
            }
            return false;
        }

        public string GetLine()
        {
            return displayname + section + deviceid;
        }

        private string GetMD5Hash()
        {
            string TextToHash = displayname + section + deviceid;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);

            return BitConverter.ToString(result);
        }
    }
}
