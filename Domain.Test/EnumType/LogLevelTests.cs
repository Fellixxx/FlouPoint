using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test.EnumType
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using global::Domain.EnumType.LogLevel;

    [TestClass]
    public class LogLevelTests
    {
        [TestMethod]
        public void When_LogLevelIsTrace_Then_ValueIsZero()
        {
            // Given, When
            var level = LogLevel.Trace;

            // Then
            Assert.AreEqual(0, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsDebug_Then_ValueIsOne()
        {
            // Given, When
            var level = LogLevel.Debug;

            // Then
            Assert.AreEqual(1, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsInformation_Then_ValueIsTwo()
        {
            // Given, When
            var level = LogLevel.Information;

            // Then
            Assert.AreEqual(2, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsWarning_Then_ValueIsThree()
        {
            // Given, When
            var level = LogLevel.Warning;

            // Then
            Assert.AreEqual(3, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsError_Then_ValueIsFour()
        {
            // Given, When
            var level = LogLevel.Error;

            // Then
            Assert.AreEqual(4, (int)level);
        }

        [TestMethod]
        public void When_LogLevelIsFatal_Then_ValueIsFive()
        {
            // Given, When
            var level = LogLevel.Fatal;

            // Then
            Assert.AreEqual(5, (int)level);
        }
    }
}
