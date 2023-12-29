using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class SubCategory
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int BigCatgoryId { get; set; }

    public virtual ICollection<Announce> Announces { get; set; } = new List<Announce>();

    public virtual BigCategory BigCatgory { get; set; } = null!;
}
