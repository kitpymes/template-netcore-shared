using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
	public class ConfigurationBuilderExtensionsTests
    {
        #region ToEnvironment

        [TestMethod]
        public void ToEnvironment_ServiceCollection_Passing_Development_Enviroment_Returns_Development_Name()
        {
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);

            var result = services.ToEnvironment();

            Assert.IsNotNull(result);
            Assert.AreEqual(environmentNameExpected, result.EnvironmentName);
            Assert.IsTrue(result.IsDevelopment());
        }

        [TestMethod]
        public void ToEnvironment_ServiceCollection_Passing_Production_Enviroment_Returns_Production_Name()
        {
            var environmentNameExpected = "Production";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);

            var result = services.ToEnvironment();

            Assert.IsNotNull(result);
            Assert.AreEqual(environmentNameExpected, result.EnvironmentName);
            Assert.IsTrue(result.IsProduction());
        }

        [TestMethod]
        public void ToEnvironment_ServiceCollection_Passing_Staging_Enviroment_Returns_Staging_Name()
        {
            var environmentNameExpected = "Staging";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);

            var result = services.ToEnvironment();

            Assert.IsNotNull(result);
            Assert.AreEqual(environmentNameExpected, result.EnvironmentName);
            Assert.IsTrue(result.IsStaging());
        }

        [TestMethod]
        public void ToEnvironment_ServiceCollection_Passing_Custom_Enviroment_Returns_Custom_Name()
        {
            var environmentNameExpected = Guid.NewGuid().ToString();
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);

            var result = services.ToEnvironment();

            Assert.IsNotNull(result);
            Assert.AreEqual(environmentNameExpected, result.EnvironmentName);
            Assert.IsTrue(result.IsEnvironment(environmentNameExpected));
        }

        [TestMethod]
        public void ToEnvironment_ServiceProvider_Passing_Development_Enviroment_Returns_Development_Name()
        {
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);

            var result = services.BuildServiceProvider().ToEnvironment();

            Assert.IsNotNull(result);
            Assert.AreEqual(environmentNameExpected, result.EnvironmentName);
            Assert.IsTrue(result.IsDevelopment());
        }

        [TestMethod]
        public void ToEnvironment_Passing_NotFound_Service_Returns_ApplicationException()
        {
            var paramName = Guid.NewGuid().ToString();
            var messageExpected = Util.Messages.NotFound(paramName);
            var services = new ServiceCollection();

            var exceptionActual = Assert.ThrowsException<ApplicationException>(() => services.ToEnvironment().ToThrowIfNotFound(paramName));

            Assert.IsNotNull(exceptionActual);
            Assert.AreEqual(messageExpected, exceptionActual.Message);
        }

        #endregion ToEnvironment

        #region ToExists

        [TestMethod]
        public void ToExists_Passing_NotFound_Service_Returns_False()
        {
            var services = new ServiceCollection();

            var result = services.ToExists<IWebHostEnvironment>();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToExists_Passing_Valid_Service_Returns_True()
        {
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);

            var result = services.ToExists<IWebHostEnvironment>();

            Assert.IsTrue(result);
        }

        #endregion ToExists

        #region ToService

        [TestMethod]
        public void ToService_Passing_NotFound_Service_Returns_Null()
        {
            var services = new ServiceCollection();

            var serviceActual = services.ToService<IWebHostEnvironment>();

            Assert.IsNull(serviceActual);
        }

        [TestMethod]
        public void ToService_Passing_Valid_Service_Returns_Service()
        {
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);

            var serviceActual = services.ToService<IWebHostEnvironment>();

            Assert.IsNotNull(serviceActual);
            Assert.AreEqual(environmentNameExpected, serviceActual.EnvironmentName);
            Assert.IsTrue(serviceActual.IsDevelopment());
        }

        [TestMethod]
        public void ToService_Passing_NotFound_Service_Returns_ApplicationException()
        {
            var paramName = Guid.NewGuid().ToString();
            var messageExpected = Util.Messages.NotFound(paramName);

            var services = new ServiceCollection();

            var exceptionActual = Assert.ThrowsException<ApplicationException>(() => services.ToService<IWebHostEnvironment>().ToThrowIfNotFound(paramName));

            Assert.IsNotNull(exceptionActual);
            Assert.AreEqual(messageExpected, exceptionActual.Message);
        }

        #endregion ToService

        #region ToServiceMatchingInterface

        [TestMethod]
        public void ToServiceMatchingInterface_Passing_Empties_Assemblies_Returns_ApplicationException()
        {
            var assemblies = new Assembly[] { };
            var messageExpected = Util.Messages.NullOrAny(nameof(assemblies));

            var services = new ServiceCollection();

            var exceptionActual = Assert.ThrowsException<ApplicationException>
            (
                () => services.ToServiceMatchingInterface(assemblies)
            );

            Assert.IsNotNull(exceptionActual);
            Assert.AreEqual(messageExpected, exceptionActual.Message);
        }

        [TestMethod]
        public void ToServiceMatchingInterface_Passing_Valid_Assembly_Returns_Service()
        {
            var assemblies = new Assembly[] { this.GetType().Assembly };

            var services = new ServiceCollection();

            services.ToServiceMatchingInterface(assemblies);

            Assert.IsTrue(services.ToExists<IFakeLoggerService>());
        }

        #endregion ToServiceMatchingInterface

        #region ToConfiguration

        [TestMethod]
        public void ToConfiguration_Passing_Empty_DirectoryPath_Returns_ApplicationException()
        {
            string? directoryPath = string.Empty;
            var jsonFileName = "FakeAppSettings";
            var expectedMessage = Util.Messages.NotFound(nameof(directoryPath));

            var services = new ServiceCollection();
            var exceptionActual = Assert.ThrowsException<ApplicationException>
            (
                () => services.ToConfiguration(directoryPath, (jsonFileName, false, true))
            );

            Assert.AreEqual(expectedMessage, exceptionActual.Message);
        }

        [TestMethod]
        public void ToConfiguration_Passing_Invalid_DirectoryPath_Returns_ApplicationException()
        {
            string? directoryPath = Guid.NewGuid().ToString();
            var jsonFileName = "FakeAppSettings";
            var expectedMessage = Util.Messages.NotFound(nameof(directoryPath));

            var services = new ServiceCollection();
            var exceptionActual = Assert.ThrowsException<ApplicationException>
            (
                () => services.ToConfiguration(directoryPath, (jsonFileName, false, true))
            );

            Assert.AreEqual(expectedMessage, exceptionActual.Message);
        }

        [TestMethod]
        public void ToConfiguration_Passing_Valid_DirectoryPath_Invalid_JsonFileName_Not_Optional_Returns_ApplicationException()
        {
            var directoryPath = Directory.GetCurrentDirectory();
            var jsonFileName = Guid.NewGuid().ToString();
            var expectedMessage = Util.Messages.NullOrEmpty(nameof(jsonFileName));

            var services = new ServiceCollection();
            var exceptionActual = Assert.ThrowsException<ApplicationException>
            (
                () => services.ToConfiguration(directoryPath, (jsonFileName, false, true))
            );

            Assert.AreEqual(expectedMessage, exceptionActual.Message);
        }

        [TestMethod]
        public void ToConfiguration_Passing_Valid_DirectoryPath_Valid_JsonFileName_Returns_Ok()
        {
            // Hay que ir a las propiedades de archivo .json y setearle en "Copiar en el directorio de salida" => "Copiar si es posterior"
            var directoryPath = Directory.GetCurrentDirectory() + "/Fakes";
            var jsonFileName = "FakeAppSettings";

            var services = new ServiceCollection();
            var configurationActual = services
                .ToConfiguration(directoryPath, (jsonFileName, true, true))
                .ToService<IConfiguration>();

            var isEnabledFacebookActual = configurationActual.GetValue<bool>("FakeSettings:FacebookSettings:Enabled");
            var keyFacebookActual = configurationActual.GetValue<string>("FakeSettings:FacebookSettings:Key");

            Assert.AreEqual(true, isEnabledFacebookActual);
            Assert.IsNotNull(keyFacebookActual);
        }

        [TestMethod]
        public void ToConfiguration_Passing_Key_Values_Returns_Ok()
        {
            var expectedConfiguration = new (string key, string? value)[] {
                ("FakeSettings:FacebookSettings:Enabled", "true"),
                ("FakeSettings:FacebookSettings:Key", "d43fde43f43f")
            };

            var services = new ServiceCollection();
            var configurationActual = services
                .ToConfiguration(expectedConfiguration)
                .ToService<IConfiguration>();

            var isEnabledFacebookActual = configurationActual.GetValue<bool>("FakeSettings:FacebookSettings:Enabled");
            var keyFacebookActual = configurationActual.GetValue<string>("FakeSettings:FacebookSettings:Key");
            
            Assert.AreEqual(Convert.ToBoolean(expectedConfiguration[0].value), isEnabledFacebookActual);
            Assert.AreEqual(expectedConfiguration[1].value, keyFacebookActual);
        }

        #endregion ToConfiguration

        #region ToSettings

        [TestMethod]
        public void ToSettings_Passing_Values_From_AppSettingsFile_Returns_Settings()
        {
            var directoryPath = Directory.GetCurrentDirectory() + "/Fakes";
            var jsonFilename = "FakeAppSettings";

            var services = new ServiceCollection();
            var settingsActual = services
                .ToConfiguration(directoryPath, (jsonFilename, false, true))
                .ToSettings<FakeSettings>();

            Assert.IsNotNull(settingsActual);

            Assert.IsTrue(settingsActual.FacebookSettings.Enabled);
            Assert.IsNotNull(settingsActual.FacebookSettings.Key);

            Assert.IsFalse(settingsActual.GoogleSettings.Enabled);
            Assert.IsTrue(string.IsNullOrWhiteSpace(settingsActual.GoogleSettings.Key));

            Assert.IsFalse(settingsActual.LinkedinSettings.Enabled);
            Assert.IsTrue(string.IsNullOrWhiteSpace(settingsActual.LinkedinSettings.Key));
        }

        [TestMethod]
        public void ToSettings_Passing_Values_From_Memory_Returns_Settings()
        {
            var expectedConfiguration = new (string key, string? value)[] {
                ("FakeSettings:FacebookSettings:Enabled", "true"),
                ("FakeSettings:FacebookSettings:Key", "d43fde43f43f"),
                ("FakeSettings:GoogleSettings:Enabled", "false"),
                ("FakeSettings:GoogleSettings:Key", null)
            };

            var services = new ServiceCollection();
            var settingsActual = services
                .ToConfiguration(expectedConfiguration)
                .ToSettings<FakeSettings>();

            Assert.IsNotNull(settingsActual);

            Assert.AreEqual(Convert.ToBoolean(expectedConfiguration[0].value), settingsActual.FacebookSettings.Enabled);
            Assert.AreEqual(expectedConfiguration[1].value, settingsActual.FacebookSettings.Key);

            Assert.AreEqual(Convert.ToBoolean(expectedConfiguration[2].value), settingsActual.GoogleSettings.Enabled);
            Assert.AreEqual(expectedConfiguration[3].value, settingsActual.GoogleSettings.Key);

            Assert.IsFalse(settingsActual.LinkedinSettings.Enabled);
            Assert.IsTrue(string.IsNullOrWhiteSpace(settingsActual.LinkedinSettings.Key));
        }

        #endregion ToSettings
    }
}
