using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal abstract class CustomCheat
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Format { get; }

        public bool Handle(string message)
        {
            CheatInput command = CheatInput.Parse(message);

            if (command == null)
            {
                return false;
            }

            // Check name
            if (command.Command != this.Format.Split(' ')[0].Trim('/'))
            {
                return false;
            }

            // Execute command
            this.Execute(command);
            return true;
        }

        public abstract void Execute(CheatInput message);
    }
}