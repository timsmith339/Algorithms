using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scratchpad
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BTree tree = new BTree();
            Random rnd = new Random();
            for (int x = 1; x <= 100; x++)
            {
                tree.Add(rnd.Next(0, 15));
            }
            
            var found = tree.FindMinValue();
            var count = found.GetRowCountFromRoot();

            while (found != null)
            {
                Console.WriteLine(found.Value);
                found = found.GetNextLargest();
            }

            Console.ReadKey();
        }
    }

    public class BTree
    {
        public BTreeNode Head { get; set; }

        public BTreeNode Find(int value)
        {
            return Head.Find(Head, value);
        }

        public BTreeNode Add(int value)
        {
            if (Head == null)
            {
                return Head = new BTreeNode() {Value = value};
            }
            return Head.Add(Head, value);
        }

        public BTreeNode FindMinValue()
        {
            return Head.GetMinValue();
        }

        public void MapTree()
        {
            Head.MapTree();
        }
    }

    public class BTreeNode
    {
        public int Value { get; set; }
        public int MapRow { get; set; }
        public int MapCol { get; set; }

        public BTreeNode Parent { get; set; }
        public BTreeNode Left { get; set; }
        public BTreeNode Right { get; set; }

        public BTreeNode Find(BTreeNode current, int value)
        {
            if (current == null) return null;
            if (current.Value == value) return current;
            if (value < current.Value) return Find(current.Left, value);
            return Find(current.Right, value);
        }
        public BTreeNode Add(BTreeNode current, int value)
        {
            if (current.Value == value)
            {
                return current;
            }
            if (value < current.Value)
            {
                if (current.Left == null)
                {
                    current.Left = new BTreeNode() {Value = value};
                    current.Left.Parent = current;
                    return current.Left;
                }
                else
                {
                    Add(current.Left, value);
                }
            }
            if (value > current.Value)
            {
                if (current.Right == null)
                {
                    current.Right = new BTreeNode() { Value = value };
                    current.Right.Parent = current;
                    return current.Right;
                }
                else
                {
                    Add(current.Right, value);
                }
            }
            return null;
        }
        public BTreeNode GetdRoot()
        {
            BTreeNode current = this;
            while (current.Parent != null)
            {
                current = current.Parent;
            }
            return current;
        }
        public BTreeNode GetMinValue()
        {
            BTreeNode current = this;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        public BTreeNode GetNextLargest()
        {
            // If right != null, move to right
            if (Right != null)
            {
                return Right.GetMinValue();
            }

            var current = this.Parent;

            while (current.Value < Value)
            {
                current = current.Parent;
            }

            return current;            
        }
        public void MapTree()
        {
            MapRow = 1;
            MapCol = 1;

            BTreeNode current = this;

            current.MapCol = MapCol;
            current.MapRow = MapRow;

            while (current.Left != null)
            {
                current = current.Left;
                MapRow++;
                current.MapRow = MapRow;

                if (current.Left == null && current.Right !=  null)
                {
                    current = current.Right;
                    MapRow++;
                    MapCol++;
                    current.MapCol = MapCol;
                    current.MapRow = MapRow;
                }
                else if (current.Left == null && current.Right == null)
                {
                    current = current.Parent;
                }
            }

            
        }

        public void Delete()
        {
            // Case 1 - Only one child
            if (Left != null && Right == null)
            {
                if (Parent != null)
                {
                    if (Parent.Value < Value)
                    {
                        Parent.Right = Left;
                    }
                    if (Parent.Value > Value)
                    {
                        Parent.Left = Left;
                    }
                    Left.Parent = Parent;
                }
            }

            if (Right != null && Left == null)
            {
                if (Parent != null)
                {
                    if (Parent.Value < Value)
                    {
                        Parent.Right = Right;
                    }
                    if (Parent.Value > Value)
                    {
                        Parent.Left = Right;
                    }
                    Right.Parent = Parent;
                }
            }
        }
    }

}
