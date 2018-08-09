using System;
using Pong.IO;
using System.Linq;
using System.Collections.Generic;

namespace Pong.Menu
{
    class CommandSelection<TCommand>: IMenu where TCommand: Enum
    {
        public string Heading { get; }
        public Dictionary<TCommand, string> Descriptions { get; } = new Dictionary<TCommand, string>();

        public event EventHandler<TCommand> CommandSelected = delegate {};

        readonly HashSet<TCommand> _disabledCommands = new HashSet<TCommand>();
        readonly Dictionary<TCommand, HashSet<Action>> _callbacks = new Dictionary<TCommand, HashSet<Action>>();

        public CommandSelection(string heading = null)
        {
            Heading = heading;

            CommandSelected += (object sender, TCommand command) => {
                foreach (KeyValuePair<TCommand, HashSet<Action>> item in _callbacks) {
                    if (!EqualityComparer<TCommand>.Default.Equals(item.Key, command))
                        continue;
                    
                    foreach (Action callback in item.Value) {
                        callback();
                    }

                    return;
                }
            };
        }

        public void Disable(TCommand command)
            => _disabledCommands.Add(command);
        
        public void Enable(TCommand command)
            => _disabledCommands.Remove(command);
        
        public void On(TCommand command, Action callback)
        {
            if (callback == null)
                return;
            
            if (!_callbacks.TryGetValue(command, out HashSet<Action> callbacks)) {
                callbacks = new HashSet<Action>();
                _callbacks.Add(command, callbacks);
            }

            callbacks.Add(callback);
        }

        public TCommand GetSelectedCommand()
        {
            TCommand[] commands = Enum
                .GetValues(typeof(TCommand))
                .Cast<TCommand>()
                .Where(command => !_disabledCommands.Contains(command))
                .ToArray();

            if (commands.Length == 0) {
                throw new InvalidOperationException("Command selection contains no commands.");
            }

            Console.WriteLine(string.IsNullOrEmpty(Heading) ? "Select a command" : Heading);

            for (int i = 0; i < commands.Length; i++) {
                Console.WriteLine($"{i + 1}) {GetDescription(commands[i])}");
            }

            Output.LineBreak();

            int num = Input.GetInt("Command", n => n > 0 && n <= commands.Length);

            return commands[num - 1];
        }

        string GetDescription(TCommand command)
        {
            if (!Descriptions.TryGetValue(command, out string desc)) {
                return command.ToString();
            }

            return desc;
        }

        public void Draw()
            => CommandSelected(this, GetSelectedCommand());
    }
}