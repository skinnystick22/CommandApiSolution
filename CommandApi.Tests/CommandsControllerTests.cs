using System;
using System.Collections.Generic;
using AutoMapper;
using CommandApi.Controllers;
using CommandApi.Data;
using CommandApi.Dtos;
using CommandApi.Models;
using CommandApi.Profiles;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommandApi.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        private Mock<ICommandApiRepo> _mockRepo;
        private CommandsProfile _realProfile;
        private MapperConfiguration _configuration;
        private Mapper _mapper;

        public CommandsControllerTests()
        {
            _mockRepo = new Mock<ICommandApiRepo>();
            _realProfile = new CommandsProfile();
            _configuration = new MapperConfiguration(cfg => cfg.AddProfile(_realProfile));
            _mapper = new Mapper(_configuration);
        }
        
        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDbIsEmpty()
        {
            // Arrange
            // Setup a mock of ICommandApiRepo Interface
            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);
            
            // Act
            var result = controller.GetAllCommands();
            
            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnsOneItem_WhenDbHasOneResource()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);
            
            // Act
            var result = controller.GetAllCommands();
            
            // Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as IEnumerable<CommandReadDto>;

            Assert.Single(commands);
        }

        [Fact]
        public void GetAllCommands_Returns200Ok_WhenDbHasOneResource()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturnCorrectType_WhenDbHasOneResource()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);
        }

        [Fact]
        public void GetCommandById_Returns404NotFound_WhenNonExistentIdProvided()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCommandById_Returns200Ok_WhenValidIdProvided()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                CommandId = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Asset
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandById_ReturnsCommandReadDto_WhenValidIdProvided()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                CommandId = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        [Fact]
        public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectSubmitted()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                CommandId = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            // Act
            var result = controller.CreateCommand(new CommandCreateDto());

            // Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);
        }

        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectSubmitted()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                CommandId = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "mock"
            });
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            // Act
            var result = controller.CreateCommand(new CommandCreateDto());

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectSubmitted()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                CommandId = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);

            // Act
            var result = controller.UpdateCommand(1, new CommandUpdateDto());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIdSubmitted()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);
            
            // Act
            var result = controller.UpdateCommand(0, new CommandUpdateDto());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIdSubmitted()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);
            
            // Act
            var result = controller.PartialCommandUpdate(0, new JsonPatchDocument<CommandUpdateDto>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidResourceIdSubmitted()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                CommandId = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "mock"
            });
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);
            
            // Act
            var result = controller.DeleteCommand(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteCommand_Return404NotFound_WhenNonExistentResourceIdSubmitted()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            
            var controller = new CommandsController(_mockRepo.Object, _mapper);
            
            // Act
            var result = controller.DeleteCommand(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private IEnumerable<Command> GetCommands(int number)
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

        public void Dispose()
        {
            _mockRepo = null;
            _realProfile = null;
            _configuration = null;
            _mapper = null;
        }
    }
}