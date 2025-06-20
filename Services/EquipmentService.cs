﻿using Microsoft.EntityFrameworkCore;
using Uninventory.Interfaces;
using WebApplication1.Models.Equipment;
using WebApplication1.Persistence.Models;
using WebApplication1.Persistence;

namespace Uninventory.Services
{
  public class EquipmentService : IEquipmentService
  {
    private readonly UninventoryDBContext _context;

    public EquipmentService(UninventoryDBContext context)
    {
      _context = context;
    }

    private EquipmentDTO ToEquipmentDTO(Equipment eq)
    {
      return new EquipmentDTO
      {
        EquipmentId = eq.EquipmentId,
        Name = eq.Name,
        CategoryId = eq.CategoryId,
        CategoryName = eq.Category.Name,
        SerialNumber = eq.SerialNumber,
        Status = eq.Status,
        Model = eq.Model,
        Description = eq.Description,
        Specifications = eq.Specifications,
        Image = eq.Image,
        Location = eq.Location,
        PurchaseDate = eq.PurchaseDate,
        WarrantyDate = eq.WarrantyDate,
        CreatedAt = eq.CreatedAt
      };
    }

    public async Task<EquipmentDTO> AddEquipment(EquipmentDTO add)
    {
      var equipment = new Equipment
      {
        Name = add.Name,
        CategoryId = add.CategoryId,
        SerialNumber = add.SerialNumber,
        Status = add.Status,
        Model = add.Model,
        Description = add.Description,
        Specifications = add.Specifications,
        Image = add.Image,
        Location = add.Location,
        PurchaseDate = add.PurchaseDate,
        WarrantyDate = add.WarrantyDate,



      };
      await _context.Equipment.AddAsync(equipment);

      await _context.SaveChangesAsync();

      return await GetEquipment(equipment.EquipmentId);
    }

    public async Task<IEnumerable<EquipmentDTO>> GetEquipmentsByCategory(int? categoryId)
    {
      var query = _context.Equipment
        .Include(e => e.Category).Where(e => e.Status == "available")
        .AsQueryable();


      if (categoryId.HasValue)
      {
        query = query.Where(e => e.CategoryId == categoryId.Value);
      }
      var equipments = await query.ToListAsync();

      return equipments.Select(ToEquipmentDTO).ToList();
    }

    public async Task<IEnumerable<EquipmentDTO>> GetEquipments(int? EquipmentId)
    {
      var query = _context.Equipment
        .Include(e => e.Category)
        .AsQueryable();


      if (EquipmentId.HasValue)
      {
        query = query.Where(e => e.EquipmentId == EquipmentId.Value);
      }
      var equipments = await query.ToListAsync();

      return equipments.Select(ToEquipmentDTO).ToList();
    }

    public async Task<EquipmentDTO> GetEquipment(int equipmentId)
    {
      var equipments = await GetEquipments(equipmentId);
      if (!equipments.Any())
      {
        throw new Exception($"El equipo {equipmentId} no está registrado.");
      }
      return equipments.First();
    }
    public async Task<EquipmentDTO> SetEquipment(int equipmentId, EquipmentDTO equipmentDTO)
    {
      var equipment = await _context.Equipment.FirstOrDefaultAsync(e => e.EquipmentId == equipmentId);

      if (equipment == null)
      {
        throw new Exception($"El equipo {equipmentId} no está registrado.");
      }

      equipment.Name = equipmentDTO.Name ?? equipment.Name;
      equipment.CategoryId = equipmentDTO.CategoryId;
      equipment.SerialNumber = equipmentDTO.SerialNumber ?? equipment.SerialNumber;
      equipment.Status = equipmentDTO.Status ?? equipment.Status;
      equipment.Model = equipmentDTO.Model ?? equipment.Model;
      equipment.Description = equipmentDTO.Description ?? equipment.Description;
      equipment.Specifications = equipmentDTO.Specifications ?? equipment.Specifications;
      equipment.Image = equipmentDTO.Image ?? equipment.Image;
      equipment.Location = equipmentDTO.Location ?? equipment.Location;
      equipment.PurchaseDate = equipmentDTO.PurchaseDate ?? equipment.PurchaseDate;
      equipment.WarrantyDate = equipmentDTO.WarrantyDate ?? equipment.WarrantyDate;

      await _context.SaveChangesAsync();

      return await GetEquipment(equipmentId);


    }

    public async Task<IEnumerable<EquipmentDTO>> SearchByName(string name)
    {
      var lowerName = name.ToLower();

      var equipments = await _context.Equipment
          .Include(e => e.Category)
          .Where(e => e.Name.ToLower().Contains(lowerName))
          .ToListAsync();

      return equipments.Select(ToEquipmentDTO).ToList();
    }


  }
}
