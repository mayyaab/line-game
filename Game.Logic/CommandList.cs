using System;
using System.Collections.Specialized;

namespace Game.Logic
{
    public class CommandList
    {
        private readonly OrderedDictionary _dictionary = new OrderedDictionary(StringComparer.InvariantCultureIgnoreCase);

        public int Count => _dictionary.Count;

        public CommandList()
        {
        }

        public bool Contains(string nameOrIndex)
        {
            if (int.TryParse(nameOrIndex, out int index))
            {
                return index >= 0 && index < _dictionary.Count;
            }

            return _dictionary.Contains(nameOrIndex);
        }

        public void Add(string name, string description, Action commandAction)
        {
            var command = new Command(name, description, commandAction);

            _dictionary.Add(name, command);
        }

        public void Add(string name, Action commandAction)
        {
            var command = new Command(name, commandAction);

            _dictionary.Add(name, command);
        }

        public void Run(string nameOrIndex)
        {
            if (int.TryParse(nameOrIndex, out int index))
            {
                RunByIndex(index);
            }
            else
            {
                RunByName(nameOrIndex);
            }
        }

        public void RunByIndex(int index)
        {
            // TG: check out-of-range

            var element = (Command)_dictionary[index];
            var action = element.Action;
            action();
        }

        public void RunByName(string name)
        {
            // TG: check out-of-range

            if (_dictionary.Contains(name))
            {
                var func = (Command)_dictionary[name];
                var action = func.Action;
                action();
            }
        }

        // TG: can be replaced with the indexing operator
        public Command GetCommand(int index)
        {
            if (index < 0)
            {
                throw new IndexOutOfRangeException("Index can not be negative");
            }

            var element = (Command)_dictionary[index];
            return element;
        }

        public void PrintCommandList()
        {
            for (int index = 0; index < _dictionary.Count; ++index)
            {
                var entry = (Command)_dictionary[index];

                Console.WriteLine("{0}|{1}: {2}", index, entry.Name, entry.Description);
            }
        }
    }
}
