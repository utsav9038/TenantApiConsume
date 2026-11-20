using System;
using System.Collections.Generic;

namespace TenatConsume.Models;


public  class TenantUser
{
    public long UserId { get; set; }

    public long TenantId { get; set; }

    public string? LoginId { get; set; }

    public string? LoginType { get; set; }

    public string? Password { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
