using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    internal class Quote
    {
        public string Text { get; set; }
        public string Author { get; set; }

        public Quote(string text, string author)
        {
            Text = text;
            Author = author;
        }

        public override string ToString()
        {
            return $"\"{Text}\" — {Author}";
        }
    }
}
