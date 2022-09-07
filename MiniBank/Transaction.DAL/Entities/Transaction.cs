using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transaction.DAL.Models;

namespace Transaction.DAL.Entities;

public class Transaction
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public Guid FromAccountId { get; set; }

    [Required]
    public Guid ToAccountId { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public StatusEnum Status { get; set; }

    public string? FailureReason { get; set; }

}


