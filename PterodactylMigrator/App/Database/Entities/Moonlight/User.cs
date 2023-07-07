﻿using PterodactylMigrator.App.Models;

namespace PterodactylMigrator.App.Database.Entities.Moonlight;

public class User
{
    public int Id { get; set; }
    
    // Personal data

    public string FirstName { get; set; } = "";
    
    public string LastName { get; set; } = "";
    
    public string Email { get; set; } = "";
    
    public string Password { get; set; } = "";
    
    public string Address { get; set; } = "";
    
    public string City { get; set; } = "";
    
    public string State { get; set; } = "";
    
    public string Country { get; set; } = "";

    public string ServerListLayoutJson { get; set; } = "";

    // States
    
    public UserStatus Status { get; set; } = UserStatus.Unverified;
    public bool Admin { get; set; } = false;
    public bool SupportPending { get; set; } = false;
    public bool HasRated { get; set; } = false;
    public int Rating { get; set; } = 0;
    public bool StreamerMode { get; set; } = false;
    
    // Security
    public bool TotpEnabled { get; set; } = false;
    public string TotpSecret { get; set; } = "";
    public DateTime TokenValidTime { get; set; } = DateTime.UtcNow;
    
    // Discord
    public ulong DiscordId { get; set; }
    
    // Date stuff
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastVisitedAt { get; set; } = DateTime.UtcNow;
    
    // Subscriptions

    public Subscription? CurrentSubscription { get; set; } = null;
    public DateTime SubscriptionSince { get; set; } = DateTime.Now;
    public int SubscriptionDuration { get; set; }
}