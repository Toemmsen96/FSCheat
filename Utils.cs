using System;

namespace FSCheat
{
    public static class Utils
    {

        /// <summary>
        /// Example utility method to add two numbers.
        /// </summary>
        /// <param name="message">Message to display</param>
        public static void DisplayMessage(string message)
        {
            Plugin.logger.LogInfo(message);
            CTDynamicModMenu.CTDynamicModMenu.Instance.DisplayMessage(message);
        }
    }
}