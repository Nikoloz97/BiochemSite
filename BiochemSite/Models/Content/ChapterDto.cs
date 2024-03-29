﻿using System.ComponentModel.DataAnnotations;

namespace BiochemSite.Models.Content
{
    public class ChapterDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<SubchapterDto> Subchapters { get; set; } = new List<SubchapterDto>();
    }
}
