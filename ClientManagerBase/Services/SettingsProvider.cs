using System;
using ClientManagerBase.Models;
using ClientManagerBase.Repository;

namespace ClientManagerBase.Services
{
    /// <summary>
    /// Provider for database settings.
    /// </summary>
    public static class SettingsProvider
    {
        /// <summary>
        /// Name of the last generate date setting.
        /// </summary>
        public const string LastGenerateSettingName = "LastGenerate";

        /// <summary>
        /// Name of the SMTP server name setting.
        /// </summary>
        public const string SmtpHostSettinggName = "SmtpHost";

        /// <summary>
        /// Gets the last letters generate date.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <returns>The last letters generate date.</returns>
        public static DateTime GetLastGenerateDate(IDataRepository repository)
        {
            var setting = repository.Single<Setting>(s => s.Name == LastGenerateSettingName);

            if (setting == null)
            {
                setting = new Setting(LastGenerateSettingName, DateTime.MinValue.ToString());
                repository.Add(setting);
                repository.SaveChanges();
            }

            return DateTime.Parse(setting.Value).Date;
        }

        /// <summary>
        /// Sets the last letters generate date.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="lastDate">The last letters generate date.</param>
        public static void SetLastGenerateDate(IDataRepository repository, DateTime lastDate)
        {
            var setting = repository.Single<Setting>(s => s.Name == LastGenerateSettingName);

            if (setting == null)
            {
                setting = new Setting(LastGenerateSettingName, lastDate.Date.ToString());
                repository.Add(setting);
            }
            else
            {
                setting.Value = lastDate.Date.ToString();
            }

            repository.SaveChanges();
        }

        /// <summary>
        /// Gets the SMTP host.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <returns>The SMTP host.</returns>
        public static string GetSmtpHost(IDataRepository repository)
        {
            var setting = repository.Single<Setting>(s => s.Name == SmtpHostSettinggName);

            return setting == null ? null : setting.Value;
        }
    }
}
