using System;
using System.Collections.Generic;
using System.Text;

namespace _03_HTTP_HW
{
    public class Post
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
    }
    public class Comment
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
    }
    public class Album
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
    }

    public class Photo
    {
        public int albumId { get; set; }
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
        public string thumbnailUrl { get; set; } = string.Empty;
    }
    public class Todo
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public bool completed { get; set; }
    }
    public class User
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}
