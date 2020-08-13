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

            var valueActual = timeSpan.ToFormat();

            Assert.IsTrue
            (
                valueActual.Contains(dateTime.Day.ToString() + "d") 
                || valueActual.Contains(dateTime.Hour.ToString() + "h") 
                || valueActual.Contains(dateTime.Minute.ToString() + "m")
                || valueActual.Contains(dateTime.Second.ToString() + "s")
            );
        }

        #endregion ToFormat
    }
}
