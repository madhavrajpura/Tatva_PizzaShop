using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels;

public class TablesViewModel
{
    public long TableId { get; set; }

    public long SectionId { get; set; }

    [Required(ErrorMessage = "Table Name is Required")]
    public string TableName { get; set; } = null!;

    [Required(ErrorMessage = "Capacity is Required")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Status is Required")]
    public string Status { get; set; } = null!;

    public bool Isdelete { get; set; }

}
