using System;
using System.Collections.Generic;

namespace SRC_Naloga.DBConnection;

public partial class Actor
{
    public string IdActor { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? BornDate { get; set; }

    public virtual ICollection<Movie> TkIdMovies { get; } = new List<Movie>();
}
