﻿namespace WebApplication1.Models.Equipment
{
  public class EquipmentDTO
  {
    public int EquipmentId { get; set; }
    public string? Name { get; set; }
    public string? SerialNumber { get; set; }
    public string? Location { get; set; }
    public string? Status { get; set; }

    public string? Model { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? Specifications { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public DateTime? WarrantyDate { get; set; }
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
