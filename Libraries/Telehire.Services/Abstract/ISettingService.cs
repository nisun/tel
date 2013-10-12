using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telehire.Core;
using Telehire.Data.Domain;

namespace Telehire.Services.Abstract
{
    public interface ISettingService
    {
        /// <summary>
        /// Gets a setting by identifier
        /// </summary>
        /// <param name="settingId">Setting identifier</param>
        /// <returns>Setting</returns>
        Setting GetSettingById(int settingId);

        /// <summary>
        /// Deletes a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        void DeleteSetting(Setting setting);



        T GetSettingByKey<T>(string key, T defaultValue = default(T),
           bool loadSharedValueIfNotFound = false);



        void SetSetting<T>(string key, T value,
            bool clearCache = true);

        /// <summary>
        /// Gets all settings
        /// </summary>
        /// <returns>Settings</returns>
        IList<Setting> GetAllSettings();



        bool SettingExists<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector)
            where T : ISettings, new();



        T LoadSetting<T>() where T : ISettings, new();



        void SaveSetting<T>(T settings) where T : ISettings, new();



        void SaveSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector,

            bool clearCache = true) where T : ISettings, new();



        void DeleteSetting<T>() where T : ISettings, new();



        void DeleteSetting<T, TPropType>(T settings,
            Expression<Func<T, TPropType>> keySelector) where T : ISettings, new();

        /// <summary>
        /// Clear cache
        /// </summary>
        void ClearCache();
    }
}
