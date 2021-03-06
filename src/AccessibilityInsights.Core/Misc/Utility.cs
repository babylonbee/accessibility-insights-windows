// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Diagnostics;

namespace AccessibilityInsights.Core.Misc
{
    public static class Utility
    {
        /// <summary>
        /// Get version from AccessibilityInsights.Core Assembly
        /// </summary>
        /// <returns></returns>
        public static string GetAppVersion()
        {
            string fileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;
            return fileVersion;
        }

        public static string GetProcessName(int processId)
        {
            if (!TryGetProcessById(processId, out Process process)) return null;

            return process?.ProcessName;
        }

        private static bool TryGetProcessById(int processId, out Process process)
        {
            process = null;

            if (processId == 0) return false;

            try
            {
                process = Process.GetProcessById(processId);
            }
            catch (ArgumentException /*ex*/)
            {
                // occurs when an invalid process id is passed to GetProcessById
                return false;
            }

            return true;
        }
    }
}
