﻿namespace PterodactylMigrator.App.Database.Entities.Moonlight;

public class NewsEntry
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public string Markdown { get; set; }
}