﻿using Microsoft.AspNetCore.Mvc;
using Uninventory.Interfaces;
using WebApplication1.Models.Equipment;

namespace Uninventory.Controllers
{
  [ApiController]
  [Route("api/")]
  public class EquipmentController : ControllerBase
  {

    private readonly IEquipmentService _equipmentService;

    public EquipmentController(IEquipmentService equipmentService)
    {
      _equipmentService = equipmentService;
    }

    [HttpPost("equipment")]
    public async Task<EquipmentDTO> AddEquipment(EquipmentDTO add)
    {
      return await _equipmentService.AddEquipment(add);
    }
    [HttpGet("equipment")]
    public async Task<IEnumerable<EquipmentDTO>> GetEquipments(int? EquipmentId)
    {
      return await _equipmentService.GetEquipments(EquipmentId);
    }

    [HttpGet("equipment/{EquipmentId}")]
    public async Task<EquipmentDTO> GetEquipmentById(int EquipmentId)
    {
      return await _equipmentService.GetEquipment(EquipmentId);
    }

    [HttpGet("equipment/category/{categoryId}")]
    public async Task<IEnumerable<EquipmentDTO>> GetEquipmentsByCategory(int? categoryId)
    {
      return await _equipmentService.GetEquipmentsByCategory(categoryId);
    }
    [HttpGet("equipment/search/name")]
    public async Task<IEnumerable<EquipmentDTO>> SearchByName([FromQuery] string name)
    {
      return await _equipmentService.SearchByName(name);
    }


    [HttpPut("equipment/{EquipmentId}")]
    public async Task<EquipmentDTO> SetEquipment(int EquipmentId, EquipmentDTO equipmentDTO)
    {
      return await _equipmentService.SetEquipment(EquipmentId, equipmentDTO);
    }


  }
}
