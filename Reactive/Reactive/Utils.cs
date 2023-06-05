using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.Remoting.Channels;

using System.Linq;

namespace Reactive
{
    public class Utils
    {
        public static int NoExplorers = 20;

        private static Random rnd = new Random();
        public static Brush PickBrush()
        {
            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();
            int random = rnd.Next(20, 35);
            Brush result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

        public static int Delay = 20;
        public static Random RandNoGen = new Random();


        public static int [,] maze =
            //       0  1  2  3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26
       /*0*/      {{ 1, 1, 1, 1, 1, 1 ,0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },
           
       /*1*/       { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*2*/       { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 5, 0, 1, 0, 6, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*3*/       { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 },

       /*4*/       { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },

       /*5*/       { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

       /*6*/       { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 7, 0, 1,0 , 8, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*7*/       { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*8*/       { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*9*/       { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 9, 0, 1, 0,10, 1, 1, 1,11, 0, 1, 1, 1, 1, 1, 1 },

       /*10*/      { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

       /*11*/      { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },

       /*12*/      { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },

       /*13*/      { 1, 1, 1, 1, 1, 1, 0,12, 1, 1, 1,13, 0, 1, 0,14, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*14*/      { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*15*/      { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*16*/      { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*17*/      { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*18*/      { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*19*/      { 1, 1, 1, 1, 1, 15, 0, 1, 1, 1, 1, 16, 0, 1, 0, 17, 1, 1, 1, 18, 0, 1, 1, 1, 1, 1, 1 },

       /*20*/      { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

       /*21*/      { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },

       /*22*/      { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},

       /*23*/      { 1, 1, 1, 1, 1, 1, 0, 19, 1, 1, 1, 20, 0, 1, 0, 21, 1, 1, 1, 1, 0, 22, 1, 1, 1, 1, 1 },

       /*24*/      { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0 , 1, 1, 1, 1, 1, 1 },

       /*25*/      { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },

       /*26*/      { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },




       /*27*/      { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1 },
                    };

 

        public static string start = "27 12 0 14 10 26 12 0";
       // public static string start1 = "0 14 ";
        public static string end = "26 10";
        public static int Height = 26;
        public static int Width = 4;


        public static void ParseMessage(string content, out string action, out List<string> parameters)
        {
            string[] t = content.Split();

            action = t[0];

            parameters = new List<string>();
            for (int i = 1; i < t.Length; i++)
                parameters.Add(t[i]);
        }

        public static void ParseMessage(string content, out string action, out string parameters)
        {
            string[] t = content.Split();

            action = t[0];

            parameters = "";

            if (t.Length > 1)
            {
                for (int i = 1; i < t.Length - 1; i++)
                    parameters += t[i] + " ";
                parameters += t[t.Length - 1];
            }
        }

        public static string Str(object p1, object p2)
        {
            return string.Format("{0} {1}", p1, p2);
        }

        public static string Str(object p1, object p2, object p3)
        {
            return string.Format("{0} {1} {2}", p1, p2, p3);
        }

        public static void addEdge(Dictionary<string, List<string>> adj,
                                    string i, string j)
        {
            adj[i].Add(j);
            adj[j].Add(i);
        }

     

    }
}

