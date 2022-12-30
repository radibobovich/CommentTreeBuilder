using Newtonsoft.Json;
using System.Net;
//using System.Text.Json;
using System.Net.Http.Json;
using HtmlAgilityPack;
namespace TreechanLogic
{
    // represents tree node
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
    }
    class RawPost
        // represents post information
    {
        private List<int> parents;
        private int id;
        private int[] childs;
        private string header;
        private string text;
        private int number;
        private PostJ json;
        public RawPost(List<int> parents, int id, int[] childs, string header, string text, int number, PostJ json)
        {
            this.parents = parents;
            this.id = id;
            this.childs = childs;
            this.header = header;
            this.text = text;
            this.number = number;
            this.json = json;
        }
        public List<int> Parents { get { return parents; } }
        public int Id { get { return id; } }
        public string Header { get { return header; } }
        public string Text { get { return text; } }
        public int Number { get { return number; } }
        public PostJ Json { get { return json; } }
    }
    class Program
    {
        static void Main(string[] args)
        {

            // real thread
            
            List<RawPost> posts = new List<RawPost>();

            string url = "https://2ch.hk/b/res/280020200.json";
            int OPpost = 0;
            TestRealThread(ref posts, url, out OPpost);

            Parser parser = new Parser();
            List<Post> trees = parser.Parse(posts, OPpost);
            foreach (Post tree in trees)
            {
                parser.DrawTree(tree, 0);
            }
            Console.WriteLine("Done.");
        }
        static RootJ GetResponse(string url)
        {
            var myJsonResponse = new WebClient().DownloadString(url);

            Console.WriteLine("JSON size: " + System.Text.Encoding.Unicode.GetByteCount(myJsonResponse)/1024 + "KB");
            RootJ threadPage = JsonConvert.DeserializeObject<RootJ>(myJsonResponse);

            return threadPage;
            //RootJ myDeserializedClass = JsonConvert.DeserializeObject<RootJ>(myJsonResponse);
        }
        static void TestRealThread(ref List<RawPost> posts, string url, out int OPpost)
        {
            RootJ threadPage = GetResponse(url);
            OPpost = threadPage.current_thread;
            foreach (PostJ post in threadPage.threads.First().posts)
            {
                // convert JSON post to RawPost
                
                string comment = post.comment;

                var htmlFragment = new HtmlDocument();
                htmlFragment.LoadHtml(comment);

                string text = "";
                var br_tags = htmlFragment.DocumentNode.SelectNodes("//br");
                if (br_tags != null)
                {
                    var br_tags_list = br_tags.ToList();
                    foreach (HtmlNode br_tag in br_tags_list)
                    {
                        var val = br_tag.SelectSingleNode("following-sibling::text()[1]"); // get text after <br> tag
                        if (val != null)
                        {
                            text += val.InnerText;
                        }
                        else
                        {
                            text = htmlFragment.DocumentNode.InnerText;
                        }
                       // text += (br_tag.OuterHtml);
                    }
                }
                else
                {
                    text = htmlFragment.DocumentNode.InnerText;
                }

                // get post parents
                var a_tags = htmlFragment.DocumentNode.SelectNodes("//a");
                var parents = new List<int>();
                if (a_tags != null)
                {
                    var a_tags_list = a_tags.ToList();

                    foreach (HtmlNode a_tag in a_tags_list)
                    {
                        if (a_tag.Attributes["data-num"] != null)
                        {
                            string parent = a_tag.Attributes["data-num"].Value; // get parent post id
                            parents.Add(Convert.ToInt32(parent));
                        }
                    }
                }
                // shorten if text is longer than 100
                string ellipsis = text.Length > 100 ? "..." : ""; 
                string textPreview =   
                    text.Substring(0, Math.Min(text.Length, 100)) + ellipsis;

                string header = "Аноним " + post.date + " №" + post.num;
                RawPost rawPost = new RawPost(
                    parents:parents,
                    id:post.num,
                    childs:new int[] { },
                    header:header,
                    text:textPreview,
                    number:post.number,
                    json:post
                    );

                posts.Add(rawPost);
            }
        }
    }
}