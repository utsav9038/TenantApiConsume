using System;
using System.Collections.Generic;

namespace TenatConsume.Models;

public class Tenant
{
    public long TenantId { get; set; }

    public string TenantName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? DatabaseConnection { get; set; }

    public string? ServiceUrl { get; set; }

    public string? ServiceUserId { get; set; }

    public string? ServicePassword { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<TenantUser> TenantUsers { get; set; } = new List<TenantUser>();
}
