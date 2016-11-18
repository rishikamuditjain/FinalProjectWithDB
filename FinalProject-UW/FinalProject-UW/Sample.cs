using System;

namespace FinalProject
{
    public class Sample
    {
        public int origRow;
        public int origCol;

        public Sample()
        {
            origRow = 0;
            origCol = 0;
        }

        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}