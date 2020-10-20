using System.Collections.Generic;
using CommandApi.Models;

namespace CommandApi.Data
{
    public class MockCommandApiRepo : ICommandApiRepo
    {
        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command 
                {
                    CommandId = 0,
                    HowTo = "How to generate a migration.",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".NET Core EF"
                },
                new Command
                {
                    CommandId = 1,
                    HowTo = "Run Migrations",
                    CommandLine = "dotnet ef database update",
                    Platform = ".NET Core EF"
                },
                new Command
                {
                    CommandId = 2,
                    HowTo = "List active migrations.",
                    CommandLine = "dotnet ef migrations list",
                    Platform = ".NET Core EF"
                }
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command
            {
                CommandId = 0,
                HowTo = "How to generate a migration.",
                CommandLine = "dotnet ef migrations add <Name of Migration>",
                Platform = ".NET Core EF"
            };
        }

        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}