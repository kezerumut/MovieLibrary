using System.ComponentModel.DataAnnotations;

namespace MovieDBApp.Data{

    public class MovieRegister{
        [Key]
        public int RegisterId {get;set;}
        public int MovieId {get;set;}
        public int BootcampId {get;set;}
        public DateTime RegisterDate {get;set;}
    }
}