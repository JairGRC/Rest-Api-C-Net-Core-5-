using System.Linq;
using System;
using System.Collections.Generic;
using Catalog.Entities;
using Catalog.Interfaces;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Catalog.Dtos;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController:ControllerBase
    {
        private readonly IInMemItemsRepository repository;
        public ItemsController(IInMemItemsRepository repository)
        {
            this.repository=repository;
        }
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items=repository.GetItems().Select(item => item.AsDTO());
            return items;
        } 
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item=repository.GetItem(id);
            if(item is null){
                return NotFound();
            }
            return item.AsDTO();
        }
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item=new(){
                Id=Guid.NewGuid(),
                Name=itemDto.Name,
                Price=itemDto.Price,
                CreatedDate=DateTimeOffset.UtcNow
            };
            repository.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new {id=item.Id}, item.AsDTO());
        }

    }
}