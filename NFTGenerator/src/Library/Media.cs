﻿using System.IO;

namespace NFTGenerator
{
    internal static class Media
    {
        public static void ComposeMedia(string first, string second, string res)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = GetMergeCommand(first, second, res);
            //Logger.LogInfo(startInfo.Arguments);
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        private static string GetMergeCommand(string first, string second, string res)
        {
            string firstExtension = new FileInfo(first).Extension;
            string secondExtension = new FileInfo(second).Extension;
            if (firstExtension != secondExtension)
            {
                Logger.LogError("Unable to combine two different file types: " + firstExtension + " - " + secondExtension);
                return string.Empty;
            }
            else
            {
                //Logger.LogInfo(firstExtension);
                return firstExtension switch
                {
                    ".gif" => "/C magick convert " + first + " -coalesce null: " + second + " -gravity center -layers composite " + res,
                    ".png" => "/C magick convert " + first + " " + second + " -compose atop -gravity center -composite " + res,
                    ".jpeg" => "/C magick convert " + first + " " + second + " -compose atop -gravity center -composite " + res,
                    _ => string.Empty
                };
            }
        }
    }
}