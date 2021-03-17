using System;
using ProjectD.Interfaces;

namespace ProjectD.Commands
{
    public class CommandSander : ICommand
    {
        private readonly Action commandToBeExec;

        public CommandSander(Action commandToBeExec, CommandHandler commandHandler)
        {
            this.commandToBeExec = commandToBeExec;
            commandHandler.AddCommand(this);
        }

        public void Execute()
        {
            commandToBeExec();
        }
    }
}