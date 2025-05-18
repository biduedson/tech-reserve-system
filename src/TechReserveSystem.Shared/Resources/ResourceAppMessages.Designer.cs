using System;
using System.Globalization;
using System.Resources;


namespace TechReserveSystem.Shared.Resources
{
    public class ResourceAppMessages
    {
        private static global::System.Globalization.CultureInfo resourceCulture;
        private static global::System.Resources.ResourceManager resourceManagerCommunication;
        private static global::System.Resources.ResourceManager resourceManagerException;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceAppMessages()
        {
        }


        public static global::System.Resources.ResourceManager ResourceManagerCommunication
        {
            get
            {
                if (resourceManagerCommunication == null)
                {
                    resourceManagerCommunication = new global::System.Resources.ResourceManager("TechReserveSystem.Shared.Resources.Communication.CommunicationMessages", typeof(ResourceAppMessages).Assembly);
                }
                return resourceManagerCommunication;
            }
        }

        public static global::System.Resources.ResourceManager ResourceManagerException
        {
            get
            {
                if (resourceManagerException == null)
                {
                    resourceManagerException = new global::System.Resources.ResourceManager("TechReserveSystem.Shared.Resources.Exception.ResourceMessagesException", typeof(ResourceAppMessages).Assembly);
                }
                return resourceManagerException;
            }
        }
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        public static string GetCommunicationMessage(string key)
        {
            return ResourceManagerCommunication.GetString(key, resourceCulture).Trim();
        }

        public static string GetExceptionMessage(string key)
        {
            return ResourceManagerException.GetString(key, resourceCulture).Trim();
        }
    }
}