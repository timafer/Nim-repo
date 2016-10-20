using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void Chosemove1Test()
        {
            //arrange
            int[] visual2 = new int[3]
      {
            0,4,4
      };
            Nim.CpuVsCpu.LearnCPU l = new Nim.CpuVsCpu.LearnCPU(visual2,"Player 2");
            Nim.Menu m = new Nim.Menu();
            Nim.CpuVsCpu.StateStore s=m.DeSeralizetest();
            l.learnedMoves = s.learnedMoves;
            //act
            int[] i=l.ChooseMove();
            //assert
            int[] t={2,3};
            CollectionAssert.AreEqual(t,i,"not corect atual is "+i[0]+","+i[1]);
        }
    }
}
