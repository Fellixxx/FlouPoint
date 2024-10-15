namespace LayerDomain.EnumType.LogLevel
{
    using FluentAssertions;
    using global::Domain.EnumType.LogLevel;
    using NUnit.Framework;

    [TestFixture]
    public class LogLevelTests
    {
        [Test]
        public Task When_LogLevelIsTrace_Then_ValueIsZero()
        {
            // Given, When
            var level = LogLevel.Trace;

            // Then
            ((int)level).Should().Be(0);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_LogLevelIsDebug_Then_ValueIsOne()
        {
            // Given, When
            var level = LogLevel.Debug;

            // Then
            ((int)level).Should().Be(1);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_LogLevelIsInformation_Then_ValueIsTwo()
        {
            // Given, When
            var level = LogLevel.Information;

            // Then
            ((int)level).Should().Be(2);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_LogLevelIsWarning_Then_ValueIsThree()
        {
            // Given, When
            var level = LogLevel.Warning;

            // Then
            ((int)level).Should().Be(3);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_LogLevelIsError_Then_ValueIsFour()
        {
            // Given, When
            var level = LogLevel.Error;

            // Then
            ((int)level).Should().Be(4);
            return Task.CompletedTask;
        }

        [Test]
        public Task When_LogLevelIsFatal_Then_ValueIsFive()
        {
            // Given, When
            var level = LogLevel.Fatal;

            // Then
            ((int)level).Should().Be(5);
            return Task.CompletedTask;
        }
    }
}
