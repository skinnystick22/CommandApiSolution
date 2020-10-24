using System.Collections.Generic;
using AutoMapper;
using CommandApi.Controllers;
using CommandApi.Data;
using CommandApi.Models;
using CommandApi.Profiles;
using Moq;
using Xunit;

namespace CommandApi.Tests
{
    public class CommandsControllerTests
    {
        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDbIsEmpty()
        {
            // Arrange
            // Setup a mock of ICommandApiRepo Interface
            var mockRepo = new Mock<ICommandApiRepo>();
            mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
            
            // Setup our CommandsProfile to work with AutoMapper
            var realProfile = new CommandsProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
            
            var controller = new CommandsController(mockRepo.Object, mapper);
        }

        private List<Command> GetCommands(int number)
        {
            var commands = new List<Command>();

            if (number > 0)
            {
                commands.Add(new Command
                {
                    CommandId = 0,
                    HowTo = "How to generate a migration",
                    Platform = ".NET Core EF",
                    CommandLine = "dotnet ef migrations add <Name of Migration>"
                });
            }

            return commands;
        }
    }
}