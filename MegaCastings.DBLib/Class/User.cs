using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class User
{
    public uint Id { get; set; }

    public string? Nom { get; set; }

    public string? Prenom { get; set; }
}
