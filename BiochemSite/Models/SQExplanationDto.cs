﻿namespace BiochemSite.Models
{
    // My attempt at making a child resource (couldn't really think of anything better...)
    public class SQExplanationDto
    {
        public string Explanation { get; set; } = string.Empty;
        public string? ImageURL { get; set; }
    }
}
