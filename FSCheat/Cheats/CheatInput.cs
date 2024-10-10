using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FSCheat.Cheats
{
    internal class CheatInput
    {
        public string Command { get; private set; }

        public List<string> Args { get; private set; } = new List<string>();

        public static CheatInput Parse(string input)
        {
            // Check for command and args
            Regex regex = new Regex(@"/(\S+)(?:\s+(""([^""]+)""|\S+))*");
            Match match = regex.Match(input);

            if (!match.Success)
            {
                return null;
            }

            CheatInput command = new CheatInput();
            command.Command = match.Groups[1].Value;

            // Extract parameters
            GroupCollection groups = match.Groups;
            CaptureCollection captures = groups[2].Captures;
            for (int i = 0; i < captures.Count; i++)
            {
                command.Args.Add(captures[i].Value.Trim('"'));
            }

            return command;
        }
    }
}