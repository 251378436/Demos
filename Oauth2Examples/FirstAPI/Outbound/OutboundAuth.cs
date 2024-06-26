﻿namespace FirstAPI.Outbound;

public class OutboundAuth
{
    public virtual string Domain { get; set; }
    public virtual string? Audience { get; set; }
    public virtual string? ClientId { get; set; }
    public virtual string? ClientSecret { get; set; }
}
