using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Stack
    {
        private int[] stack = new int[10];
        private int sp = 0;

        public void Push(int v)
        {
            //hii
            stack[sp++] = v;
            if (sp >= stack.Length)
            {
                int newLength = stack.Length * 2;
                int[] newStack = new int[newLength];
                for(int i = 0; i < newLength; i++)
                {
                    newStack[i] = stack[0];
                }
                stack = newStack;
            }
        }

        public int Pop()
        {
            return stack[--sp];
        }

        public int Top
        {
            get
            {
                return stack[sp - 1];
            }
        }

        public bool IsEmpty
        {
            get
            {
                return sp == 0;
            }
        }
    }

    class Arrays_Stacks
    {
        static void Main()
        {
            Stack stack = new Stack();

            stack.Push(5);
            stack.Push(3);
            stack.Push(2);
            stack.Push(9);

            Console.WriteLine("The last value pushed was {0}", stack.Top);

            while (!stack.IsEmpty)
            {
                Console.WriteLine("Popping {0}", stack.Pop());
            }

            Console.Write("Press Enter...");
            Console.ReadLine();
        }
    }
}
