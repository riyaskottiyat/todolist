using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TODOApp
{
    public class ConfigUtility
    {
        #region Utility Methods

        /// <summary>
        /// Read integer value from config file.
        /// </summary>
        /// <param name="configKey">Name of the applicant key.</param>
        /// <returns>Integer based on the config key passed in.</returns>
        public static int ReadInteger(string configKey)
        {
            //Extract value from app.config
            string strInterval = ReadString(configKey);

            int value;
            if (int.TryParse(strInterval, out value))
            {
                return value;
            }
            else
            {
                string errMsg = string.Format("'{0}' is not a valid integer value for a valid integer.", configKey);
                throw new InvalidCastException(errMsg);
            }
        }

        /// <summary>
        /// Read string value from config file.
        /// </summary>
        /// <param name="configKey">Name of the applicant key.</param>
        /// <returns>Integer based on the config key passed in.</returns>
        public static string ReadString(string configKey)
        {
            //Extract value from app.config
            string strConfigValue = ConfigurationManager.AppSettings[configKey];

            if (string.IsNullOrEmpty(strConfigValue))
            {
                string errMsg = string.Format("There is no '{0}' key in app.config.", configKey);
                throw new ConfigurationErrorsException(errMsg);
            }

            return strConfigValue;
        }

        #endregion
    }
}