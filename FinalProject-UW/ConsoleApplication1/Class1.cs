using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc
{
    class Stack
    {
        private int[] stack = new int[10];
        private int sp = 0;

        public void Push(int v)
        {
            stack[sp++] = v;
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

        public int Pop()
        {
            return stack[--sp];
        }
    }

    class RPN
    {
        private Stack stack = new Stack();

        public void Process(string str)
        {
            switch (str)
            {
                case "+":
                    {
                        int v1 = stack.Pop();
                        int v2 = stack.Pop();
                        int sum = v1 + v2;
                        stack.Push(sum);
                        break;
                    }
                case "-":
                    {
                        int v1 = stack.Pop();
                        int v2 = stack.Pop();
                        int diff = v1 - v2;
                        stack.Push(diff);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public int Result
        {
            get
            {
                return stack.Pop();
            }
        }
    }

    class ex17
    {
        public static void Main()
        {
            RPN rpn = new RPN();

            while (true)
            {
                string str;

                Console.Write("RPN>");
                str = Console.ReadLine();

                rpn.Process(str);

                Console.WriteLine("={0}", rpn.Result);
                Console.ReadLine();
            }
        }
    }
}
