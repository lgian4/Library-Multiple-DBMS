using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_App
{
    /// <summary>Represents a tree node</summary>
    /// <typeparam name="T">the type of the values in nodes
    /// </typeparam>
    public class TreeNode<T, U>
    {
        // Contains the value of the node
        private T value;
        private U text;
        // Shows whether the current node has a parent or not
        private bool hasParent;
        // Contains the children of the node (zero or more)
        private List<TreeNode<T, U>> children;
        /// <summary>Constructs a tree node</summary>
        /// <param name="value">the value of the node</param>
        public TreeNode(T value, U text)
        {
            if (value == null || text == null)
            {
                throw new ArgumentNullException(
                "Cannot insert null value!");
            }
            this.value = value;
            this.text = text;
            this.children = new List<TreeNode<T, U>>();
        }
        /// <summary>The value of the node</summary>
        public T Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        /// <summary>
        /// the text explaination of the value
        /// </summary>
        public U Text
        {
            get { return text; }
            set { text = value; }
        }

        /// <summary>The number of node's children</summary>
        public int ChildrenCount
        {
            get
            {
                return this.children.Count;
            }
        }
        /// <summary>Adds child to the node</summary>
        /// <param name="child">the child to be added</param>
        public void AddChild(TreeNode<T, U> child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(
                "Cannot insert null value!");
            }
            if (child.hasParent)
            {
                throw new ArgumentException(
                "The node already has a parent!");
            }
            child.hasParent = true;
            this.children.Add(child);
        }
        /// <summary>
        /// Gets the child of the node at given index
        /// </summary>
        /// <param name="index">the index of the desired child</param>
        /// <returns>the child on the given position</returns>
        public TreeNode<T, U> GetChild(int index)
        {
            return this.children[index];
        }
    }
    /// <summary>Represents a tree data structure</summary>
    /// <typeparam name="T">the type of the values in the
    /// tree</typeparam>
    public class Tree<T, U>
    {
        // The root of the tree
        private TreeNode<T, U> root;
        /// <summary>Constructs the tree</summary>
        /// <param name="value">the value of the node</param>
        public Tree(T value, U text)
        {
            if (value == null || text == null)
            {
                throw new ArgumentNullException(
                "Cannot insert null value!");
            }
            this.root = new TreeNode<T, U>(value, text);
        }
        /// <summary>Constructs the tree</summary>
        /// <param name="value">the value of the root node</param>
        /// <param name="children">the children of the root
        /// node</param>
        public Tree(T value, U text, params Tree<T, U>[] children)
        : this(value, text)
        {
            foreach (Tree<T, U> child in children)
            {
                this.root.AddChild(child.root);
            }
        }
        /// <summary>
        /// The root node or null if the tree is empty
        /// </summary>
        public TreeNode<T, U> Root
        {
            get
            {
                return this.root;
            }
        }

    }
}
