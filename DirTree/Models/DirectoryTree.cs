using System;
using System.IO;
using System.Threading.Tasks;

namespace DirTree.Models
{
    public static class DirectoryTree
    {
        public static DirectoryNode BuildTree(string path)
        {
            var rootDirectory = new DirectoryInfo(path);

            DirectoryNode root = new(rootDirectory);

            void BuildTree(DirectoryNode node, DirectoryInfo directory)
            {
                var e = directory.GetDirectories().GetEnumerator();
                while (e.MoveNext())
                {
                    try
                    {
                        if (e.Current is DirectoryInfo childDirectory)
                        {
                            var childNode = node.AddChild(childDirectory);
                            BuildTree(childNode, childDirectory);
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {

                    }
                }
                node.SortChildren();
            }

            BuildTree(root, rootDirectory);

            return root;
        }

        public static async Task<DirectoryNode> BuildTreeAsync(string path)
        {
            return await Task.Run(() => BuildTree(path));
        }
    }
}
