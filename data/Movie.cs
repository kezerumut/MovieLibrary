using System.ComponentModel.DataAnnotations;

namespace MovieDBApp.Data{

    public class Movie{

        [Key]
        public int MovieId {get;set;}
        public string? MovieName {get;set;}
        public string? MovieGenre {get;set;}
        public string? ReleaseDate {get;set;}
        public string? Director {get;set;}
        public int Point {get;set;}
    }
}