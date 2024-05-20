using System;
using System.ComponentModel.DataAnnotations;

public class Bills
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Bill Type is required.")]
    public string BillType { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Due Date is required.")]
    [DataType(DataType.Date)]
    [DateInTheFuture(ErrorMessage = "Due Date must be in the future.")]
    public DateTime DueDate { get; set; }

    public bool IsPaid { get; set; }

    public string UserId { get; set; }
}

public class DateInTheFutureAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }
        return false;
    }
}
