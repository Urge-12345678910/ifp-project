using System.ComponentModel.DataAnnotations;

namespace BlazorFinalProject.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "enter the name")]
    [StringLength(50)]
    public string Name { get; set; } = "";

    public string Color { get; set; } = "#6c757d";
}