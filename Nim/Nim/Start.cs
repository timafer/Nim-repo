﻿using Nim.CpuVsCpu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nim
{
    public class Start
    {
        public static void Main(string[] args)
        {
            Game game = new Game();
            Menu n = new Menu(game);
            n.Select();
        }
    }
}
