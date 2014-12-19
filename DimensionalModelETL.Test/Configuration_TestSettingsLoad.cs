using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DimensionalModelETL.Test
{
    [TestClass]
    public class Configuration_TestSettingsLoad
    {
        [TestMethod]
        public void CheckSettingsLoad()
        {
            var fileName = @"..\..\..\DimensionalModelETL\bin\Debug\Configuration.xml";
            var settings = Configuration.SettingsLoader.LoadSettings(fileName);

            Assert.IsNotNull(settings.KVPAccountName);
            Assert.IsNotNull(settings.KVPAccountKey);
            Assert.IsNotNull(settings.KVPConnectionString);

            Assert.IsNotNull(settings.BlobAccountName);
            Assert.IsNotNull(settings.BlobAccountKey);
            Assert.IsNotNull(settings.BlobConnectionString);

            Assert.IsNotNull(settings.SQLServerName);
            Assert.IsNotNull(settings.SQLServerDatabase);
            Assert.IsNotNull(settings.SQLServerUserID);
            Assert.IsNotNull(settings.SQLServerPassword);
            Assert.IsNotNull(settings.SQLServerConnectionString);
        }

        [TestMethod]
        public void CheckSettingsSave()
        {
            var fileName = @"tempsettings.xml";
            var testSettings = new Configuration.Settings
            {
                BlobAccountKey = "test",
                KVPAccountName = "test",
                SQLQueries = new Configuration.Settings.SQL
                {
                    Product = "test"
                }
            };

            Configuration.SettingsLoader.SaveSettings(fileName, testSettings);

            Assert.IsTrue(System.IO.File.Exists(fileName));

            System.IO.File.Delete(fileName);
        }
    }
}
