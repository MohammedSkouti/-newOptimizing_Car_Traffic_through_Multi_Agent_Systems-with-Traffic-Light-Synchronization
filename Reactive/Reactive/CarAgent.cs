using ActressMas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Reactive
{
    public class CarAgent : Agent
    {

        private Random _random=new Random();
        private EvacuationAgent _evacuationAgent;
        private int _x, _y, _x1, _y1;
        private int x_end, y_end;
        //private bool deadEnd = false;
        //private const int deadEndWeight = 5;
        private Dictionary<string, int> toRemember = new Dictionary<string, int>();
        private string positionExit = null;
        private List<string> pathExit = null;  // Dijkistra path
        private int nrMoves = 0;

        string[] start = Utils.start.Split();

        // string[] start1 = Utils.start1.Split();

        public override void Setup()
        {
            Console.WriteLine(start);
            Console.WriteLine("Starting " + Name);
            int N1;

            N1 = Int32.Parse(this.Name.Replace("explorer", " "));
            if (N1 <= 3)
            {
                _x = Int32.Parse(start[0]);
                _y = Int32.Parse(start[1]);
            }
            else if(N1 >3&& N1 <= 6)
            {
                _x = Int32.Parse(start[2]);
                _y = Int32.Parse(start[3]);
            }
            else if(N1>6&& N1<=9)
            {
                _x = Int32.Parse(start[4]);
                _y = Int32.Parse(start[5]);
            }
            else if (N1 > 9 && N1 <= 12)
            {
                _x = Int32.Parse(start[6]);
                _y = Int32.Parse(start[7]);
            }
          /*  else if (N1 > 12 && N1 <= 15)
            {
                _x = Int32.Parse(start[8]);
                _y = Int32.Parse(start[9]);
            }*/


                //    _x1 = Int32.Parse(start[2]);
                //    _y1 = Int32.Parse(start[3]);
                //  Console.WriteLine(_x+ " "+_y+ " " +_x1+  " "+_y1);
                //    Random Nnewr= new Random(); 
                string[] end = Utils.end.Split();
            x_end = Int32.Parse(end[0]);
            y_end = Int32.Parse(end[1]);
       //     int N1,N2   ;

        //    N1 = Int32.Parse(this.Name.Replace("explorer"," "));
       //     Console.WriteLine("n1"+N1);
            //int R= Nnewr.Next(2);
           // Console.WriteLine("R"+R);
          //  if (N1 < 6)
         
            //    Console.WriteLine("first1");
                Send("planet", Utils.Str("position", _x, _y));


        }



        public void restart()
        {
            Console.WriteLine("Restarting " + Name);
            //    deadEnd = false;
            toRemember.Clear();
            positionExit = null;
            pathExit = null;
            nrMoves = 0;

            _x = Int32.Parse(start[0]);
            _y = Int32.Parse(start[1]);
            _x1 = Int32.Parse(start[2]);
            _y1 = Int32.Parse(start[3]);
            //   _x1 = Int32.Parse(start[0]);
            //   _y1 = Int32.Parse(start[1]);
            Send("planet", Utils.Str("position", _x1, _y1));
             Send("planet", Utils.Str("position", _x1, _y1));
        }

        public bool isOccupied(string newPos, List<string> occupied)
        {
            if (occupied.Contains(newPos)) return true;
            else return false;
        }

        public override void Act(Message message)
        {
            Console.WriteLine("\t[{1} -> {0}]: {2}", this.Name, message.Sender, message.Content);

            string action;
            List<string> parameters;
            Utils.ParseMessage(message.Content, out action, out parameters);

            switch (action)
            {
                case "move":
                    if (!(IsAtExit()))
                    {
                        string mess;
                       // string mess1;
                        if (positionExit == null)
                        {
                            MoveRandomly(parameters);

                            mess = Utils.Str("change", _x, _y);
                         //   mess1 = Utils.Str("change1", _x1, _y1);
                            //   mess = Utils.Str("change", _x1, _y1);
                            int weight = 1;
                            // if (deadEnd) weight = deadEndWeight;

                            for (int i = 0; i < Utils.NoExplorers; i++)
                                Send("explorer" + i, mess + " " + weight);
                        }
                        else
                        {
                            if (pathExit == null)
                            {
                                // computeShortest
                                // computeShortestPath();
                            }
                            // go to the exit
                            int index = pathExit.IndexOf(_x + " " + _y);

                            if (!isOccupied((pathExit[index - 1]).Replace(" ", "-"), parameters))
                                move(pathExit[index - 1]);

                            mess = Utils.Str("change", _x, _y);
                          //  mess1 = Utils.Str("change1", _x1, _y1);
                        }
                        Send("planet", mess);
                       // Send("planet", mess1);
                        nrMoves++;
                    }
                    else
                    {
                        if (positionExit == null)
                        {
                            positionExit = _x + " " + _y;
                            for (int i = 0; i < Utils.NoExplorers; i++)
                                if (this.Name != "explorer" + i)
                                {
                                    Send("explorer" + i, Utils.Str("winner", _x, _y));
                                }
                        }
                        Send("planet", Utils.Str("winner", nrMoves));
                        // Stop();
                    }
                    break;
                /*case "move1":
                    if (!(IsAtExit()))
                    {
                       // string mess;
                         string mess1;
                        if (positionExit == null)
                        {
                            MoveRandomly(parameters);

                            mess1 = Utils.Str("change1", _x1, _y1);
                            //   mess1 = Utils.Str("change1", _x1, _y1);
                            //   mess = Utils.Str("change", _x1, _y1);
                            int weight = 1;
                            // if (deadEnd) weight = deadEndWeight;

                            for (int i = 0; i < Utils.NoExplorers; i++)
                                Send("explorer" + i, mess1 + " " + weight);
                        }
                        else
                        {
                            if (pathExit == null)
                            {
                                // computeShortest
                                // computeShortestPath();
                            }
                            // go to the exit
                            int index = pathExit.IndexOf(_x1 + " " + _y1);

                            if (!isOccupied((pathExit[index - 1]).Replace(" ", "-"), parameters))
                                move(pathExit[index - 1]);

                            mess1 = Utils.Str("change1", _x1, _y1);
                            //  mess1 = Utils.Str("change1", _x1, _y1);
                        }
                        Send("planet", mess1);
                        // Send("planet", mess1);
                        nrMoves++;
                    }
                    else
                    {
                        if (positionExit == null)
                        {
                            positionExit = _x1 + " " + _y1;
                            for (int i = 0; i < Utils.NoExplorers; i++)
                                if (this.Name != "explorer" + i)
                                {
                                    Send("explorer" + i, Utils.Str("winner", _x1, _y1));
                                }
                        }
                        Send("planet", Utils.Str("winner", nrMoves));
                        // Stop();
                    }
                    break;*/

                case "change":
                    // message from other explorers
                    visit(string.Join(" ", parameters.Take(2)), Int32.Parse(parameters[2]));
                    break;

             /* case "change1":
                    // message from other explorers
                    visit(string.Join(" ", parameters.Take(2)), Int32.Parse(parameters[2]));
                    break;*/

                case "restart":
                    restart();
                    break;

                default:
                    break;
            }

        }
      


        private void computeShortestPath()
        {
            // find shortest path Dijkistra
            int v = toRemember.Keys.Count;

            Dictionary<string, List<string>> adj =
                       new Dictionary<string, List<string>>(v);

            foreach (string cell in toRemember.Keys)
            {
                adj[cell] = new List<string>();
            }

            foreach (string cell_1 in toRemember.Keys)
            {
                foreach (string cell_2 in toRemember.Keys)
                {
                    string[] cell1 = cell_1.Split();
                    int x1 = Int32.Parse(cell1[0]);
                    int y1 = Int32.Parse(cell1[1]);

                    string[] cell2 = cell_2.Split();
                    int x2 = Int32.Parse(cell2[0]);
                    int y2 = Int32.Parse(cell2[1]);

                    if ((Math.Abs(x1 - x2) == 1 && y1 == y2) || (Math.Abs(y1 - y2) == 1 && x1 == x2))
                        Utils.addEdge(adj, cell_1, cell_2);
                }
            }
            string source = _x + " " + _y;
            string dest = positionExit;
            // Utils.shortestDistance(adj, source, dest, v, out pathExit);
        }

        private void visit(string position, int weight)
        {
            // keep a map of the visited nodes
            if (!(toRemember.Keys.Contains(position)))
            {
                toRemember.Add(position, weight);
            }
            else
            {
                toRemember[position] = toRemember[position] + weight;
            }

        }

        private void move(string position)
        {
            string[] pos = position.Split();
            _x = Int32.Parse(pos[0]);
            _y = Int32.Parse(pos[1]);
        }
        private void MoveRandomly(List<string> occupied)
        {
            int countWallsAroundMe = 0;

            Dictionary<int, string> paths = new Dictionary<int, string>();
            int N1;
            N1 = Int32.Parse(this.Name.Replace("explorer", " "));

            if (N1 <= 3)
            {
                int newX = _x - 1;
                bool floorLeft = _x > 0 && (Utils.maze[newX, _y] == 0);
                if (floorLeft && !isOccupied(newX + "-" + _y, occupied))
                {
                    // go left
                    paths.Add(0, newX + " " + _y);
                }


                if (!floorLeft) countWallsAroundMe++;

            }
            if (N1 > 3 && N1 <=6)
            {
                int newXX = _x + 1;
                bool floorRight = _x < Utils.maze.GetLength(0) - 1 && (Utils.maze[newXX, _y] == 0);
                if (floorRight && !isOccupied(newXX + "-" + _y, occupied))
                {
                    // go right
                    paths.Add(1, newXX + " " + _y);
                }
                if (!floorRight) countWallsAroundMe++;

            }
            if (N1 > 6 && N1 <= 9)
            {
                // bool floorUp = _y > 0 && (Utils.maze[_x, _y] == 0);
                int newY = _y - 1;
                bool floorUp = _y > 0 && (Utils.maze[_x, newY] == 0);
                if (floorUp && !isOccupied(_x + "-" + newY, occupied))
                {
                    // go up
                    paths.Add(2, _x + " " + newY);
                }
                if (!floorUp) countWallsAroundMe++;

            }
            if(N1 > 9 && N1 <= 12)
            {
                int newYY = _y + 1;
                bool floorDown = _y < Utils.maze.GetLength(1) - 1 && (Utils.maze[_x, newYY] == 0);
                if (floorDown && !isOccupied(_x + "-" + newYY, occupied))
                {
                    // go down
                    paths.Add(3, _x + " " + newYY);
                }
                if (!floorDown) countWallsAroundMe++;

            }




            /*if (countWallsAroundMe == 3)
            {
                deadEnd = true;
            }
            if (deadEnd && countWallsAroundMe == 1)
            {
                deadEnd = false;
            }*/

            string finalPosition = _x + " " + _y;
            bool shouldStop = false;

            if (paths.Count > 0)
            {
                int minimum = int.MaxValue;

                foreach (KeyValuePair<int, string> path in paths)
                {
                    int weight;
                    // for every cell in the paths get toRemember weight
                    if (toRemember.Keys.Contains(path.Value))
                    {
                        weight = toRemember[path.Value];
                    }
                    else weight = 0;
                    if (weight < minimum)
                    {
                        minimum = weight;
                        finalPosition = path.Value;
                    }

                    // check if neighboring cell has value 2
                    string[] neighborCoords = path.Value.Split(' ');
                    int neighborX = int.Parse(neighborCoords[0]);
                    int neighborY = int.Parse(neighborCoords[1]);
                    if (Utils.maze[neighborX, neighborY] == 2)
                    {
                        shouldStop = true;
                        break;
                    }
                    for (int i = Math.Max(0, neighborY - 1); i <= Math.Min(Utils.maze.GetLength(1) - 1, neighborY + 1); i++)
                    {
                        if (i != neighborY && new int[] { 5, 7, 9, 10, 13, 14, 15, 18, 19, 20, 21, 22 }.Contains(Utils.maze[neighborX, i]) && TrafficSynForm.C || i != neighborY && new int[] { 6, 8, 11, 12, 16, 17 }.Contains(Utils.maze[neighborX, i]) && TrafficSynForm.C1)
                        {
                            // stop moving if neighboring cell in the same row has value 2
                            // or if a cell in any row above or below has value 2
                            return;
                        }
                    }
                }

                if (shouldStop)
                {
                    // stop moving if neighbor cell has value 2
                    return;
                }

                int toAddWeight = 1;
                /*if (deadEnd)
                {
                    toAddWeigth = deadEndWeight;
                }*/
                // visit(finalPosition, toAddWeigth);

                visit(finalPosition, toAddWeight);

                move(finalPosition);
            }
        }
        /*       private void MoveRandomly(List<string> occupied)
               {
                   int countWallsAroundMe = 0;

                   Dictionary<int, string> paths = new Dictionary<int, string>();


                   int N1;

                   N1 = Int32.Parse(this.Name.Replace("explorer", " "));
                   if (N1 <= 6)
                   {    int newX = _x - 1;
                   bool floorLeft = _x > 0 && (Utils.maze[newX, _y] == 0);
                   if (floorLeft && !isOccupied(newX + "-" + _y, occupied))
                   {
                       // go left
                       paths.Add(0, newX + " " + _y);
                   }


                      if (!floorLeft) countWallsAroundMe++;

                      }

                   int newXX = _x + 1;
                   bool floorRight = _x < Utils.maze.GetLength(0) - 1 && (Utils.maze[newXX, _y] == 0);
                   if (floorRight && !isOccupied(newXX + "-" + _y, occupied))
                   {
                       // go right
                       paths.Add(1, newXX + " " + _y);
                   }
                  if (!floorRight) countWallsAroundMe++;


                  // N1 = Int32.Parse(this.Name.Replace("explorer", " "));
                   if (N1 <= 6)
                   {
                      // bool floorUp = _y > 0 && (Utils.maze[_x, _y] == 0);
                          int newY = _y - 1;
                          bool floorUp = _y > 0 && (Utils.maze[_x, newY] == 0);
                          if (floorUp && !isOccupied(_x + "-" + newY, occupied))
                          {
                              // go up
                              paths.Add(2, _x + " " + newY);
                          }
                       if (!floorUp) countWallsAroundMe++;
                   }

                   int newYY = _y + 1;
                   bool floorDown = _y < Utils.maze.GetLength(1) - 1 && (Utils.maze[_x, newYY] == 0);
                   if (floorDown && !isOccupied(_x + "-" + newYY, occupied))
                   {
                       // go down
                       paths.Add(3, _x + " " + newYY);
                   }
                   if (!floorDown) countWallsAroundMe++;

                   /* if (countWallsAroundMe == 3)
                   {
                       deadEnd = true;
                   }
                   if (deadEnd && countWallsAroundMe == 1)
                   {
                       deadEnd = false;
                   } */
        /*

                   string finalPosition = _x + " " + _y;
                   bool shouldStop = false;

                   if (paths.Count > 0)
                   {
                       int minimum = int.MaxValue;

                       foreach (KeyValuePair<int, string> path in paths)
                       {
                           int weight;
                           // for every cell in the paths get toRemember weight
                           if (toRemember.Keys.Contains(path.Value))
                           {
                               weight = toRemember[path.Value];
                           }
                           else weight = 0;
                           if (weight < minimum)
                           {
                               minimum = weight;
                               finalPosition = path.Value;
                           }

                           // check if neighboring cell has value 2
                           string[] neighborCoords = path.Value.Split(' ');
                           int neighborX = int.Parse(neighborCoords[0]);
                           int neighborY = int.Parse(neighborCoords[1]);
                           if (Utils.maze[neighborX, neighborY] == 2)
                           {
                               shouldStop = true;
                               break;
                           }
                           for (int i = Math.Max(0, neighborY - 1); i <= Math.Min(Utils.maze.GetLength(1) - 1, neighborY + 1); i++)
                           {
                               if (i != neighborY && new int[] { 2, 8 }.Contains(Utils.maze[neighborX, i]) && TrafficSynForm.C || i != neighborY && new int[] { 5, 9 }.Contains(Utils.maze[neighborX, i]) && TrafficSynForm.C1)
                               {
                                   // stop moving if neighboring cell in the same row has value 2
                                   // or if a cell in any row above or below has value 2
                                   return;
                               }
                           }
                       }

                       if (shouldStop)
                       {
                           // stop moving if neighbor cell has value 2
                           return;
                       }

                       int toAddWeight = 1;
                       /* if (deadEnd)
                       {
                           toAddWeigth = deadEndWeight;
                       } */
        // visit(finalPosition, toAddWeigth);
        /*
                        visit(finalPosition, toAddWeight);

                        move(finalPosition);
                    }
                }*/


        private bool IsAtExit()
        {
            return (_x == x_end && _y == y_end); // the position of the base
        }
    }
}



