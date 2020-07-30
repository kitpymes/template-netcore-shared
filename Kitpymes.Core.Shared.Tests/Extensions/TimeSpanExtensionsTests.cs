using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class TimeSpanExtensionsTests
    {
        #region ToFormat

        [TestMethod]
        public void ToFormat_Passing_ValidTimeSpanValue_Returns_ValueWithFormatString()
        {
            var dateTime = DateTime.Now;
            var timeSpan = new TimeSpan(dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            var valueExpected = $"{dateTime.Day}d - {dateTime.Hour}h:{dateTime.Minute}m:{dateTime.Second}s";

            var valueActual = timeSpan.ToFormat();

            Assert.AreEqual(valueExpected, valueActual);
        }

        #endregion ToFormat
    }
}
