using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Reactive
{
    public partial class TrafficSynForm : Form
    {
        private TrafficSyn _ownerAgent;
        private Bitmap _doubleBufferImage;
        private List<EvacuationAgent> _agents;

        public static Dictionary<string, Image> agentImages = new Dictionary<string, Image>();
        public static Dictionary<string, Image> traficc = new Dictionary<string, Image>();
        public static bool C;
        public static bool C1;

        public TrafficSynForm()
        {
            InitializeComponent();
            _agents = new List<EvacuationAgent>();

            for (int i = 0; i < Utils.NoExplorers; i++)
            {
                agentImages.Add("explorer" + i, Image.FromFile(@"C:\Users\Mohammed Skouti\Downloads\Reactive\Reactive\bin\car.png"));
            }
        }

        public void SetOwner(TrafficSyn a)
        {
            _ownerAgent = a;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawPlanet();
        }

        public void UpdatePlanetGUI()
        {
            UpdateAgentPositions();
            DrawPlanet();
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            DrawPlanet();
        }



        public bool IsOccupiedNearby(string pos1, string pos2)
        {
            // Check if the given position (x, y) is surrounded by occupied positions in a 5x5 area

            if (_ownerAgent.ExplorerPositions.ContainsValue(pos1) && _ownerAgent.ExplorerPositions.ContainsValue(pos2))
            {
                return true;
            }
            return false;
        }

      ///  public double time21 = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;

        private Color GetCurrentColor()
        {
                     double time22 = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
        /*int colorIndex = (int)(time22 / 10) % 2;
        if (colorIndex == 1)
        {
            // Green light is currently active
            // Check for conditions and add 5 seconds only for the green light
            if (IsOccupiedNearby("10 3", "11 3") ||//5
                IsOccupiedNearby("14 0", "14 1") ||//6
                IsOccupiedNearby("11 5", "10 5") ||//7
                IsOccupiedNearby("15 5", "16 5") ||//8
                IsOccupiedNearby("9 10", "10 10") ||//9
                IsOccupiedNearby("17 10", "16 10") ||//10
                IsOccupiedNearby("20 8", "20 10"))//11
            {
                time22 += 5;
                Console.WriteLine("time22 after adding 5 seconds for green: " + time22);
            }

        }
        if (colorIndex == 2)
        {
            // Green light is currently active

            // Check for conditions and add 5 seconds only for the green light
            if (!IsOccupiedNearby("10 3", "11 3") ||//5
                !IsOccupiedNearby("14 0", "14 1") ||//6
                !IsOccupiedNearby("11 5", "10 5") ||//7
                !IsOccupiedNearby("15 5", "16 5") ||//8
                !IsOccupiedNearby("9 10", "10 10") ||//9
                !IsOccupiedNearby("17 10", "16 10") ||//10
                !IsOccupiedNearby("20 8", "20 10"))//11
            {
                time22 += 5;
                Console.WriteLine("time22 after adding 5 seconds for red: " + time22);
            }

        }*/
        int colorIndex = (int)(time22 / 10) % 2;
            return colorIndex == 0 ? Color.Red : Color.Green;
        }


        private Color GetCurrentColor1()
        {
            double time22 = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;

            int colorIndex = (int)(time22 / 10) % 2;
  

            
            return colorIndex == 0 ? Color.Green : Color.Red;

        }


        private void DrawPlanet()
        {
            int w = pictureBox.Width;
            int h = pictureBox.Height;

            if (_doubleBufferImage != null)
            {
                _doubleBufferImage.Dispose();
                GC.Collect(); // prevents memory leaks
            }

            _doubleBufferImage = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(_doubleBufferImage);
            g.Clear(Color.White);

            int minXY = Math.Min(w, h);
            int cellSize = 20;

            if (_ownerAgent != null)
            {
                for (int i = 0; i < Utils.maze.GetLength(0); i++)
                {
                    for (int j = 0; j < Utils.maze.GetLength(1); j++)
                    {
                        if (Utils.maze[i, j] == 0)
                        {
                            g.FillRectangle(Brushes.White, i * cellSize, j * cellSize, cellSize, cellSize);
                        }
                        else if (Utils.maze[i, j] == 1)
                        {
                            g.FillRectangle(Brushes.Black, i * cellSize, j * cellSize, cellSize, cellSize);
                        }
                        else
                        {
                            g.FillEllipse(Brushes.Black, i * cellSize, j * cellSize, cellSize, cellSize);
                        }



                        if (new int[] { 5, 7, 9,10, 13,14,15,18,19,20,21,22 }.Contains(Utils.maze[i, j]))
                        {
                            Color color = GetCurrentColor();
                            if (color == Color.Red)
                            {
                                C = true; 
                            }
                            if (color == Color.Green)
                            {
                                C = false;
                            }
                            g.FillEllipse(new SolidBrush(color), i * cellSize, j * cellSize, cellSize, cellSize);
                            string numberString = Utils.maze[i, j].ToString();
                            Font font = new Font("Arial", 12);
                            Brush brush = Brushes.White;
                            SizeF stringSize = g.MeasureString(numberString, font);
                            float x = i * cellSize + (cellSize - stringSize.Width) / 2;
                            float y = j * cellSize + (cellSize - stringSize.Height) / 2;
                            g.DrawString(numberString, font, brush, x, y);
                        }

                        if (new int[] { 6, 8, 11, 12, 16, 17 }.Contains(Utils.maze[i, j]))
                        {
                            Color color = GetCurrentColor1();

                            if (color == Color.Red)
                            {
                                C1 = true;
                            }
                            if (color == Color.Green)
                            {

                                C1 = false;
                            }
                            g.FillEllipse(new SolidBrush(color), i * cellSize, j * cellSize, cellSize, cellSize);
                            string numberString = Utils.maze[i, j].ToString();
                            Font font = new Font("Arial", 12);
                            Brush brush = Brushes.White;
                            SizeF stringSize = g.MeasureString(numberString, font);
                            float x = i * cellSize + (cellSize - stringSize.Width) / 2;
                            float y = j * cellSize + (cellSize - stringSize.Height) / 2;
                            g.DrawString(numberString, font, brush, x, y);
                        }
                    }
                }

                int pos = 0;
                foreach (KeyValuePair<string, string> v in _ownerAgent.ExplorerPositions)
                {
                    string[] t = v.Value.Split();
                    int x = Convert.ToInt32(t[0]);
                    int y = Convert.ToInt32(t[1]);
                    if (agentImages.TryGetValue(v.Key, out Image agentImage))
                    {
                        g.DrawImage(agentImage, x * cellSize, y * cellSize, cellSize, cellSize);
                    }
                    else
                    {
                        g.FillEllipse(Brushes.Red, x * cellSize, y * cellSize, cellSize, cellSize);
                    }

                    pos += 1;
                }

                foreach (EvacuationAgent agent in _agents)
                {
                    int x = agent.PositionX;
                    int y = agent.PositionY;

                    g.FillEllipse(agent.Brush, x * cellSize, y * cellSize, cellSize, cellSize);
                }
            }

            Graphics pbg = pictureBox.CreateGraphics();
            pbg.DrawImage(_doubleBufferImage, 0, 0);
        }



        private void pictureBox_Click(object sender, EventArgs e)
        {
            // Not implemented
        }

        private void UpdateAgentPositions()
        {
            foreach (EvacuationAgent agent in _agents)
            {
                agent.UpdatePosition();
            }
        }

        private void AddAgent(int x, int y)
        {
            EvacuationAgent agent = new EvacuationAgent();
            _agents.Add(agent);

        }


    }

}