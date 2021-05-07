using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kitpymes.Core.Shared.Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        private DateTime DateTime { get; set; }

        [TestMethod]
        public void ToIsWeekend_Passing_Friday_DateTime_Returns_False()
        {
            var isWeekendExpected = false;

            DateTime = new DateTime(2020, 07, 03);

            var isWeekendActual = DateTime.ToIsWeekend();

            Assert.AreEqual(isWeekendExpected, isWeekendActual);
        }

        [TestMethod]
        public void ToIsWeekend_Passing_Saturday_DateTime_Returns_True()
        {
            var isWeekendExpected = true;

            DateTime = new DateTime(2020, 07, 04);

            var isWeekendActual = DateTime.ToIsWeekend();

            Assert.AreEqual(isWeekendExpected, isWeekendActual);
        }

        [TestMethod]
        public void ToIsWeekend_Passing_Sunday_DateTime_Returns_True()
        {
            var isWeekendExpected = true;

            DateTime = new DateTime(2020, 07, 05);

            var isWeekendActual = DateTime.ToIsWeekend();

            Assert.AreEqual(isWeekendExpected, isWeekendActual);
        }

        [TestMethod]
        public void ToAge_Passing_Birthday_DateTime_Returns_Age()
        {
            var ageExpected = DateTime.Now.Year - 1979;

            DateTime = new DateTime(1979, 01, 01);

            var ageActual = DateTime.ToAge();

            Assert.AreEqual(ageExpected, ageActual);
        }

        [TestMethod]
        public void ToIsLastDayOfTheMonth_Passing_NotLastDayOfMonth_Returns_False()
        {
            var isLastDayOfTheMonthExpected = false;

            DateTime = new DateTime(2020, 07, 30);

            var isLastDayOfTheMonthActual = DateTime.ToIsLastDayOfTheMonth();

            Assert.AreEqual(isLastDayOfTheMonthExpected, isLastDayOfTheMonthActual);
        }

        [TestMethod]
        public void ToIsLastDayOfTheMonth_Passing_LastDayOfMonth_Returns_True()
        {
            var isLastDayOfTheMonthExpected = true;

            DateTime = new DateTime(2020, 07, 31);

            var isLastDayOfTheMonthActual = DateTime.ToIsLastDayOfTheMonth();

            Assert.AreEqual(isLastDayOfTheMonthExpected, isLastDayOfTheMonthActual);
        }

        [TestMethod]
        public void ToEndOfTheMonth_Passing_DateTime_Returns_End_Of_Month_DateTime()
        {
            var endOfTheMonthExpected = new DateTime(2020, 07, 31);

            DateTime = new DateTime(2020, 07, 05);

            var endOfTheMonthActual = DateTime.ToEndOfTheMonth();

            Assert.AreEqual(endOfTheMonthExpected, endOfTheMonthActual);
        }

        [TestMethod]
        public void ToStartOfWeek_Passing_DateTime_Returns_Start_Of_Week_DateTime()
        {
            var startOfWeekExpected = new DateTime(2020, 06, 29);

            DateTime = new DateTime(2020, 07, 05);

            var startOfWeekActual = DateTime.ToStartOfWeek();

            Assert.AreEqual(startOfWeekExpected, startOfWeekActual);
        }

        [TestMethod]
        public void ToYesterday_Passing_DateTime_Returns_Yesterday_DateTime()
        {
            var yesterdayExpected = new DateTime(2020, 06, 04);

            DateTime = new DateTime(2020, 06, 05);

            var yesterdayActual = DateTime.ToYesterday();

            Assert.AreEqual(yesterdayExpected, yesterdayActual);
        }

        [TestMethod]
        public void ToTomorrow_Passing_DateTime_Returns_Tomorrow_DateTime()
        {
            var tomorrowExpected = new DateTime(2020, 06, 06);

            DateTime = new DateTime(2020, 06, 05);

            var tomorrowActual = DateTime.ToTomorrow();

            Assert.AreEqual(tomorrowExpected, tomorrowActual);
        }

        [TestMethod]
        public void ToSetTime_Passing_Time_String_Returns_DateTime()
        {
            var dateTimeExpected = new DateTime(2020, 06, 05, 20, 10, 05);

            DateTime = new DateTime(2020, 06, 05);

            var dateTimeActual = DateTime.ToSetTime("20:10:05");

            Assert.AreEqual(dateTimeExpected, dateTimeActual);
        }

        [TestMethod]
        public void ToSetTime_Passing_Time_Int_Returns_DateTime()
        {
            var dateTimeExpected = new DateTime(2020, 06, 05, 20, 10, 05);

            DateTime = new DateTime(2020, 06, 05);

            var dateTimeActual = DateTime.ToSetTime(20, 10, 05);

            Assert.AreEqual(dateTimeExpected, dateTimeActual);
        }
    }
}
