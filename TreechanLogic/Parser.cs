using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreechanLogic
{
    internal class Parser
    {
        public List<Post> Parse(List<RawPost> posts, int OPpost)
        {
            List<Post> roots = new List<Post>();
            foreach (RawPost post in posts)
            {
                //if (Enumerable.SequenceEqual(post.Parents, new int[] { })) // if post without parents then add it to tree list
                if (!post.Parents.Any() || post.Parents.Contains(OPpost))
                {
                    Post root = new Post(post);
                    if (post.Id != OPpost)
                    {
                        root = MakeTree(root, posts);// attach child posts
                    }
                     
                    roots.Add(root);
                }
            }
            return roots;
        }
        public Post MakeTree(Post root, List<RawPost> posts)
        {
            List<RawPost> childs = posts.FindAll(x => x.Parents.Contains(root.data.Id)); // get child posts
            root.AddChilds(childs); // attach childs
            foreach (Post post in root.children)
            {
                MakeTree(post, posts); // make it for childs
            }
            return root;
        }
        public void DrawTree(Post tree, int depth)
        {
            string symbol = "";
            if (depth != 0)
            {
                symbol = "L";
            }
            Console.WriteLine(new String('|', Math.Max(0, depth-1)) + symbol + tree.data.Header);
            Console.WriteLine(new String('|', depth) + "" + tree.data.Text);
            Console.WriteLine(new String('|', depth));
            foreach (Post child in tree.children)
            {
                DrawTree(child, depth + 1);
            }
        }
    }
}
