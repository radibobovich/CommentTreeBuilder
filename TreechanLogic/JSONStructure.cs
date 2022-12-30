using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreechanLogic
{
    // RootJ myDeserializedClass = JsonConvert.DeserializeObject<RootJ>(myJsonResponse);
    public class BoardJ
    {
#nullable disable warnings
        public int bump_limit { get; set; }
        public string category { get; set; }
        public string default_name { get; set; }
        public bool enable_dices { get; set; }
        public bool enable_flags { get; set; }
        public bool enable_icons { get; set; }
        public bool enable_likes { get; set; }
        public bool enable_names { get; set; }
        public bool enable_oekaki { get; set; }
        public bool enable_posting { get; set; }
        public bool enable_sage { get; set; }
        public bool enable_shield { get; set; }
        public bool enable_subject { get; set; }
        public bool enable_thread_tags { get; set; }
        public bool enable_trips { get; set; }
        public List<string> file_types { get; set; }
        public string id { get; set; }
        public string info { get; set; }
        public string info_outer { get; set; }
        public int max_comment { get; set; }
        public int max_files_size { get; set; }
        public int max_pages { get; set; }
        public string name { get; set; }
        public int threads_per_page { get; set; }
    }

    public class FileJ
    {
        public string displayname { get; set; }
        public string fullname { get; set; }
        public int height { get; set; }
        public string md5 { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public int size { get; set; }
        public string thumbnail { get; set; }
        public int tn_height { get; set; }
        public int tn_width { get; set; }
        public int type { get; set; }
        public int width { get; set; }
    }

    public class PostJ
    {
        public int banned { get; set; }
        public string board { get; set; }
        public int closed { get; set; }
        public string comment { get; set; }
        public string date { get; set; }
        public string email { get; set; }
        public int endless { get; set; }
        public List<FileJ> files { get; set; }
        public int lasthit { get; set; }
        public string name { get; set; }
        public int num { get; set; }
        public int number { get; set; }
        public int op { get; set; }
        public int parent { get; set; }
        public int sticky { get; set; }
        public string subject { get; set; }
        public string tags { get; set; }
        public int timestamp { get; set; }
        public string trip { get; set; }
        public int views { get; set; }
    }

    public class RootJ
    {
        public string advert_mobile_image { get; set; }
        public string advert_mobile_link { get; set; }
        public BoardJ board { get; set; }
        public string board_banner_image { get; set; }
        public string board_banner_link { get; set; }
        public int current_thread { get; set; }
        public int files_count { get; set; }
        public bool is_board { get; set; }
        public int is_closed { get; set; }
        public bool is_index { get; set; }
        public int max_num { get; set; }
        public int posts_count { get; set; }
        public string thread_first_image { get; set; }
        public List<ThreadJ> threads { get; set; }
        public string title { get; set; }
        public int unique_posters { get; set; }
    }

    public class ThreadJ
    {
        public List<PostJ> posts { get; set; }
    }


}
