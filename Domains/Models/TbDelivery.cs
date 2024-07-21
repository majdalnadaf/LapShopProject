using System;
using System.Collections.Generic;

namespace Domains.Models;


public partial class TbDelivery
{
    public int DeliveryId { get; set; }

    public int CountOfDeliveryEvent { get; set; }

    public string? DeliveryName { get; set; }

    public virtual ICollection<TbDeliveryInfo> TbDeliveriesInfo { get; set; } = new List<TbDeliveryInfo>();
}
