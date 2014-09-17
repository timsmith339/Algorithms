using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
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
            
            tree.MapTree();

            Console.ReadKey();
        }
    }

    public class BTree
    {
        public BTreeNode Head { get; set; }
        private List<BTreeNode> _nodeList;
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
            var mapper = new BTreeNodeMapper(Head);
            _nodeList = mapper.BeginMap();

            var rowCount = _nodeList.OrderByDescending(n => n.Map.MapCol).FirstOrDefault().Map.MapCol;
            var center = Convert.ToInt32(Math.Pow(2, (rowCount - 1)));

            var numList = new List<int>();

            for (int row = 1; row <= rowCount; row++)
            {
                var nodesInRow = _nodeList.Where(n => n.Map.MapRow == row).ToList();
            
                foreach (var node in nodesInRow)
                {
                    var col = node.Map.MapCol;

                    var position = Convert.ToInt32(((center / Math.Pow(2, (row-1))) + ((center / (Math.Pow(2, row-2))) * (col-1))));
                    numList.Add(position);
                }

            }

            numList.OrderBy(n => n);

          
            numList.ForEach(n => Console.WriteLine(n));
        }

        
    }

    public class BTreeNode
    {
        public BTreeNode()
        {
            Map = new BTreeNodeMap();
        }

        public int Value { get; set; }
        public BTreeNodeMap Map { get; set; }

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

    public class BTreeNodeMapper
    {
        private BTreeNode _currentNode { get; set; }
        private int _currentRow;
        private int _currentCol;
        private int _index = 0;
        
        public BTreeNodeMapper(BTreeNode node)
        {
            _currentNode = node;
            _currentCol = 1;
            _currentRow = 1;
        }

        public bool IsLeftOpen()
        {
            if (_currentNode.Left != null
                && _currentNode.Left.Map.MapCol == -1)
            {
                return true;
            }

            return false;
        }
        public bool IsRightOpen()
        {
            if (_currentNode.Right != null
                && _currentNode.Right.Map.MapCol == -1)
            {
                return true;
            }

            return false;
        }
        public bool HasParent()
        {
            return _currentNode.Parent != null;
        }

        public List<BTreeNode> BeginMap()
        {
            var returnList = new List<BTreeNode>();

            while (IsLeftOpen() || IsRightOpen() || HasParent())
            {
                if (IsLeftOpen())
                {
                    MoveLeft();
                    continue;
                }

                if (IsRightOpen())
                {
                    MoveRight();
                    continue;
                }

                returnList.Add(Map());

                if (HasParent())
                {
                    MoveUp();
                }
            }

            returnList.Add(Map());

            return returnList;
        }
        public void MoveLeft()
        {
            _currentRow++;
            _currentNode = _currentNode.Left;
        }
        public void MoveRight()
        {
            _currentRow++;
            _currentCol++;
            _currentNode = _currentNode.Right;
        }
        public void MoveUp()
        {
            _currentRow--;
            if (_currentNode.Parent.Right != null)
            {
                if (_currentNode.Parent.Right.Value == _currentNode.Value)
                {
                    _currentCol--;
                }
            }
            _currentNode = _currentNode.Parent;
        }

        public BTreeNode Map()
        {
            _currentNode.Map.MapCol = _currentCol;
            _currentNode.Map.MapRow = _currentRow;
            _currentNode.Map.Index = _index++;
            return _currentNode;
        }
    }

    public class BTreeNodeMap
    {
        public int Index { get; set; }
        public int MapRow { get; set; }
        public int MapCol { get; set; }

        public BTreeNodeMap()
        {
            Index = -1;
            MapRow = -1;
            MapCol = -1;
        }
    }
}
