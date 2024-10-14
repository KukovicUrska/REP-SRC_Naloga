namespace SRC_Naloga.Models
{
    public class Actor
    {
        public int id_actor { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateBorn { get; set; }
        public List<Movie> list_moviesOfActor { get; set; }

        public Actor() { }
        public Actor(int idactor, string firstname, string lastname, DateTime dateBorn, List<Movie> listmovie)
        {
            this.id_actor = idactor;
            this.firstName = firstname;
            this.lastName = lastname;
            this.dateBorn = dateBorn;
            this.list_moviesOfActor = listmovie;

        }
    }
}
