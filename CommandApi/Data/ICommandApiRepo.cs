﻿using System.Collections.Generic;
using CommandApi.Models;

namespace CommandApi.Data
{
    public interface ICommandApiRepo
    {
        bool SaveChanges();
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}