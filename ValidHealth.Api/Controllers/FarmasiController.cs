using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValidHealth.Api.Dtos;
using ValidHealth.Api.Helpers;
using ValidHealth.Data;
using ValidHealth.Data.Models;
using ValidHealth.Data.Repository;
using ValidHealth.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ValidHealth.Api.Controllers
{
  [Route("api/farmasi")]
  [Authorize]
  public class FarmasiController : BaseController
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FarmasiController(IUnitOfWork unitOfWork,
      IMapper mapper)
    {
      _unitOfWork = unitOfWork;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] CommonFiltersDto filters)
    {
      // var models = await _unitOfWork.Farmasi.GetAllFarmasi(_mapper.Map<CommonFilters>(filters));
      if (string.IsNullOrWhiteSpace(filters.Nama))
      {
        filters.Nama = "># n/s #<";
      }
      JArray list = new JArray();

      if (!filters.Page.HasValue)
      {
        // IEnumerable<Farmasi> models = (await _unitOfWork.Farmasi.GetAll(
        //     q => q.Nama.StartsWith(filters.Nama))).OrderBy(a => a.Nama).Take(20);
        IEnumerable<Farmasi> models = await _unitOfWork.Farmasi.GetTopWhereOrderBy(
            q => q.Nama.StartsWith(filters.Nama), a => a.Nama, 20);
        // JArray list = new JArray();
        // JsonSerializer szer = CamelCaseJsonSerializer.Serializer;

        List<FarmasiDto> listf = new List<FarmasiDto>();
        int rowCount = 0;
        IEnumerator<Farmasi> emor = models.GetEnumerator();

        while (emor.MoveNext() && rowCount < 20)
        {
          // JObject obj = JObject.FromObject(_mapper.Map<FarmasiDto>(emor.Current), szer);
          // list.Add(obj);
          listf.Add(_mapper.Map<FarmasiDto>(emor.Current));
          rowCount++;
        }

        if (rowCount < 20)
        {
          // models = await _unitOfWork.Farmasi.GetAll(
          //     q => q.Nama.Contains(filters.Nama) && !q.Nama.StartsWith(filters.Nama));
          models = await _unitOfWork.Farmasi.GetTopWhereOrderBy(
              q => q.Nama.Contains(filters.Nama) && !q.Nama.StartsWith(filters.Nama),
              a => a.Nama, 20);
          emor = models.GetEnumerator();

          while (emor.MoveNext() && rowCount < 20)
          {
            // JObject obj = JObject.FromObject(_mapper.Map<FarmasiDto>(emor.Current), szer);
            // list.Add(obj);
            listf.Add(_mapper.Map<FarmasiDto>(emor.Current));
            rowCount++;
          }
        }

        await Task.Run(() =>
        {
          list = JArray.FromObject(listf, CamelCaseJsonSerializer.Serializer);
        });
      }
      else
      {
        PaginatedList<Farmasi> pl = (PaginatedList<Farmasi>)await
            _unitOfWork.Farmasi.GetWhereOrderByPaged(q => q.Nama.StartsWith(filters.Nama), 
            a => a.Nama, (int)filters.PageLength, (int)filters.Page);
        int totalPages = pl.TotalPages;
        IEnumerable<Farmasi> models = pl;
        int nGap = (int)filters.PageLength - pl.Count;
        int pageLenB = (int)filters.PageLength + nGap;

        PaginatedList<Farmasi> plB = (PaginatedList<Farmasi>)await
            _unitOfWork.Farmasi.GetWhereOrderByPaged(
            q => q.Nama.Contains(filters.Nama) && !q.Nama.StartsWith(filters.Nama),
            a => a.Nama, (int)filters.PageLength, 1);
        bool hasNextPage = (int)filters.Page < totalPages;

        if (filters.Page == totalPages)
        {
          if (nGap > 0)
          {
            models = models.Concat(plB.Take(nGap));
          }
          plB = (PaginatedList<Farmasi>)await
              _unitOfWork.Farmasi.GetWhereOrderByPaged(
              q => q.Nama.Contains(filters.Nama) && !q.Nama.StartsWith(filters.Nama),
              a => a.Nama, (int)filters.PageLength, plB.TotalPages);
          totalPages = totalPages + plB.TotalPages;
          if (plB.Count <= nGap)
          {
            totalPages--;
          }
          hasNextPage = (int)filters.Page < totalPages;
        }
        else if (filters.Page > totalPages)
        {
          if (totalPages > 0)
          {
            pl = (PaginatedList<Farmasi>)await
                _unitOfWork.Farmasi.GetWhereOrderByPaged(q => q.Nama.StartsWith(filters.Nama),
                a => a.Nama, (int)filters.PageLength, totalPages);
            nGap = (int)filters.PageLength - pl.Count;
            pageLenB = (int)filters.PageLength + nGap;
          }

          int nSkipPA = ((int)filters.Page - 1 - totalPages) * (int)filters.PageLength;
          int nSkipPB = nGap + nSkipPA;
          int nPageToSkipB = nSkipPB / pageLenB;
          int nRowToSkipB = nSkipPB % pageLenB;
          plB = (PaginatedList<Farmasi>)await
              _unitOfWork.Farmasi.GetWhereOrderByPaged(
              q => q.Nama.Contains(filters.Nama) && !q.Nama.StartsWith(filters.Nama),
              a => a.Nama, pageLenB, nPageToSkipB + 1);
          totalPages += plB.TotalPages;
          if (plB.Count > filters.PageLength)
          {
            totalPages++;
          }
          hasNextPage = (int)filters.Page < totalPages;

          if (plB.Count > nRowToSkipB)
          {
            models = models.Concat(plB.Skip(nRowToSkipB));
            if (plB.Count > filters.PageLength && filters.PageLength > 0)
            {
              models = models.Take((int) filters.PageLength);
            }

            if (nRowToSkipB > nGap)
            {
              plB = (PaginatedList<Farmasi>)await
                            _unitOfWork.Farmasi.GetWhereOrderByPaged(
                            q => q.Nama.Contains(filters.Nama) && !q.Nama.StartsWith(filters.Nama),
                            a => a.Nama, pageLenB, nPageToSkipB + 2);

              if (plB.Count > nRowToSkipB - nGap && nRowToSkipB - nGap > 0)
              {
                models = models.Concat(plB.Take(nRowToSkipB - nGap));
              }
              else
              {
                models = models.Concat(plB);
              }
            }
          }
          else
          {
            models = plB;
          }
        }
        else
        {
          totalPages += plB.TotalPages;
        }

        return Ok(new
        {
          list = _mapper.Map<IEnumerable<FarmasiDto>>(models),
          pageIndex = (int)filters.Page,
          totalPages = totalPages,
          hasNextPage = hasNextPage,
          hasPrevPage = (int)filters.Page > 1,
          count = models.Count()
        });
      }

      // return Ok(_mapper.Map<IEnumerable<FarmasiDto>>(models));
      // return Ok(CamelCaseJsonSerializer.SerializeObject(models));
      return Ok(list.ToString());
    }

    [HttpGet("{id}", Name = "GetFarmasi")]
    public async Task<IActionResult> Get(long id)
    {
      var model = await _unitOfWork.Farmasi.Get(id);

      if (model == null)
        return NotFound("Data farmasi tidak ada");

      return Ok(_mapper.Map<FarmasiDto>(model));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FarmasiCreateDto dto)
    {
      if (dto == null) return BadRequest();

      if (!ModelState.IsValid) return new UnprocessableEntityResult(/* ModelState */);

      var model = _mapper.Map<Farmasi>(dto);

      _unitOfWork.Farmasi.Add(model);

      await _unitOfWork.Complete();

      return CreatedAtRoute("GetFarmasi", new { id = model.Id }, _mapper.Map<FarmasiDto>(model));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(long id, [FromBody] FarmasiCreateDto dto)
    {
      if (dto == null) return BadRequest();

      if (!ModelState.IsValid) return new UnprocessableEntityResult(/* ModelState */);

      var model = await _unitOfWork.Farmasi.Get(id);

      if (model == null) return NotFound("Data farmasi tidak ada");

      _mapper.Map(dto, model);

      await _unitOfWork.Complete();

      return Ok(_mapper.Map<FarmasiDto>(model));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
      try
      {
        var model = await _unitOfWork.Farmasi.Get(id);

        if (model == null) return NotFound("Data farmasi tidak ada");

        _unitOfWork.Farmasi.Remove(model);

        await _unitOfWork.Complete();

        return Ok();
      }
      catch (Exception e)
      {
        ModelState.AddModelError("", e.InnerException?.Message ?? e.Message);
        return BadRequest(ModelState);
      }
    }
  }
/* 
  public class LowercaseContractResolver : DefaultContractResolver
  {
    protected override string ResolvePropertyName(string propertyName)
    {
      return propertyName.ToLower();
    }
  }

  public class LowercaseNamingStrategy : NamingStrategy
  {
    protected override string ResolvePropertyName(string name)
    {
      return name.ToLowerInvariant();
    }
  }
 */
}
