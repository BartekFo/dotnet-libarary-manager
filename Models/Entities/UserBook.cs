using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManager.Models.Entities;

public class UserBook
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string BookKey { get; set; } = string.Empty;

    [Required]
    public ReadingStatus Status { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [NotMapped]
    public Book? Book { get; set; }
}

public enum ReadingStatus
{
    WantToRead,
    CurrentlyReading,
    Completed
}