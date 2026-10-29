using System.ComponentModel.DataAnnotations;

namespace CampusCare.Models;

public class Complaint
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter your full name.")]
    [StringLength(80, MinimumLength = 2)]
    [Display(Name = "Student name")]
    public string StudentName { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a category.")]
    [StringLength(40)]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a complaint title.")]
    [StringLength(100, MinimumLength = 4)]
    [Display(Name = "Complaint title")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please describe the issue.")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters.")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Priority { get; set; } = "Medium";

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Submitted";

    [Display(Name = "Date submitted")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
