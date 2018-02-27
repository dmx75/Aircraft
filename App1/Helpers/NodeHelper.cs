using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1.Helpers
{

    public class Node
    {
        public IDictionary<string, Node> Nodes { get; set; }

        public Node()
        {
            Nodes = new Dictionary<string, Node>();
        }

        public string Path { get; set; }

        public void AddPath(string path)
        {
            char[] charSeparators = new char[] { '/' };

            // Parse into a sequence of parts.
            string[] parts = path.Split(charSeparators,
                StringSplitOptions.RemoveEmptyEntries);

            // The current node.  Start with this.
            Node current = this;

            // Iterate through the parts.
            foreach (string part in parts)
            {
                // The child node.
                Node child;

                // Does the part exist in the current node?  If
                // not, then add.
                if (!current.Nodes.TryGetValue(part, out child))
                {
                    // Add the child.
                    child = new Node
                    {
                        Path = part
                    };

                    // Add to the dictionary.
                    current.Nodes[part] = child;
                }

                // Set the current to the child.
                current = child;
            }
        }
    }
}