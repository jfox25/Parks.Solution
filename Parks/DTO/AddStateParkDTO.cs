using System.ComponentModel.DataAnnotations;

namespace Park.DTO
{
  public class AddStateParkDTO
  {
    public string Name { get; set; }
    public int TotalNumberofVisitors { get; set; }
    public string StateName { get; set; }
  }
}