using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models;

public class Movie
{
    public int ID { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [Range(0, 10)]
    public int Rating { get; set; }

    [Required]
    [Range(0, Int32.MaxValue)]
    public int Budget { get; set; }

    [Required]
    [Range(0, Int32.MaxValue)]
    public int Gross { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Release Date")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public string Genre { get; set; }

    [Required]
    public int Runtime { get; set; }

    public string Summary { get; set; }
}

public class Actor
{
    public int ID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Birth City")]
    public string BirthCity { get; set; }

    [Required]
    [Display(Name = "Birth Country")]
    public string BirthCountry { get; set; }

    [Required]
    [Range(0, 1000)]
    [Display(Name = "Height (inches)")]
    public int HeightInInches { get; set; }

    [Required]
    public string Biography { get; set; }

    [Required]
    [RegularExpression("Male|Female")]
    public string Gender { get; set; }

    [Required]
    [Range(0, Int32.MaxValue)]
    [Display(Name = "Net Worth")]
    public int NetWorth { get; set; }

    public List<Character>? Characters { get; set; }
}

public class Character
{
    public int ID { get; set; }

    [Required]
    [Display(Name = "Character Name")]
    public string CharacterName { get; set; }

    [Required]
    public int Pay { get; set; }

    [Required]
    [Range(0, Int32.MaxValue)]
    public int Screentime { get; set; }

    [Required]
    [Display(Name = "Movie")]
    public int MovieID { get; set; }

    [Required]
    [Display(Name = "Actor")]
    public int ActorID { get; set; }

    public Movie? Movie { get; set; }
    public Actor? Actor { get; set; }
}
