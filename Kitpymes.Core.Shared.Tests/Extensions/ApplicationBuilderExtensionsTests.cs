using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
	public class ApplicationBuilderExtensionsTests
    {
        #region ToEnvironment

        [TestMethod]
		public void ToEnvironment_Passing_Development_Enviroment_Returns_Development_Name()
		{
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToEnvironment();

            Assert.IsNotNull(result);
            Assert.AreEqual(environmentNameExpected, result?.EnvironmentName);
            Assert.IsTrue(result.IsDevelopment());
         }

        [TestMethod]
        public void ToEnvironment_Passing_Production_Enviroment_Returns_Production_Name()
        {
            var environmentNameExpected = "Production";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToEnvironment();

            Assert.IsNotNull(result);
            Assert.AreEqual(environmentNameExpected, result?.EnvironmentName);
            Assert.IsTrue(result.IsProduction());
        }

        [TestMethod]
        public void ToEnvironment_Passing_Staging_Enviroment_Returns_Staging_Name()
        {
            var environmentNameExpected = "Staging";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToEnvironment();

            Assert.IsNotNull(result);
            Assert.AreEqual(environmentNameExpected, result?.EnvironmentName);
            Assert.IsTrue(result.IsStaging());
        }

        [TestMethod]
        public void ToEnvironment_Passing_Custom_Enviroment_Returns_Custom_Name()
        {
            var environmentNameExpected = Guid.NewGuid().ToString();
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToEnvironment();

            Assert.IsNotNull(result);
            Assert.AreEqual(environmentNameExpected, result?.EnvironmentName);
            Assert.IsTrue(result.IsEnvironment(environmentNameExpected));
        }

        [TestMethod]
        public void ToEnvironment_Passing_Service_Not_Found_Returns_ApplicationException()
        {
            var paramName = typeof(IWebHostEnvironment).Name;
            var messageExpected = Util.Messages.NotFound(paramName);

            var services = new ServiceCollection();
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var exceptionActual = Assert.ThrowsException<ApplicationException>(() =>  application.ToEnvironment().ToIsNullOrEmptyWithMessageThrow(messageExpected));

            Assert.IsNotNull(exceptionActual);
            Assert.AreEqual(messageExpected, exceptionActual.Message);
        }

        #endregion ToEnvironment

        #region ToExists

        [TestMethod]
        public void ToExists_Passing_Service_Not_Found_Returns_False()
        {
            var services = new ServiceCollection();
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToExists<IWebHostEnvironment>();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToExists_Passing_Valid_Service_Returns_True()
        {
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var result = application.ToExists<IWebHostEnvironment>();

            Assert.IsTrue(result);
        }

        #endregion ToExists

        #region ToService

        [TestMethod]
        public void ToService_Passing_Service_Not_Found_Returns_Null()
        {
            var services = new ServiceCollection();
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var serviceActual = application.ToService<IWebHostEnvironment>();

            Assert.IsNull(serviceActual);
        }

        [TestMethod]
        public void ToService_Passing_Valid_Service_Returns_Service()
        {
            var environmentNameExpected = "Development";
            var services = new ServiceCollection().FakeAddEnviroment(x => x.EnvironmentName = environmentNameExpected);
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var serviceActual = application.ToService<IWebHostEnvironment>();

            Assert.IsNotNull(serviceActual);
            Assert.AreEqual(environmentNameExpected, serviceActual?.EnvironmentName);
            Assert.IsTrue(serviceActual.IsDevelopment());
        }

        [TestMethod]
        public void ToService_Passing_Service_Not_Found_Returns_ApplicationException()
        {
            var paramName = typeof(IWebHostEnvironment).Name;
            var messageExpected = Util.Messages.NotFound(paramName); ;

            var services = new ServiceCollection();
            var application = new ApplicationBuilder(services.BuildServiceProvider());

            var exceptionActual = Assert.ThrowsException<ApplicationException>(() => application.ToService<IWebHostEnvironment>().ToIsNullOrEmptyWithMessageThrow(messageExpected));

            Assert.IsNotNull(exceptionActual);
            Assert.AreEqual(messageExpected, exceptionActual.Message);
        }

        #endregion ToService
    }
}
