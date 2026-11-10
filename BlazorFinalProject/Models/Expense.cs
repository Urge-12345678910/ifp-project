using System.ComponentModel.DataAnnotations;

namespace BlazorFinalProject.Models;

public class Expense
{
    public int Id { get; set; }

    [Required(ErrorMessage = "enter the name")]
    [StringLength(100)]
    public string Title { get; set; } = "";

    [Range(0.01, 1000000, ErrorMessage = "sum from 0.01 to 1 000 000")]
    public decimal Amount { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;

    public string? Note { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "choose the category")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }
}