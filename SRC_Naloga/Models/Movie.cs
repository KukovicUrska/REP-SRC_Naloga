namespace SRC_Naloga.Models
{
    public class Movie
    {
        public int imdbID { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string description { get; set; }
        public List<Actor> list_actors { get; set; }

        public Movie() { 
            
        }
        public Movie(int idmovie, string Title, int Year, string Description, List<Actor> listactor)
        {
            this.imdbID = idmovie;
            this.title = Title;
            this.year = Year;
            this.description = Description;
            this.list_actors = listactor;
        }

    }
}
