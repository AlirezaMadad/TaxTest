using System;
using System.Collections.Generic;

namespace Presentation.Models;

public partial class TaxPeriod
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int TaxAmmount { get; set; }
}
