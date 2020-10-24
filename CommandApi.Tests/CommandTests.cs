using System;
using CommandApi.Models;
using Xunit;

namespace CommandApi.Tests
{
    public class CommandTests : IDisposable
    {
        private Command _testCommand;
        public CommandTests()
        {
            _testCommand = new Command
            {
                HowTo = "Do something",
                Platform = "Some platform",
                CommandLine = "Some commandline"
            };
        }
        
        [Fact]
        public void CanChangeHowTo()
        {
            // Arrange

            // Act
            _testCommand.HowTo = "Execute Unit Tests";
            
            // Assert
            Assert.Equal("Execute Unit Tests", _testCommand.HowTo);
        }

        [Fact]
        public void CanChangePlatform()
        {
            // Arrange

            // Act
            _testCommand.Platform = "xUnit in .NET Core";
            
            // Assert
            Assert.Equal("xUnit in .NET Core", _testCommand.Platform);
        }

        [Fact]
        public void CanChangeCommandLine()
        {
            // Arrange
            
            // Act
            _testCommand.CommandLine = "dotnet test";
            
            // Assert
            Assert.Equal("dotnet test", _testCommand.CommandLine);
        }

        public void Dispose()
        {
            _testCommand = null;
        }
    }
}