﻿using System;
using System.Collections.Generic;


namespace NewsApp.Models
{
    public class News
    {
        
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? URL { get; set; }
        public string? UrlToImage { get; set; }
        public string? Content { get; set; }
        public DateTime PublishedAt { get; set; }

    }
    
}
