using CoreEngineHierarchy;
using Positioning;
using Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class Command : ICommandable
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public delegate bool DelegateMethod(Positioning.Position position);
        public Action Action { get; set; }
        public Action<object> Action1 { get; set; }
        public Action<object, object> Action2 { get; set; }
        public Action<object, object, object> Action3 { get; set; }
        public Action <Position,TileMap,RenderingEngine> select { get; set; }
        public Func<object> Function1 { get; set; }
        public Func<object, object> Function2 { get; set; }
        public Func<object, object, object> Function3 { get; set; }
        public Predicate<Position> move { get; set; }

        #region Constructors

        public Command(string name, string description, Action action)
        {
            Name = name;
            Description = description;
            Action = action;
        }

        public Command(string name, string description, Action<object> action)
        {
            Name = name;
            Description = description;
            Action1 = action;
        }
        public Command(string name, string description, Action<object, object> action)
        {
            Name = name;
            Description = description;
            Action2 = action;
        }
        public Command(string name, string description, Action<object, object, object> action)
        {
            Name = name;
            Description = description;
            Action3 = action;
        }

        /// <summary>
        /// Select Overload
        /// </summary>
        public Command(string name, string description, Action<Position, TileMap, RenderingEngine> action)
        {
            Name = name;
            Description = description;
            select = action;
        }
        public Command(string name, string description, Predicate<Position> predicate)
        {
            Name = name;
            Description = description;
            move = predicate;
        }

        #endregion
    }
}
