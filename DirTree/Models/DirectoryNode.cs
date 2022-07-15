using System;
using System.Collections.Generic;
using System.IO;

namespace DirTree.Models
{
    public class DirectoryNode : IComparable<DirectoryNode>
    {
        public string Name => info.Name;
        public long Size
        {
            get
            {
                if (valid) return size;
                else return CalculateSize();
            }
        }
        public string Path => info.FullName;

        public DirectoryNode(DirectoryInfo info)
        {
            children = new List<DirectoryNode>();
            this.info = info;
            valid = false;
        }

        public DirectoryNode AddChild(DirectoryInfo childInfo) =>
            AddChild(new DirectoryNode(childInfo));

        private DirectoryNode AddChild(DirectoryNode child)
        {
            children.Add(child);
            valid = false;
            return child;
        }

        public IEnumerable<DirectoryNode> Children => children;

        public void SortChildren()
        {
            children.Sort((x, y) => y.CompareTo(x));
        }

        public long CalculateSize()
        {
            long runningSize = 0;

            runningSize += info.GetSizeOfFiles();
            foreach (var child in children)
            {
                runningSize += child.Size;
            }

            size = runningSize;
            valid = true;

            return size;
        }

        public int CompareTo(DirectoryNode? other)
        {
            if (this == other) return 0;

            if (other == null) return -1;

            return this.Size.CompareTo(other.Size);
        }

        private readonly List<DirectoryNode> children;
        private readonly DirectoryInfo info;
        private bool valid;
        private long size;
    }
}
