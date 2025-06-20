﻿using WebApplication1.Models.Equipment;

namespace Uninventory.Interfaces
{
  public interface IEquipmentService
  {
    public Task<EquipmentDTO> AddEquipment(EquipmentDTO add);
    public Task<IEnumerable<EquipmentDTO>> GetEquipments(int? EquipmentId);
    public Task<EquipmentDTO> GetEquipment(int EquipmentId);
    public Task<EquipmentDTO> SetEquipment(int EquipmentId, EquipmentDTO equipmentDTO);

    public Task<IEnumerable<EquipmentDTO>> GetEquipmentsByCategory(int? categoryId);

    Task<IEnumerable<EquipmentDTO>> SearchByName(string name);

  }
}
