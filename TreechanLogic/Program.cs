namespace TreechanLogic
{
    class Post
    {
        public RawPost data;
        public LinkedList<Post> children;
        public int childrenCount = 0;
        public Post(RawPost data)
        {
            this.data = data;
            children = new LinkedList<Post>();
        }
        public void AddChild(RawPost data)
        {
            children.AddFirst(new Post(data));
            childrenCount++;
        }
        public void AddChilds(List<RawPost> childs)
        {
            foreach(RawPost child in childs)
            {
                children.AddLast(new Post(child));
                childrenCount++;
            }
        }
        public Post GetChild(int i)
        {
            foreach (Post n in children)
            {
                if (--i == 0) return n;
            }
            return null;
        }
    }
    class RawPost
    {
        public uint[] parents;
        public uint id;
        public uint[] childs;
        private string text;
        private uint date;
        public RawPost(uint[] parents, uint id, uint[] childs, string text, uint date)
        {
            this.parents = parents;
            this.id = id;
            this.childs = childs;
            this.text = text;
            this.date = date;
        }
        public string Text
        {
            get
            {
                return text;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<RawPost> posts = new List<RawPost>();
            posts.Add(new RawPost(new uint[] { 0 },     1, new uint[] {2, 3}, "Пост 1", 22));
            posts.Add(new RawPost(new uint[] { 1 },     2, new uint[] { 4 }, "Пост 2", 23));
            posts.Add(new RawPost(new uint[] { 1 },     3, new uint[] { 4 }, "Пост 3", 24));
            posts.Add(new RawPost(new uint[] { 2, 3 },  4, new uint[] { }, "Пост 4", 25));
            posts.Add(new RawPost(new uint[] { 0 },     5, new uint[] { }, "Пост 5", 26));
            List<Post> trees = Parser(posts);
            foreach (Post tree in trees)
            {
                DrawTree(tree, 0);
            }

        }
        static List<Post> Parser(List <RawPost> posts)
        {
            List <Post> roots = new List<Post>();
            foreach(RawPost post in posts)
            {
                if (Enumerable.SequenceEqual(post.parents, new uint[] {0}))
                {
                    Post root = new Post(post);
                    root = MakeTree(root, posts);
                    roots.Add(root);
                }
            }
            return roots;
        }
        static Post MakeTree(Post root, List<RawPost> posts)
        {
            List<RawPost> childs = posts.FindAll(x => x.parents.Contains(root.data.id));
            root.AddChilds(childs);
            foreach(Post post in root.children)
            {
                MakeTree(post, posts);
            }
            return root;
        }
        static void DrawTree(Post tree, int depth)
        {
            string symbol = "";
            if (depth != 0)
            {
                symbol = "L_";
            }
            Console.WriteLine(new String(' ', depth) + symbol + tree.data.Text);
            foreach (Post child in tree.children)
            {
                DrawTree(child, depth + 1);
            }
        }
    }
}