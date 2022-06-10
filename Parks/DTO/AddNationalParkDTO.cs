using System.ComponentModel.DataAnnotations;

namespace Park.DTO
{
  public class AddNationalParkDTO
  {
    public string Name { get; set; }
    public int TotalNumberofVisitors { get; set; }
    public string StateName { get; set; }
  }
}