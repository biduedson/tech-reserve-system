using System.Globalization;
using System.Resources;
using System;


namespace TechReserveSystem.Shared.Resources
{
    public static class ResourceAppMessages
    {
        private static CultureInfo resourceCulture = CultureInfo.CurrentCulture;

        private static readonly ResourceManager resourceManagerCommunication =
            new ResourceManager("TechReserveSystem.Shared.Resources.Communication.CommunicationMessages", typeof(ResourceAppMessages).Assembly);

        private static readonly ResourceManager resourceManagerException =
            new ResourceManager("TechReserveSystem.Shared.Resources.Exception.ResourceMessagesException", typeof(ResourceAppMessages).Assembly);

        public static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        public static string GetCommunicationMessage(string key)
        {
            return resourceManagerCommunication.GetString(key, resourceCulture);
        }

        public static string GetExceptionMessage(string key)
        {
            return resourceManagerException.GetString(key, resourceCulture);
        }
    }
}