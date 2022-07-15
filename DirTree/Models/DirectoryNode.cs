using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirTree.Models
{
    public class DirectoryNode
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

        public long CalculateSize()
        {
            long runningSize = 0;

            runningSize += info.GetSizeOfFiles();
            foreach(var child in children)
            {
                runningSize += child.Size;
            }

            size = runningSize;
            valid = true;

            return size;
        }

        private readonly List<DirectoryNode> children;
        private readonly DirectoryInfo info;
        private bool valid;
        private long size;
    }
}
