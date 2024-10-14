using System;
using System.Collections.Generic;

namespace SRC_Naloga.DBConnection;

public partial class Movie
{
    public string IdMovie { get; set; } = null!;

    public string? Title { get; set; }

    public int? Year { get; set; }

    public string? Description { get; set; }

    public byte[]? Picture { get; set; }

    public virtual ICollection<Actor> TkIdActors { get; } = new List<Actor>();
}
