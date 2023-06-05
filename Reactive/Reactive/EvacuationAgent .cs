using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactive
{
    internal class EvacuationAgent
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public Brush Brush { get; private set; }

        private Random _random;

        public EvacuationAgent()
        {
            _random = new Random(); // Initialize the _random object

            PositionX = _random.Next(Utils.maze.GetLength(0));
            PositionY = _random.Next(Utils.maze.GetLength(1));
            Brush = Brushes.Blue;
        }

        public void UpdatePosition()
        {
            int dx = _random.Next(-1, 2);
            int dy = _random.Next(-1, 2);

            PositionX += dx;
            PositionY += dy;
        }
    }
}
